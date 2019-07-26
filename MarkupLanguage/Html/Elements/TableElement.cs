using MarkupLanguage.Html.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Elements
{
    public class TableElement : HtmlElement
    {
        public TableElement()
        {
            SetTagName("table");

            AddId(Id.ToString());

            AddChildren(new HtmlElement("thead"), new HtmlElement("tbody"));
        }

        public HtmlElement Thead
        {
            get
            {
                return DescendantsByTag("thead").FirstOrDefault();
            }
        }

        public HtmlElement Tbody
        {
            get
            {
                return DescendantsByTag("tbody").FirstOrDefault();
            }
        }

        public List<HtmlElement> TbodyRows
        {
            get
            {
                return Tbody.DescendantsByTag("tr").ToList();
            }
        }

        public static TableElement ObjectToTableElement(List<HeaderDTO> headers, List<RowDTO> rows)
        {
            var table = new TableElement();

            var ths = headers.Select(h => new HtmlElement("th").SetInnerText(h.Name)).ToArray();
            
            table.Thead.AddChildren(new HtmlElement("tr").AddChildren(ths));

            foreach (var row in rows)
            {
                var tr = new HtmlElement("tr");

                table.Tbody.AddChildren(tr);

                foreach (var column in row.Columns)
                {
                    tr.AddChildren(new HtmlElement("td").SetInnerText(column.Value.ToString()));
                }
            }

            return table;
        }

        public static HtmlElement ObjectToAdvancedTableElement(List<HeaderDTO> headers, List<RowDTO> rows,int page=1)
        {
            var table = ObjectToTableElement(headers, rows);

            table.AddClass("table table-striped table-hover");

            table.Thead.DescendantsByTag("th")
                        .ForEach(th => th.AddStyle("cursor", "pointer")
                                            .AddChildren(new HtmlElement("i").AddStyle("float", "right")
                                                                                .AddStyle("color", "grey")
                                                                                .AddStyle("line-height", "normal")
                                                                                .AddStyle("line-height","1.6")
                                                                                .AddClass("fas fa-sort-amount-down")));
            

            var size = 2;

            var recordCount = table.TbodyRows.Count;

            var totalPagesCount = (int)Math.Ceiling((decimal)recordCount / size);

            var pagination = new Pagination(totalPagesCount, page);

            var containerDiv = new HtmlElement("div");

            containerDiv.AddChildren(table, pagination);

            return containerDiv;
        }

        public string Jquery()
        {
            return "$('#" + HtmlId + "').ready(function(){console.log('Alert')});";
        }
    }
}
