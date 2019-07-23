using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Elements
{
    public class Pagination : HtmlElement
    {
        public int CurrentPage { get; set; }

        public int Offset { get; set; }

        public Pagination(int maxPage, int minPage = 1, int currentPage = 1, int offset = 5, string floatAsStyle = "right")
        {
            SetTagName("ul");

            AddClass("pagination");

            AddStyle("float", floatAsStyle);

            var previousPageLink = new HtmlElement("li").AddClass("page-item")
                                                        .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText("Önceki"));

            AddChildren(previousPageLink);

            if (currentPage > offset - 1)
            {
                var minPageLink = new HtmlElement("li").AddClass("page-item")
                                                            .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText(minPage.ToString()));

                AddChildren(minPageLink);
            }

            if (currentPage > offset - 2)
            {
                var dotsLink = new HtmlElement("li").AddClass("page-item")
                                                            .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText("..."));

                AddChildren(dotsLink);
            }

            var start = currentPage-2;

            var finish = start + offset;

            finish = finish > maxPage ? maxPage : finish;

            finish = maxPage - currentPage < offset ? finish : finish - 3;

            for (var i = start; i <= finish; i++)
            {

                if (minPage > i)
                    continue;
                
                AddChildren(new HtmlElement("li").AddClass("page-item")
                                                            .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText(i.ToString())));
            }

            if (!(maxPage - currentPage < offset))
            {
                AddChildren(new HtmlElement("li").AddClass("page-item")
                                                         .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText("...")));

                AddChildren(new HtmlElement("li").AddClass("page-item")
                                                         .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText(maxPage.ToString())));
            }


            var nextPageLink = new HtmlElement("li").AddClass("page-item")
                                                        .AddChildren(new LinkElement("#").AddClass("page-link").SetInnerText("Sonraki"));

            AddChildren(nextPageLink);
        }
    }
}
