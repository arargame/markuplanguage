using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.DTO
{
    public class RowDTO : MarkupLanguageObject
    {
        public TableDTO Table { get; set; }

        public object Value { get; set; }

        public RowDTO(object value)
        {
            Value = value;
        }
    }
}
