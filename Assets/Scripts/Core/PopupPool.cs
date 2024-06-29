using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Core
{
    public class PopupPool
    {
        private const int POPUP_PRELOAD_COUNT = 15;
        
        private ClickPopup _popupPrefab;
        private PoolBase<ClickPopup> _popupPool;

        private Transform _parentObject;
        
        [Inject] 
        public PopupPool(ClickPopup popupPrefab, Transform parentObject)
        {
            _popupPrefab = popupPrefab;

            _parentObject = parentObject;
            
            _popupPool = new PoolBase<ClickPopup>(
                Preload,
                GetAction,
                ReturnAction,
                POPUP_PRELOAD_COUNT);
        }
        
        
        public void ShowPopup(Vector3 position)
        {
            ClickPopup popup = _popupPool.Get();

            popup._valueText.ResetComponent();

            //popup._valueText.text = ClickCounter.clicks.ToString();
            
            popup.transform.DOMoveY(position.y + 1f, 1f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => _popupPool.Return(popup));
        }
      
        
        private ClickPopup Preload() => Object.Instantiate(_popupPrefab, _parentObject.transform);

        private void GetAction(ClickPopup popup)
        {
            popup.gameObject.SetActive(true);
           //popup.transform.position = _parentObject.position;
        }

        private void ReturnAction(ClickPopup popup) => popup.gameObject.SetActive(false);
    }
}
