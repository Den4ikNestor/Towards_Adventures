using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Towards_Adventures;
using System.Collections.Generic;

namespace UnitTestProgram
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dto = new PurchaseTicketsDto
            {
                FilledTime = DateTime.Now,
                WayPoints = new List<WayPoint>()
                {
                    new WayPoint
                    {
                        CityName = "Yekaterinburg",
                        Type = WayPointType.Begin,
                    },
                    new WayPoint
                    {
                        CityName = "Kazan",
                        Type = WayPointType.End,
                    },
                },
                Price = 4999,
                Currency = Currency.Rubles,
                Person = new PersonalData()
                {
                    Sex = "мужской",
                    DateBirth = new DateTime(1995, 4, 30),
                    DocumentType = Document.Passport,
                    FullName = new NameBuyer()
                    {
                        LastName = "Васильев",
                        FirstName = "Игорь",
                        Patronymic = "Анатольевич",
                    },
                    Number = 530445,
                    Series = 6654,
                },
                Train = new TrainData()
                {
                    ArrivalTime = new DateTime(2018, 4, 20, 6,45,00),
                    DepartureTime = new DateTime(2018, 4, 21, 19,50,00),
                    Seat = 32,
                    WagonType = WagonType.Lux,
                    WagonNumber = 4,
                },
                AdditionalServices = new List<Services>()
                {
                    Services.WiFi,
                    Services.RestaurantFood,
                },
                AdditionalServicePrice = 4500,
            };


            var tempFileName = "Order.xml";
            try
            {
                Serializer.WriteToFile(tempFileName, dto);
                var readDto = Serializer.LoadFromFile(tempFileName);
                Assert.AreEqual(dto.FilledTime, readDto.FilledTime);
                Assert.AreEqual(dto.Person.DateBirth, readDto.Person.DateBirth);
                Assert.AreEqual(dto.Train.Seat, readDto.Train.Seat);
            }
            finally
            {
                File.Delete(tempFileName);
            }
        }
    }
}
