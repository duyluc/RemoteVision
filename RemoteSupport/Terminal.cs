using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSupport
{
    [Serializable()]
    public class Terminal
    {
        public object Value { get; set; }
        public string Name { get; set; }
        public Terminal(string Name)
        {
            this.Name = Name;
        }

        public void SetValue<T>(T value)
        {
            this.Value = value;
        }
    }
}
