using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{
    public abstract class Behavior<T> : IBehavior
    {
        public T AssociatedObject { get; set; }

        public Behavior(T t)
        {
            AssociatedObject = t;
        }

        protected abstract void OnAttached();

        protected abstract void OnDetaching();

        public void RegisterBehavior()
        {
            this.OnAttached();
        }

        public void ReleaseBehavior()
        {
            this.OnDetaching();
        }
    }
}
