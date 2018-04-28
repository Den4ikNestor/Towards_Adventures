using System;
using System.Collections.Generic;

namespace Towards_Adventures
{
    class Program
    {
        static void Main(string[] args)
        {
            /////////////
        }
    }
    /// <summary>
    /// Информация о предстоящей поезде покупателя билета(-ов)
    /// </summary>
    public class PurchaseTicketsDto
    {
        /// <summary>
        /// Дата и время заполнения
        /// </summary>
        public DateTime FilledTime { get; set; }
        /// <summary>
        /// Информация о человеке
        /// </summary>
        public PersonalData Person { get; set; }
        /// <summary>
        /// Точка отправления
        /// </summary>
        public BeginPoint BeginPoint { get; set; }
        /// <summary>
        /// Точка прибытия
        /// </summary>
        public EndPoint EndPoint { get; set; }
        /// <summary>
        /// Информация о поезде
        /// </summary>
        public TrainData Train { get; set; }
        /// <summary>
        /// Стоиомость билета
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Валюта
        /// </summary>
        public Currency Currency { get; set; }
        /// <summary>
        /// Ресторанная еда
        /// </summary>
        public bool RestaurantFood { get; set; }
        /// <summary>
        /// Холодильник
        /// </summary>
        public bool Fridge { get; set; }
        /// <summary>
        /// Стоимость дополнительной услуги
        /// </summary>
        public double AdditionalServicePrice { get; set; }
    }

    public enum BeginPoint
    {
        Ekaterinburg,
        Moscow,
    }

    public enum EndPoint
    {
        Kazan,
        Vladivostok,
    }
    /// <summary>
    /// Персональные данные
    /// </summary>
    public class PersonalData
    {
        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// Полное имя человека
        /// </summary>
        public NameBuyer FullName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateBirth { get; set; }
        /// <summary>
        /// Документ
        /// </summary>
        public Document DocumentType { get; set; }
        /// <summary>
        /// Серия документа
        /// </summary>
        public int Series { get; set; }
        /// <summary>
        /// номер документа
        /// </summary>
        public double Number { get; set; }
    }
    /// <summary>
    /// Тип документа
    /// </summary>
    public enum Document
    {
        Passport,
        BirthSertificate,
    }
    public enum Sex
    {
        Male,
        Female,
    }
    /// <summary>
    /// Полное имя покупателя
    /// </summary>
    public class NameBuyer
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", LastName, FirstName, Patronymic);
        }
    }

    /// <summary>
    /// Данные о маршруте
    /// </summary>
    public class WayPoint
    {
        /// <summary>
        /// Название Города
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// Тип точки
        /// </summary>
        public WayPointType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", CityName, Type);
        }

        public WayPoint Clone()
        {
            return new WayPoint { CityName = CityName, Type = Type };
        }
    }
    /// <summary>
    /// Тип точки
    /// </summary>
    public enum WayPointType
    {
        Begin,
        End,
    }
    public class TrainData
    {
        /// <summary>
        /// Время прибытия
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// Время отправления
        /// </summary>
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// Тип вагона
        /// </summary>
        public WagonType WagonType { get; set; }
        /// <summary>
        /// Номер вагона
        /// </summary>
        public int WagonNumber { get; set; }
        /// <summary>
        /// Место
        /// </summary>
        public int Seat { get; set; }
    }

    public enum WagonType
    {
        ReservedTicket,
        Compartment,
        Lux,
    }
    /// <summary>
    /// Валюта
    /// </summary>
    public enum Currency
    {
        Rubles
    }
}
