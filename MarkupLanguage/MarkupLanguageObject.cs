using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupLanguage
{
    public abstract class MarkupLanguageObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public MarkupLanguageObject(string name=null,string description=null)
        {
            Id = Guid.NewGuid();

            SetName(name);

            SetDescription(description);
        }

        public MarkupLanguageObject SetName(string name)
        {
            Name = name;

            return this;
        }

        public MarkupLanguageObject SetDescription(string description)
        {
            Description = description;

            return this;
        }

        public T To<T>() where T : MarkupLanguageObject
        {
            return (T)this;
        }

        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return json;
        }
    }
}
