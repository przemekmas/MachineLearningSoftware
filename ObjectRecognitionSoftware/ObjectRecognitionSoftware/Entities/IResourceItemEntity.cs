using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ObjectRecognitionSoftware.Entities
{
    public interface IResourceItemEntity
    {
        string Name { get; }
        Page Page { get; }
        Control IconControl { get; }
    }
}
