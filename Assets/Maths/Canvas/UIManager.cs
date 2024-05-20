using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SinglentonParent<UIManager>
{
    [SerializeField] public TextMeshProUGUI title;

    [Header("Entrys")]
    public Slider age;
    public Slider height;
    public Slider weight;
    public Slider FCM;
    public Toggle cardiacIllnessHistory;

    [Header("Alerts")]
    public GameObject alertElder;
    public GameObject alertObese;
    public GameObject alertCardiacIllness;

    // Start is called before the first frame update
    void Start()
    {
        age.onValueChanged.AddListener(delegate { CheckElderAlert(); });
        weight.onValueChanged.AddListener(delegate { CheckObeseAlert(); });
        cardiacIllnessHistory.onValueChanged.AddListener(delegate { CheckCardiacIllnessAlert(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CheckElderAlert()
    {
        if (UtilFormulas.isElder((int)age.value))
        {
            ShowAlert(alertElder, true);
        }
        else
        {
            ShowAlert(alertElder, false);
        }
    }
    void CheckObeseAlert()
    {
        if (UtilFormulas.IsObese(weight.value, height.value))
        {
            ShowAlert(alertObese, true);
        }
        else
        {
            ShowAlert(alertObese, false);
        }
    }

    void CheckCardiacIllnessAlert()
    {
        if (cardiacIllnessHistory.isOn)
        {
            ShowAlert(alertCardiacIllness, true);
        }else
        {
            ShowAlert(alertCardiacIllness, false);
        }
    }

    void ShowAlert(GameObject alert, bool enable)
    {
        alert.SetActive(enable);
    }
}
