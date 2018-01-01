using ObjectRecognitionSoftware.Entities;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

namespace ObjectRecognitionSoftware.Views.DialogBoxes
{
    /// <summary>
    /// Interaction logic for LoadingDialogBox.xaml
    /// </summary>
    public partial class LoadingDialogModal : UserControl, IUserModal
    {
        public LoadingDialogModal()
        {
            InitializeComponent();
        }
        
        public void HideModal()
        {
            Visibility = Visibility.Hidden;
            EndAnimation();
        }

        public void ShowModal()
        {
            Visibility = Visibility.Visible;
            StartAnimation();
        }

        public void StartAnimation()
        {
            var storyboard = this.FindResource("StartModalAnimation") as Storyboard;
            Storyboard.SetTarget(storyboard, MainModal);
            storyboard.Begin();
        }

        public void EndAnimation()
        {
            var storyboard = this.FindResource("EndModalAnimation") as Storyboard;
            Storyboard.SetTarget(storyboard, MainModal);
            storyboard.Begin();
        }
    }
}
