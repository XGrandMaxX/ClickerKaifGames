using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private ClickPopup _popupPrefab;
        [SerializeField] private Transform _parentObject;
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPopupPool(builder);
            RegisterEntryPoint(builder);
        }
        
        private void RegisterEntryPoint(IContainerBuilder builder) => builder.RegisterEntryPoint<EntryPoint>();

        private void RegisterPopupPool(IContainerBuilder builder)
        {
            PopupPool popupPool = new PopupPool(_popupPrefab, _parentObject);
            builder.RegisterInstance(popupPool);
        }
    }
}
