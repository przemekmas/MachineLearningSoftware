using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Controls
{
    public class MainMenuButtonControl : Control
    {
        public static readonly DependencyProperty ContentPlaceholderProperty =
           DependencyProperty.Register(nameof(ContentPlaceholder), typeof(object), typeof(MainMenuButtonControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PageTitleProperty =
           DependencyProperty.Register(nameof(PageTitle), typeof(object), typeof(MainMenuButtonControl), new UIPropertyMetadata(null));

        public object ContentPlaceholder
        {
            get { return GetValue(ContentPlaceholderProperty); }
            set { SetValue(ContentPlaceholderProperty, value); }
        }

        public object PageTitle
        {
            get { return GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        static MainMenuButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainMenuButtonControl), new FrameworkPropertyMetadata(typeof(MainMenuButtonControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
