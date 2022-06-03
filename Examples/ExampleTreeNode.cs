using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    internal class ExampleTreeNode : TreeNode
    {
        internal Example example { get; private set; }

        internal ExampleTreeNode(Example example)
        {
            this.example = example;
        }
    }
}
