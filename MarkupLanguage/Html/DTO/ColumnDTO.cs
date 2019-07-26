using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.DTO
{
    public class ColumnDTO : MarkupLanguageObject
    {
        public RowDTO Row { get; set; }

        public object Value { get; set; }

        public ColumnDTO(object value)
        {
            Value = value;
        }
    }
}
