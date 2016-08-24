using Ada.Framework.Development.Log4Me.Entities;
using Ada.Framework.Util.FileMonitor;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Ada.Framework.Development.Log4Me.Writers.FileWrite
{
    [Serializable]
    public class FileWriter : ALogWriter
    {
        private static FileStream file;
        private static StreamWriter writer;

        [XmlAttribute]
        public string OutputPath { get; set; }

        protected override void Agregar(RegistroInLineTO registro)
        {
            writer.WriteLine(Formatear(registro));
        }

        public override void Inicializar()
        {
            if (file == null)
            {
                IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
                file = new FileStream(monitor.ObtenerRutaAbsoluta(OutputPath), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            }

            if (writer == null)
            {
                writer = new StreamWriter(file) { AutoFlush = true };
            }
        }

        public override void AgregarParametros()
        {
            writer.WriteLine(FormatoSalida);
            writer.WriteLine(SeparadorSalida);
        }
    }
}
