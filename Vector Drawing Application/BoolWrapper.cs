using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Drawing_Application
{
    public class BoolWrapper
    {
        public BoolWrapper()
        {

        }
        public bool Value { get; set; }
        public BoolWrapper(bool value) { this.Value = value; }
    }

}