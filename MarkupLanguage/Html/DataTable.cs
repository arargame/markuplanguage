using MarkupLanguage.Html.DTO;
using MarkupLanguage.Html.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html
{
    public class DataTableOptions
    {

    }

    public class DataTable : MarkupLanguageObject
    {
        public int PageNumber { get; set; }

        //public Dictionary<string, bool> SortingInfo { get; set; }

        public string SearchText { get; set; }

        public HtmlElement TableDiv { get; set; }

        public DataTable(int pageNumber, string searchText, TableDTO tableDTO)
        {
            TableDiv = TableElement.ObjectToAdvancedTableElement(tableDTO.Headers, tableDTO.Rows);
        }
    }
}
