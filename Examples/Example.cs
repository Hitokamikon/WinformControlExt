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

        internal List<string> namespaceList { get; private set; }
        
        internal List<string> codeList { get; private set; }

        internal string description { get; private set; }

        public Example(string path , List<string> namespaceList , List<string> codeList , string description)
        {
            this.path = path;
            this.namespaceList = namespaceList;
            this.codeList = codeList;
            this.description = description;
        }
    }
}
