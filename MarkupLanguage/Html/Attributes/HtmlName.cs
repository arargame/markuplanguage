using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Attributes
{
    /// <summary>
    /// <button name="subject" type="submit" value="CSS">CSS</button>
    /// </summary>
    public class HtmlName : HtmlAttribute
    {
        public HtmlName(string value)
            : base("name", value)
        {
            SetAttributeGroup(HtmlAttributeGroup.Name);
        }
    }
}
