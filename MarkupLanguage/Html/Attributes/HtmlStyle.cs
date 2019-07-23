using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Attributes
{
    /// <summary>
    /// <body style="background-color:powderblue;">
    /// </summary>
    public class HtmlStyle : HtmlAttribute
    {
        public HtmlStyle(string property, string value)
            : base(property, value)
        {
            SetAttributeGroup(HtmlAttributeGroup.Style);
        }

        public override string OuterHtml
        {
            get
            {
                return string.Format("{0}:{1}", Property, Value);
            }
        }
    }
}
