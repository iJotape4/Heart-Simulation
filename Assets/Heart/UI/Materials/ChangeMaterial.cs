using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMaterial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Material highlightedMaterial;
    public Material defaultMaterial;

    Renderer rend;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = defaultMaterial;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rend.material = highlightedMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rend.material = defaultMaterial;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.title.text = name;
    }

}
