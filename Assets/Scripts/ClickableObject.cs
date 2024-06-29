using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class ClickableObject : MonoBehaviour , IPointerClickHandler
{
    private PopupPool _popupPool;

    [Inject]
    private void Construct(PopupPool popupPool) => _popupPool = popupPool;
    
    public void OnPointerClick(PointerEventData eventData) => _popupPool.ShowPopup(Input.GetTouch(0).position);
}
