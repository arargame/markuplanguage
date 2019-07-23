using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Attributes
{
    /// <summary>
    /// <form onsubmit="myFunction()">
    /// </summary>
    public class EventAttribute : HtmlAttribute
    {
        public EventAttribute(string property,string value) : base(property,value)
        {
            SetAttributeGroup(HtmlAttributeGroup.Event);
        }
    }
}
