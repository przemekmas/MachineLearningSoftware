using MachineLearningSoftware.Entities;
using System;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Common
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewExportAttribute : ExportAttribute, IResourceItemMetadata
    {
        public string PageName { get; set; }

        public Type DeclaredType { get; private set; }

        public Type ClassType { get; private set; }

        public bool IsPageVisible { get; set; }

        public ViewExportAttribute(Type classType, Type declaredType, string pageName, bool isPageVisible)
            : base(null, typeof(IResourceItemEntity))
        {
            ClassType = classType;
            DeclaredType = declaredType;
            PageName = pageName;
            IsPageVisible = isPageVisible;
        }
    }
}
