using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Views.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Views
{
    /// <summary>
    /// Interaction logic for TensorBoard.xaml
    /// </summary>
    [ViewExport(typeof(TensorBoard), typeof(IResourceItemEntity), "TensorBoard", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TensorBoard : UserControl, IResourceItemEntity
    {
        private readonly TensorBoardViewModel _tensorBoardViewModel;

        public UserControl Page => this;

        public Control IconControl => new DefaultIcon();

        [ImportingConstructor]
        public TensorBoard(TensorBoardViewModel tensorBoardViewModel)
        {
            InitializeComponent();
            DataContext = tensorBoardViewModel;
            _tensorBoardViewModel = tensorBoardViewModel;
        }
    }
}
