using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinqToSQLMvcApplication.DAL;
using LinqToSQLMvcApplication.Models;

namespace LinqToSQLMvcApplication.Controllers
{
    public class PublisherController : Controller
    {
        private OperationDataContext context;
        public PublisherController()
        {
            context = new OperationDataContext();
        }

        public ActionResult Index()
        {         
            IList<PublisherModel> publisherList = new List<PublisherModel>();
            var query = from publisher in context.Publishers
                        select publisher;            
            var publishers = query.ToList();
            foreach(var publisherData in publishers )
            {
               publisherList.Add(new PublisherModel()
                {
                    Id= publisherData.Id,
                    Name = publisherData.Name,
                    Year = publisherData.Year
                });
            }
            return View(publisherList);
        }    
        
        public ActionResult Create()
        {
            PublisherModel model = new PublisherModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PublisherModel model)
        {
            try
            {                
                Publisher publisher = new Publisher()
                {
                    Name = model.Name,
                    Year = model.Year
                };
                context.Publishers.InsertOnSubmit(publisher);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }       
    }
}
