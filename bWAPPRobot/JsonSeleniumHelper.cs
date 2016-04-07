using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bWAPPRobot
{
    class JsonSeleniumHelper
    {
        public string Type { get; set; }
        public string seleniumVersion { get; set; }
        public int formatVersion { get; set; }
        public object[] steps { get; set; }
    }
}
