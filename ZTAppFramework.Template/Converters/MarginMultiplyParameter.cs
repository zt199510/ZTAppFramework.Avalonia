using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Converters
{
    public class MarginMultiplyParameter
    {
        public static MarginMultiplyParameter Default { get; } = new();

        public double LeftMultiplier { get; set; }
        public double TopMultiplier { get; set; }
        public double RightMultiplier { get; set; }
        public double BottomMultiplier { get; set; }
    }
}
