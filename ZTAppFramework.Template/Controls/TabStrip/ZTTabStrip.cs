using Avalonia;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Enums;

namespace ZTAppFramework.Template.Controls
{
    public class ZTTabStrip:TabStrip
    {

        /// <summary>
        /// Defines the <see cref="TabStripType"/> property.
        /// </summary>
        public static readonly StyledProperty<TabStripEnums> TabStripTypeProperty =
            AvaloniaProperty.Register<ZTTabStrip, TabStripEnums>(nameof(TabStripType), TabStripEnums.Default);

        /// <summary>
        /// Comment
        /// </summary>
        public TabStripEnums TabStripType
        {
            get { return GetValue(TabStripTypeProperty); }
            set { SetValue(TabStripTypeProperty, value); }
        }

        

    }
}
