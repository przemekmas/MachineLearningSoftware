using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRecognitionSoftware.ViewModels
{
    class MainWindowViewModel
    {
        public ICollection<IResourceItemEntity> ResourceItems { get; }
        public MainWindowViewModel()
        {
            ResourceItems = new ObservableCollection<IResourceItemEntity>();
        }
    }
}
