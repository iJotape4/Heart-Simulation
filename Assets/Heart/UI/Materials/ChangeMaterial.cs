using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMaterial : MonoBehaviour, IPointerEnterHandler  , IPointerExitHandler
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
}
