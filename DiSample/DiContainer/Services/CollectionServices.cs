namespace DiContainer.Services
{
    public class CollectionServices
    {
        internal readonly ScopeService _scopeService;
        internal readonly SingletonService _singletonService;
        internal readonly TransientService _transientService;

        public CollectionServices(ScopeService scopeService, SingletonService singletonService, TransientService transientService)
        {
            _scopeService=scopeService;
            _singletonService=singletonService;
            _transientService=transientService;
        }
    }
}
