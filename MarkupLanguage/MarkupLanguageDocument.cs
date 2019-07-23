using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage
{
    public abstract class MarkupLanguageDocument : MarkupLanguageObject
    {
        public string Text { get; set; }

        public List<MarkupLanguageNode> Nodes { get; set; }

        public MarkupLanguageDocument(string text = null)
        {
            Nodes = new List<MarkupLanguageNode>();

            Load(text);
        }

        protected MarkupLanguageDocument AddNode(params MarkupLanguageNode[] nodes)
        {
            Nodes.AddRange(nodes);

            return this;
        }

        protected MarkupLanguageDocument RemoveNode(Predicate<MarkupLanguageNode> match)
        {
            Nodes.RemoveAll(match);

            return this;
        }

        public override string ToString()
        {
            return string.Join("", Nodes.Select(n => n.OuterHtml));
        }

        public MarkupLanguageDocument Load(string text)
        {
            Text = text;

            return this;
        }

        public MarkupLanguageDocument LoadFromFile()
        {
            return this;
        }
    }
}
