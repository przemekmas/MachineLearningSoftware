using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Entities
{
    public interface IResourceItemEntity
    {
        string Name { get; }
        Page Page { get; }
        Control IconControl { get; }
        bool IsVisible { get; }
    }
}
