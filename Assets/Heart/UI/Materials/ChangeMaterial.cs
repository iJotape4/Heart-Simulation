using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMaterial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] HeartSpace heartSpace;
    static int count = 0;

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
        UIManager.Instance.selectedHeartSpace = heartSpace;
        UIManager.Instance.CheckPhase();
        if(count==0)
        {
            UIManager.Instance.EnablePhasesData();
            count++;
        }
    }

}
