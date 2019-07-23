using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage
{
    //public interface IMarkupLanguageNode
    //{
    //    string OuterHtml { get; }

    //    string TagName { get; set; }
    //    string Value { get; set; }
    //    TagType Type { get; set; }
    //    IMarkupLanguageNode Parent { get; set; }
    //    List<IMarkupLanguageNode> Child { get; set; }
    //}

    public enum TagType
    {
        OpenThenClose,
        WithoutClosing
    }

    public enum InnerTextDirection
    {
        Left,
        Right
    }

    public class MarkupLanguageNode : MarkupLanguageObject
    {
        [JsonIgnore]
        public MarkupLanguageDocument OwnerDocument { get; set; }

        public string TagName { get; set; }

        public string Value { get; set; }

        public virtual string InnerHtml 
        {
            get
            {
                return null;
            }
        }

        public string OwnInnerText { get; set; }

        public string InnerText
        {
            get
            {
                return string.Format(InnerTextDirection == InnerTextDirection.Left ? "{0}{1}" : "{1}{0}", (!string.IsNullOrWhiteSpace(OwnInnerText) ? OwnInnerText.Trim() : null), string.Join(" ", Child.Select(c => c.InnerText.Trim()))).Trim();
            }
        }

        //public string OuterText { get; set; }

        public virtual string OuterHtml
        {
            get
            {
                return null;
            }
        }

        public TagType Type { get; set; }
        public InnerTextDirection InnerTextDirection { get; set; }

        public MarkupLanguageNode Parent { get; set; }

        //protected delegate void EventHandler();
        //protected event EventHandler ChildChangedEvent;
        [JsonIgnore]
        public Action ChildChangedAction { get; set; }

        public List<MarkupLanguageNode> Child { get; set; }

        public MarkupLanguageNode(TagType type = TagType.OpenThenClose,InnerTextDirection innerTextDirection = InnerTextDirection.Left)
        {
            Child = new List<MarkupLanguageNode>();

            SetType(type);

            SetInnerTextDirection(innerTextDirection);

            ChildChangedAction = new Action(() => { });
        }

        public MarkupLanguageNode SetType(TagType type)
        {
            Type = type;

            return this;
        }

        public MarkupLanguageNode SetParent(MarkupLanguageNode parent) 
        {
            Parent = parent;

            return this;
        }

        public MarkupLanguageNode AddChildren(params MarkupLanguageNode[] childrens) 
        {
            Child.AddRange(childrens);

            childrens.ToList().ForEach(c => c.SetParent(this));

            ChildChangedAction.Invoke();

            return this;
        }

        public MarkupLanguageNode RemoveChildren(Predicate<MarkupLanguageNode> match)
        {
            Child.RemoveAll(match);

            ChildChangedAction.Invoke();

            return this;
        }

        public MarkupLanguageNode SetTagName(string tagName)
        {
            TagName = tagName;

            return this;
        }

        public MarkupLanguageNode SetInnerText(string innerText)
        {
            OwnInnerText = innerText;

            return this;
        }

        public MarkupLanguageNode SetInnerTextDirection(InnerTextDirection direction)
        {
            InnerTextDirection = direction;

            return this;
        }


        public override string ToString()
        {
            return OuterHtml;
        }

        public new T To<T>() where T : MarkupLanguageNode
        {
            return (T)this;
        }
    }
}
