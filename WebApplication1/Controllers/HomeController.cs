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
                },
                new MyData()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name4",
                    Value = "Value4"
                },
                new MyData()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name5",
                    Value = "Value5"
                },
                new MyData()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name6",
                    Value = "Value6"
                }
            };

            Task.Delay(3000);

            return list;
        }

        public HtmlElement GetTable()
        {
            var t = TableElement.ObjectToAdvancedTableElement(new List<HeaderDTO>() { new HeaderDTO("FirtColumn"), new HeaderDTO("SecondColumn") }, new List<RowDTO>() { new RowDTO() });

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
            var list = new Dictionary<string, string>();
            foreach (string key in fc.Keys)
            {
                list.Add(key, fc[key]);
            }
            var x = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            if (fc.AllKeys.Contains("ColumnsCount"))
            {
                var lenght = Convert.ToInt32(fc["ColumnsCount"]);

                for (int i = 0; i < lenght ; i++)
                {
                    var columnName = fc[string.Format("Columns[{0}][Name]", i)].ToString();
                }
            }



            var table = await Task.Run(() => GetTable());

            var pageNumber = fc.AllKeys.Contains("CurrentPage") ? Convert.ToInt32(fc["CurrentPage"]) : 1;
            var searchText = fc.AllKeys.Contains("searchText") ? fc["searchText"].ToString() : null;
            var headers = new Dictionary<string, string>() { {"",""}};

            var objectList = Fonk();

            TableDTO tableDTO = new TableDTO("MyTable",Guid.NewGuid());

            tableDTO.AddHeaders(new HeaderDTO("1H"), new HeaderDTO("2H"), new HeaderDTO("3H"));

            foreach (var item in objectList)
            {
                tableDTO.AddRows(new RowDTO().AddColumns(new ColumnDTO(item.Id), new ColumnDTO(item.Name), new ColumnDTO(item.Value)));
            }

            var dt = new DataTable(pageNumber, searchText, tableDTO);

            var response = new
            {
                OuterHtml = dt.TableDiv.OuterHtml,
                Columns = tableDTO.Headers.Select(h => new 
                {
                    Name = h.Name,
                    IsSorting = h.IsSorting,
                    SortDirection = h.SortDirection==SortDirection.Ascending ? "asc" : "desc"
                }),
                ColumnsCount = tableDTO.Headers.Count
                //Json = dt.TableDiv.ToJson(),
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