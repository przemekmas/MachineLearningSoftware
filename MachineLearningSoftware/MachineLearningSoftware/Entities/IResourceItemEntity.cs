using System.Windows.Controls;

namespace MachineLearningSoftware.Entities
{
    public interface IResourceItemEntity
    {
        UserControl Page { get; }
        Control IconControl { get; }
    }
}
