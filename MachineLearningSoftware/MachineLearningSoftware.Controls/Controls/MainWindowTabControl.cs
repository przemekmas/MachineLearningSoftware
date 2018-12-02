using System;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Controls
{
    [TemplatePart(Name = "PART_CustomTabControl", Type = typeof(TabControl))]
    public class MainWindowTabControl : Control
    {
        private TabControl _tabControl;

        public static readonly DependencyProperty AddPageProperty =
           DependencyProperty.Register(nameof(AddPage), typeof(Action<object>), typeof(MainWindowTabControl), new UIPropertyMetadata(null));

        public Action<object> AddPage
        {
            get { return (Action<object>)GetValue(AddPageProperty); }
            set { SetValue(AddPageProperty, value); }
        }

        static MainWindowTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindowTabControl), new FrameworkPropertyMetadata(typeof(MainWindowTabControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AddPage = AddPageTabControl;
            _tabControl = (TabControl)Template.FindName("PART_CustomTabControl", this);
        }

        private void AddPageTabControl(object page)
        {
            Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                _tabControl.Items.Add(page);
                _tabControl.SelectedItem = page;
            }));
        }
    }
}
