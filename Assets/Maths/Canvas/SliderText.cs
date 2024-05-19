using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = slider.value.ToString();
        slider.onValueChanged.AddListener(delegate { text.text = slider.value.ToString(); });
    }
}
