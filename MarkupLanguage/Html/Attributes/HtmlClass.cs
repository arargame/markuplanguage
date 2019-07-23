using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Attributes
{
    /// <summary>
    /// <h2 class="city main">London</h2>
    /// </summary>
    public class HtmlClass : HtmlAttribute
    {
        public HtmlClass(string property)
            : base(property, null, HtmlAttributeType.InEquality)
        {
            SetAttributeGroup(HtmlAttributeGroup.Class);
        }

    }
}
