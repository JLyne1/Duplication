using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    private EventSystem eventSystem;
    private Selectable selectedElement;

    [SerializeField] private Selectable initialSelectedElement;

    private void Start()
    {
        eventSystem = EventSystem.current;
        selectedElement = initialSelectedElement;
        SetSelectedElement(selectedElement);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNextElement(selectedElement.FindSelectableOnDown());
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectNextElement(selectedElement.FindSelectableOnUp());
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNextElement(selectedElement.FindSelectableOnRight());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectNextElement(selectedElement.FindSelectableOnLeft());
        }
    }

    private void SetSelectedElement(Selectable element)
    {
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(element.gameObject);
            selectedElement = element;
        }
    }

    private void SelectNextElement(Selectable nextElement)
    {
        if (eventSystem!= null && nextElement != null)
        {
            selectedElement = nextElement;
            eventSystem.SetSelectedGameObject(nextElement.gameObject);
        }
    }
}
