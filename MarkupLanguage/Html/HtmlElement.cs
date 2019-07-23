using MarkupLanguage.Html.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html
{
    public class HtmlElement : MarkupLanguageNode
    {
        public string HtmlId 
        {
            get
            {
                return IdAttribute != null ? IdAttribute.Value : null; 
            }
        }

        public string HtmlName
        {
            get
            {
                return NameAttribute != null ? NameAttribute.Value : null;
            }
        }


        [JsonIgnore]
        public List<HtmlAttribute> Attributes { get; set; }

        [JsonIgnore]
        public List<HtmlAttribute> NormalAttributes 
        {
            get
            {
                return Attributes.Where(a => a.Group == HtmlAttributeGroup.Normal).ToList();
            }
        }

        [JsonIgnore]
        public HtmlAttribute IdAttribute
        {
            get
            {
                return Attributes.OfType<HtmlId>().FirstOrDefault();
            }
        }

        [JsonIgnore]
        public HtmlAttribute NameAttribute
        {
            get
            {
                return Attributes.OfType<HtmlName>().FirstOrDefault();
            }
        }

        [JsonIgnore]
        public List<EventAttribute> Events 
        {
            get
            {
                return Attributes.OfType<EventAttribute>().ToList();
            }
        }

        [JsonIgnore]
        public List<HtmlStyle> Styles
        {
            get
            {
                return Attributes.OfType<HtmlStyle>().ToList();
            }
        }

        [JsonIgnore]
        public List<HtmlClass> Classes
        {
            get
            {
                return Attributes.OfType<HtmlClass>().ToList();
            }
        }

        public string OuterHtmlOfStyles
        {
            get
            {
                var result = string.Join(";", Styles.Select(s => s.OuterHtml));

                return !string.IsNullOrWhiteSpace(result) ? string.Format(" style='{0}'", result) : null;
            }
        }

        public string OuterHtmlOfNormalAttributes
        {
            get
            {
                var result = string.Join(" ", NormalAttributes.Select(a => a.OuterHtml));

                return !string.IsNullOrWhiteSpace(result) ? string.Format(" {0}", result) : null;
            }
        }


        public string OuterHtmlOfClasses
        {
            get
            {
                var result = string.Join(" ", Classes.Select(s => s.OuterHtml));

                return !string.IsNullOrWhiteSpace(result) ? string.Format(" class='{0}'", result) : null;
            }
        }

        public string OuterHtmlOfEvents
        {
            get
            {
                var result = string.Join(" ", Events.Select(e => e.OuterHtml));

                return !string.IsNullOrWhiteSpace(result) ? string.Format(" {0}", result) : null;
            }
        }

        public string OuterHtmlOfId
        {
            get
            {
                return IdAttribute != null ? string.Format(" {0}", IdAttribute.OuterHtml) : null;
            }
        }


        public override string OuterHtml
        {
            get
            {
                var result = "";

                switch (Type)
                {
                    case TagType.OpenThenClose:
                        result = string.Format("<{0}{1}{2}{3}{4}{5}>{6}{7}</{0}>", TagName, OuterHtmlOfNormalAttributes, OuterHtmlOfId, OuterHtmlOfClasses, OuterHtmlOfStyles, OuterHtmlOfEvents, OwnInnerText, InnerHtml);
                        break;

                    case TagType.WithoutClosing:
                        result = string.Format("<{0}{1}{2}{3}{4}{5}/>", TagName, OuterHtmlOfNormalAttributes, OuterHtmlOfId, OuterHtmlOfClasses, OuterHtmlOfStyles, OuterHtmlOfEvents);
                        break;
                }

                return result;
            }
        }

        public override string InnerHtml
        {
            get
            {
                return Type == TagType.OpenThenClose ? string.Join("", Child.Select(c => c.OuterHtml)) : null;
            }
        }

        public HtmlElement(string tagName = null)
        {
            Attributes = new List<HtmlAttribute>();

            SetTagName(tagName);

            //ChildChangedEvent += ResetSibling;
            ChildChangedAction = ResetSibling;
        }


        public HtmlElement AddAttributes(params HtmlAttribute[] attributes)
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i] is HtmlId)
                {
                    if (!Attributes.Any(a => a.Property.ToUpper() == "id".ToUpper()))
                        Attributes.Add(new HtmlId(attributes[i].Value));
                }
                else
                    Attributes.Add(attributes[i]);
            }

            Attributes.ForEach(a => a.SetElement(this));

            return this;
        }

        public HtmlElement RemoveAttribute(Predicate<HtmlAttribute> match)
        {
            Attributes.RemoveAll(match);

            return this;
        }

        public HtmlElement AddClass(string classToAdd)
        {
            if (classToAdd.Contains(" "))
            {
                classToAdd.Split(' ').ToList().ForEach(c => AddClass(c));

                return this;
            }

            AddAttributes(new HtmlClass(classToAdd));

            return this;
        }

        public HtmlElement RemoveClass(string classToRemove)
        {
            Classes.RemoveAll(c => c.Property == classToRemove);

            return this;
        }

        public HtmlElement AddStyle(string property,string value)
        {
            AddAttributes(new HtmlStyle(property, value));

            return this;
        }

        public HtmlElement RemoveStyle(string styleToRemove)
        {
            Styles.RemoveAll(s => s.Property == styleToRemove);

            return this;
        }

        public HtmlElement AddId(string id)
        {
            if (IdAttribute != null)
                IdAttribute.Value = id;
            else
                Attributes.Add(new HtmlId(id));

            return this;
        }

        public HtmlElement AddEventAttribute(string property,string value)
        {
            AddAttributes(new EventAttribute(property, value));

            return this;
        }

        public HtmlElement RemoveEventAttribute(string eventToRemove)
        {
            Events.RemoveAll(e => e.Property == eventToRemove);

            return this;
        }

        public List<HtmlElement> DescendantsByTag(string tagName)
        {
            var list = new List<HtmlElement>();

            if (TagName == tagName)
                list.Add(this);

            foreach (var item in Child.OfType<HtmlElement>())
	        {
                list.AddRange(item.DescendantsByTag(tagName));
	        }

            return list;
        }

        public List<HtmlElement> DescendantsByName(string name)
        {
            var list = new List<HtmlElement>();

            if (HtmlName == name)
                list.Add(this);

            foreach (var item in Child.OfType<HtmlElement>())
            {
                list.AddRange(item.DescendantsByName(name));
            }

            return list;
        }


        public List<HtmlElement> DescendantsById(string id)
        {
            var list = new List<HtmlElement>();

            if (HtmlId == id)
                list.Add(this);

            foreach (var item in Child.OfType<HtmlElement>())
            {
                list.AddRange(item.DescendantsById(id));
            }

            return list;
        }

        [JsonIgnore]
        public List<HtmlElement> Siblings
        {
            get
            {
                return Parent != null ? Parent.Child.OfType<HtmlElement>().Except(new[] { this }).ToList() : new List<HtmlElement>();
            }
        }

        private void ResetSibling()
        {
            previousSibling = null;
            nextSibling = null;
        }

        HtmlElement previousSibling;

        public HtmlElement PreviousSibling 
        {
            get 
            {
                if (previousSibling != null)
                    return previousSibling;

                if (Parent == null)
                    return null;

                var parentChild = Parent.Child.OfType<HtmlElement>().ToList();

                for (int i = 0; i < parentChild.Count; i++)
                {
                    if (this.Id == parentChild[i].Id)
                        previousSibling = i > 0 ? parentChild[i - 1] : null;
                }

                return previousSibling;
            } 
        }

        HtmlElement nextSibling;
        public HtmlElement NextSibling
        {
            get
            {
                if (nextSibling != null)
                    return nextSibling;

                if (Parent == null)
                    return null;

                var parentChild = Parent.Child.OfType<HtmlElement>().ToList();

                for (int i = 0; i < parentChild.Count; i++)
                {
                    if (this.Id == parentChild[i].Id)
                        nextSibling = (i + 1) < parentChild.Count ? parentChild[i + 1] : null;
                }

                return nextSibling;
            }
        }

    }
}
