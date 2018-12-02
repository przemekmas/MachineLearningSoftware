using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Controls
{
    [TemplatePart(Name = "PART_GithubButton", Type = typeof(Button))]
    public class GitHubButtonControl : Control
    {
        static GitHubButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GitHubButtonControl), new FrameworkPropertyMetadata(typeof(GitHubButtonControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var gitHubButton = (Button)Template.FindName("PART_GithubButton", this);
            gitHubButton.Click += new RoutedEventHandler(OnButtonClick);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/przemekmas?tab=repositories");
        }
    }
}
