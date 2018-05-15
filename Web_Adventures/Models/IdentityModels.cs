using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_Adventures.Migrations;

namespace Web_Adventures.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<DbPurchaseTickets> orderRequest { get; set; }
        public DbSet<DbPersonalDatas> datas { get; set; }
        public DbSet<DbNameBuyers> buyers { get; set; }
    }

    public class DbPurchaseTickets
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Дата и время заполнения
        /// </summary>
        public DateTime FilledTime { get; set; }
        /// <summary>
        /// Информация о человеке
        /// </summary>
        public DbPersonalDatas Person { get; set; }
        /// <summary>
        /// Точка отправления
        /// </summary>
        public BeginPoint BeginPoint { get; set; }
        /// <summary>
        /// Точка прибытия
        /// </summary>
        public EndPoint EndPoint { get; set; }
        /// <summary>
        /// Стоиомость билета
        /// </summary>
        public double Price { get; set; }
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
    public class DbPersonalDatas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// Полное имя человека
        /// </summary>
        public DbNameBuyers FullName { get; set; }
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
    public class DbNameBuyers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
    }
}