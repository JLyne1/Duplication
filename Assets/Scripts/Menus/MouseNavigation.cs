using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class MouseNavigation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current != null && !EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject && !EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (GetComponent<Selectable>() != null)
        {
            GetComponent<Selectable>().OnPointerExit(null);
        }
    }
}
