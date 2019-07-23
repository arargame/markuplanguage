using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.DTO
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }


    public class HeaderDTO : MarkupLanguageObject
    {
        public object Value { get; set; }

        public TableDTO Table { get; set; }

        public int Index { get; set; }

        public bool IsSelectable { get; set; }

        public bool IsSortable { get; set; }

        public bool IsSearchable { get; set; }

        public bool IsFilter { get; set; }

        public bool Visible { get; set; }

        public bool IsSorting { get; set; }

        public SortDirection SortDirection { get; set; }

        public HeaderDTO()
        {
            IsSelectable = IsSortable = IsSearchable = Visible = true;
            SortDirection = SortDirection.Descending;        
        }

        public HeaderDTO(string name, bool isSorting = false, SortDirection sortDirection = DTO.SortDirection.Ascending)
        {
            Name = name;
            IsSorting = isSorting;
            SortDirection = sortDirection;
        }
    }
}
