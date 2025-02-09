using Services.Implementations;
using Services.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Services
{
    public class ProdServicesContainer : MonoBehaviour, ISerivceContainer
    {
        [SerializeField] private ConfigsService _configsService;
        
        public void Bind()
        {
            // TODO : Add all services here
            AssignAllServices();
            ServiceLocator.Register<IProgressService>(_configsService);
            // TODO Create instances of static services here as well.
            ServiceLocator.Register<IJourneyService>(new JourneyService());
        }

        private void AssignAllServices()
        {
            if (TryGetComponent(out _configsService) == false)
                _configsService = this.AddComponent<ConfigsService>();
        }
    }
}