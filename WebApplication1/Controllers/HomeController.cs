using MarkupLanguage.Html;
using MarkupLanguage.Html.DTO;
using MarkupLanguage.Html.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class MyData
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class HomeController : Controller
    {

        public List<MyData> Fonk()
        {
            var list = new List<MyData>()
            {
                new MyData()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name1",
                    Value = "Value1"
                },
                new MyData()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name2",
                    Value = "Value2"
                },
                new MyData()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name3",
                    Value = "Value3"
                }
            };

            Task.Delay(3000);

            return list;
        }

        public HtmlElement GetTable()
        {
            var t =  TableElement.ObjectToAdvancedTableElement(new List<string>() { "FirtColumn", "SecondColumn" }, null);

            return t;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Get(FormCollection fc)
        {
            var table = await Task.Run(() => GetTable());

            var pageNumber = fc.AllKeys.Contains("pageNumber") ? Convert.ToInt32(fc["pageNumber"]) : 1;
            var searchText = fc.AllKeys.Contains("searchText") ? fc["searchText"].ToString() : null;
            var headers = new Dictionary<string, string>() { {"",""}};

            var list = Fonk();

            TableDTO tableDTO = new TableDTO("MyTable",Guid.NewGuid());

            foreach (var item in list)
            {
                tableDTO.AddHeaders(new HeaderDTO(item.Name));
                tableDTO.AddRows(new RowDTO(item.Value));
            }

            var dt = new DataTable(pageNumber, searchText, tableDTO);

            var response = new
            {
                Json = dt.TableDiv.ToJson(),
               // Datatable = dt
            };

            //var response = JsonConvert.DeserializeObject<TableElement>(table.ToJson());


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}