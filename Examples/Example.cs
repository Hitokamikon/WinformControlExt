using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    internal class Example
    {
        internal string path { get; private set; }

        internal string code { get; private set; }

        public Example(string path , string code)
        {
            this.path = path;
            this.code = code;
        }
    }
}
