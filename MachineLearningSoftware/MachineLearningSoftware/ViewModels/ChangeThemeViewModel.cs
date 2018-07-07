using System.ComponentModel.Composition;

namespace MachineLearningSoftware.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChangeThemeViewModel : BaseViewModel
    {
        public ChangeThemeViewModel()
        {
            ConfigureHeaderControl(true, true, Properties.ChangeThemeResource.Title);
        }
    }
}
