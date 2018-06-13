using System;

namespace MachineLearningSoftware.Common
{
    public interface IResourceItemMetadata
    {
        Type ClassType { get; }
        Type DeclaredType { get; }
        string PageName { get; }
        bool IsPageVisible { get; }
    }
}
