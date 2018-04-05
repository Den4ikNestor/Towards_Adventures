using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Towards_Adventures
{
    public static class Serializer
    {
        private static readonly XmlSerializer Xs = new XmlSerializer(typeof(PurchaseTicketsDto));
        public static void WriteToFile(string fileName, PurchaseTicketsDto data)
        {
            using (var fileStream = File.Create(fileName))
            {
                Xs.Serialize(fileStream, data);
            }
        }

        public static PurchaseTicketsDto LoadFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return (PurchaseTicketsDto)Xs.Deserialize(fileStream);
            }
        }
    }
}
