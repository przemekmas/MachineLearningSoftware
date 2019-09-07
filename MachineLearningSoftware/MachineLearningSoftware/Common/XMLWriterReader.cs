using MachineLearningSoftware.DataAccess;
using System;
using System.IO;
using System.Xml.Serialization;

namespace MachineLearningSoftware.Common
{
    public static class XMLWriterReader
    {
        private static ExceptionLogDataAccess _exceptionLogging = DependencyInjection.ResolveSingle<ExceptionLogDataAccess>();

        public static void SerialiseAndWriteObject(string filePath, object parameter, Type type)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    var serialiser = new XmlSerializer(type);
                    serialiser.Serialize(writer, parameter);
                }   
            }
            catch (Exception ex)
            {
                _exceptionLogging.LogException(ex.ToString());
            }
        }

        public static object DeserialiseObject(string filePath, Type type)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var serialiser = new XmlSerializer(type);
                    return serialiser.Deserialize(fileStream);
                }   
            }
            catch (Exception ex)
            {
                _exceptionLogging.LogException(ex.ToString());
            }
            return null;
        }
    }
}
