using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{
    public interface IBehavior
    {
        void RegisterBehavior();

        void ReleaseBehavior();
    }
}
