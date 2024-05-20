using UnityEngine;
using UnityEngine.UI;

public class FCMSlider : MonoBehaviour
{
    Slider age;
    Slider FCM;
    // Start is called before the first frame update
    void Start()
    {
        age = UIManager.Instance.age;
        FCM = GetComponent<Slider>();
        age.onValueChanged.AddListener(delegate { FCM.maxValue = (220 - age.value); });
    }
}