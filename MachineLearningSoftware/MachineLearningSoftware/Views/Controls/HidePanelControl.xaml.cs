using MachineLearningSoftware.Common;
using MachineLearningSoftware.Views.Controls.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for HidePanelControl.xaml
    /// </summary>
    public partial class HidePanelControl : StackPanel
    {
        public static readonly DependencyProperty ContentPlaceholderProperty =
            DependencyProperty.Register(nameof(ContentPlaceholder), typeof(object), typeof(HidePanelControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(object), typeof(HidePanelControl), new UIPropertyMetadata(null));

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

        public HidePanelControl()
        {
            InitializeComponent();
            DataContext = DependencyInjection.ResolveSingle<HidePanelViewModel>();
        }
    }
}
