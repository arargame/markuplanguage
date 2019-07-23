using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html
{
    public class HtmlDocument : MarkupLanguageDocument
    {
        public string Url { get; set; }

        public string CharacterSet { get; set; }

        public string Title { get; set; }

        public List<HtmlElement> Elements
        {
            get
            {
                return Nodes.OfType<HtmlElement>().ToList();
            }
        }

        public static HtmlDocument CreateEmpty(string title)
        {
            var document = new HtmlDocument();

            var head = new HtmlElement("head").AddChildren(new HtmlElement("title").SetInnerText(title));
            var body = new HtmlElement("body");

            document.AddNode(head, body);

            return document;
        }
    }
}
