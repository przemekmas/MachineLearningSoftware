using System.Windows.Controls;

namespace MachineLearningSoftware.Entities
{
    public interface IResourceItemEntity
    {
        string Name { get; }
        Page Page { get; }
        Control IconControl { get; }
        bool IsVisible { get; }
    }
}
