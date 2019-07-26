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

        //public object Value { get; set; }

        public List<ColumnDTO> Columns { get; set; }
        
        public RowDTO()
        {
            //Value = value;

            Columns = new List<ColumnDTO>();
        }

        public RowDTO AddColumns(params ColumnDTO[] columns)
        {
            Columns.AddRange(columns);

            foreach (var column in columns)
            {
                column.Row = this;
            }

            return this;
        }
    }
}
