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
        private Camera _cameraMain;
        
        [Inject] 
        public PopupPool(ClickPopup popupPrefab, Transform parentObject)
        {
            _popupPrefab = popupPrefab;

            _parentObject = parentObject;
            
            _cameraMain = Camera.main;
            
            _popupPool = new PoolBase<ClickPopup>(
                Preload,
                GetAction,
                ReturnAction,
                POPUP_PRELOAD_COUNT);
        }
        
        
        public void ShowPopup(Vector3 position)
        {
            ClickPopup popup = _popupPool.Get();
            
            //popup._valueText.text = ClickCounter.clicks.ToString();
            
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                popup._valueText.rectTransform,
                position,
                _cameraMain,
                out Vector3 worldPoint
            );
            popup.transform.position = worldPoint;
            
            AnimatePopup(popup, position);
        }

        private void AnimatePopup(ClickPopup popup, Vector3 position)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(popup.transform.DOLocalMoveY(position.y + 1f, popup._lifeTime)
                .SetEase(Ease.OutQuad));
            
            sequence.Join(popup._valueText.DOFade(0, popup._lifeTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    popup._valueText.DOFade(1, 0);
                    _popupPool.Return(popup);
                })); 
            
            sequence.Play();
        }


        #region PoolBaseMethods
        
        private ClickPopup Preload() => Object.Instantiate(_popupPrefab, _parentObject.transform);
        private void GetAction(ClickPopup popup) => popup.gameObject.SetActive(true);
        private void ReturnAction(ClickPopup popup) => popup.gameObject.SetActive(false);

        #endregion
    }
}
