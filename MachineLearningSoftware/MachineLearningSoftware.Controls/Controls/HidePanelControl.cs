using MachineLearningSoftware.Controls.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Controls
{
    public class HidePanelControl : Control
    {
        public static readonly DependencyProperty ContentPlaceholderProperty =
            DependencyProperty.Register(nameof(ContentPlaceholder), typeof(object), typeof(HidePanelControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(object), typeof(HidePanelControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsPanelVisibleProperty =
            DependencyProperty.Register(nameof(IsPanelVisible), typeof(bool), typeof(HidePanelControl), new UIPropertyMetadata(null));
        
        public object ContentPlaceholder
        {
            get { return GetValue(ContentPlaceholderProperty); }
            set { SetValue(ContentPlaceholderProperty, value); }
        }

        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public bool IsPanelVisible
        {
            get { return (bool)GetValue(IsPanelVisibleProperty); }
            set { SetValue(IsPanelVisibleProperty, value); }
        }

        public ICommand ShowHidePanelCommand
        {
            get { return new CommandDelegate(ShowHidePanel, (_) => true); }
        }

        private void ShowHidePanel(object parameter)
        {
            IsPanelVisible = !IsPanelVisible;
        }

        static HidePanelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HidePanelControl), new FrameworkPropertyMetadata(typeof(HidePanelControl)));
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            IsPanelVisible = true;
        }
    }
}
