using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Controls
{
    public class WindowHeaderControl : Control
    {
        static WindowHeaderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowHeaderControl), new FrameworkPropertyMetadata(typeof(WindowHeaderControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
