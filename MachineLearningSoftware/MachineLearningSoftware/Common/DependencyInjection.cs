using System.ComponentModel.Composition.Hosting;

namespace MachineLearningSoftware.Common
{
    public static class DependencyInjection
    {
        private static CompositionContainer _container;

        public static CompositionContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new CompositionContainer(new DirectoryCatalog(".", "MachineLearningSoftware.*"));
                }

                return _container;
            }
        }

        public static T ResolveSingle<T>()
        {
            return Container.GetExport<T>().Value;
        }
    }
}
