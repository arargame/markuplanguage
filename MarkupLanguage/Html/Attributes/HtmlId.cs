using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Attributes
{
    /// <summary>
    /// <h2 id="C4">Chapter 4</h2>
    /// </summary>
    public class HtmlId : HtmlAttribute
    {
        public HtmlId(string value)
            : base("id", value)
        {
            SetAttributeGroup(HtmlAttributeGroup.Id);
        }
    }
}
