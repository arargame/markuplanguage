using MarkupLanguage.Html.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Elements
{
    public class LinkElement : HtmlElement
    {
        public LinkElement(string href)
        {
            SetTagName("a");

            AddAttributes(new HtmlAttribute("href",href));
        }
    }
}
