using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Controls
{
    public class InformationButtonControl : Button
    {
        static InformationButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InformationButtonControl), new FrameworkPropertyMetadata(typeof(InformationButtonControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
