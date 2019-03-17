using MachineLearningSoftware.Entities;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

namespace MachineLearningSoftware.Views.DialogBoxes
{
    /// <summary>
    /// Interaction logic for LoadingProgressDialogModal.xaml
    /// </summary>
    public partial class LoadingProgressDialogModal : UserControl, IUserModal
    {
        public LoadingProgressDialogModal()
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
            if (FindResource("StartModalAnimation") is Storyboard storyboard)
            {
                Storyboard.SetTarget(storyboard, MainModal);
                storyboard.Begin();
            }            
        }

        public void EndAnimation()
        {
            if (FindResource("EndModalAnimation") is Storyboard storyboard)
            {
                Storyboard.SetTarget(storyboard, MainModal);
                storyboard.Begin();
            }            
        }
    }
}
