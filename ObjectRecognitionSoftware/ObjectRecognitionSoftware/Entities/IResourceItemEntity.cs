using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Entities
{
    public interface IResourceItemEntity
    {
        string Name { get; }
        Page Page { get; }
    }
}
