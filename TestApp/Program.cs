using MarkupLanguage;
using MarkupLanguage.Html;
using MarkupLanguage.Html.Attributes;
using MarkupLanguage.Html.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var te = TableElement.ObjectToAdvancedTableElement(new[] { "column1", "column2" }.ToList(), new[] { "row1", "row2" }.ToList());

            var json = JsonConvert.SerializeObject(te, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var ggg = te.ToString();
            HtmlElement element = new HtmlElement("MyTag");

            element.AddAttributes(new HtmlStyle("background-color", "powderblue"),
                new HtmlStyle("float", "right"),
                new HtmlClass("main"),
                new HtmlClass("testClass"),
                new EventAttribute("onSubmit","myFunction()"));

            var doc = HtmlDocument.CreateEmpty("First");

            var table = new TableElement();

            var tbody = table.DescendantsByTag("tbody").FirstOrDefault();

            var thead = table.DescendantsByTag("thead").FirstOrDefault();
            

            Console.WriteLine(doc.ToString());
        }
    }
}
