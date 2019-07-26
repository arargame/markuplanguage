using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage.Html.Attributes
{
    //public interface IHtmlAttribute
    //{
    //    HtmlElement Element { get; set; }

    //    string Property { get; set; }

    //    string Value { get; set; }

    //    string OuterHtml { get; }
    
    //    IHtmlAttribute SetElement(HtmlElement element);
    //}

    public enum HtmlAttributeType
    {
        Equality,
        InEquality
    }

    public enum HtmlAttributeGroup
    {
        Normal,
        Id,
        Name,
        Event,
        Style,
        Class
    }

    /// <summary>
    /// <tagname style="property:value;">
    /// sample : <button type="button" disabled>Click Me!</button>
    /// </summary>
    public class HtmlAttribute : MarkupLanguageObject
    {
        public HtmlElement Element { get; set; }

        public HtmlAttributeType Type { get; set; }

        public HtmlAttributeGroup Group { get; set; }

        public string Property { get; set; }

        public string Value { get; set; }

        public HtmlAttribute(string property, string value = null, HtmlAttributeType type = HtmlAttributeType.Equality, HtmlAttributeGroup group = HtmlAttributeGroup.Normal)
        {
            SetProperty(property);

            SetValue(value);

            SetAttributeType(type);

            SetAttributeGroup(group);
        }

        public HtmlAttribute SetProperty(string property)
        {
            Property = property;

            return this;
        }

        public HtmlAttribute SetValue(string value)
        {
            Value = value;

            return this;
        }

        public HtmlAttribute SetElement(HtmlElement element)
        {
            Element = element;

            return this;
        }

        public HtmlAttribute SetAttributeType(HtmlAttributeType type)
        {
            Type = type;

            return this;
        }

        public HtmlAttribute SetAttributeGroup(HtmlAttributeGroup group)
        {
            Group = group;

            return this;
        }

        public virtual string OuterHtml
        {
            get
            {
                return Type == HtmlAttributeType.Equality ? string.Format(" {0}='{1}'", Property, Value) : string.Format("{0}", Property);
            }
        }

        public new T To<T>() where T : HtmlAttribute
        {
            return (T)this;
        }
    }
}
