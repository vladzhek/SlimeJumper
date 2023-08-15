using Script.Gameplay.Services;

namespace Script.Gameplay.Services
{
    public class InjectService
    {
        private static InjectService _instance;
        public static InjectService Instance => _instance ??= new InjectService();
        
        private ServiceInstaller _serviceInstaller;

        public void Inject(object obj)
        {
            _serviceInstaller.Inject(obj);
        }

        public void SetContainer(ServiceInstaller serviceInstaller)
        {
            _serviceInstaller = serviceInstaller;
        }
    }
}