using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.DTO
{
    public class TableDTO : MarkupLanguageObject
    {
        public List<HeaderDTO> Headers { get; set; }

        public List<RowDTO> Rows { get; set; }

        public TableDTO(string name, Guid id)
        {
            Name = name;

            Id = id;

            Headers = new List<HeaderDTO>();

            Rows = new List<RowDTO>();
        }

        public TableDTO AddHeaders(params HeaderDTO[] headers)
        {
            Headers.AddRange(headers);

            foreach (var header in headers)
            {
                header.Table = this;
            }

            return this;
        }

        public TableDTO AddRows(params RowDTO[] rows)
        {
            Rows.AddRange(rows);

            foreach (var row in rows)
            {
                row.Table = this;
            }

            return this;
        }
    }
}
