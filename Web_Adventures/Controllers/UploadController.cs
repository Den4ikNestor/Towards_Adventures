using System.Web;
using System.Web.Mvc;
using Towards_Adventures;
using Web_Adventures.Models;

namespace Web_Adventures.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Print(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var dto = Serializer.LoadFromStream(file.InputStream);

                using (var db = new ApplicationDbContext())
                {
                    var row = new DbPurchaseTickets
                    {
                        FilledTime = dto.FilledTime,
                        BeginPoint = (Models.BeginPoint)dto.BeginPoint,
                        EndPoint = (Models.EndPoint)dto.EndPoint,
                        Price = dto.Price,
                        RestaurantFood = dto.RestaurantFood,
                        Fridge = dto.Fridge,
                        AdditionalServicePrice = dto.AdditionalServicePrice,
                    };

                    row.Person = new DbPersonalDatas
                    {
                        DateBirth = dto.Person.DateBirth,
                        DocumentType = (Models.Document)dto.Person.DocumentType,
                        Sex = (Models.Sex)dto.Person.Sex,
                        Number = dto.Person.Number,
                        Series = dto.Person.Series,
                    };

                    row.Person.FullName = new DbNameBuyers
                    {
                        LastName = dto.Person.FullName.LastName,
                        FirstName = dto.Person.FullName.FirstName,
                        Patronymic = dto.Person.FullName.Patronymic,
                    };
                    db.orderRequest.Add(row);
                    db.SaveChanges();
                }
                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}