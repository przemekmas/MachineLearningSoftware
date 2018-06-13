using System.Windows.Controls;

namespace MachineLearningSoftware.Entities
{
    public interface IResourceItemEntity
    {
        Page Page { get; }
        Control IconControl { get; }
    }
}
