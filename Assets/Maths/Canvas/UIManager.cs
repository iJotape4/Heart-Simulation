using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SinglentonParent<UIManager>
{
    [SerializeField] public TextMeshProUGUI title;
    public HeartSpace selectedHeartSpace;

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

    [Header("Phases")]
    public TMP_Dropdown phase;
    public TextMeshProUGUI atrialPression;
    public TextMeshProUGUI ventricularVolume;
    public TextMeshProUGUI ventricularInitialPression;
    public TextMeshProUGUI ventricularFinalPression;
    public TextMeshProUGUI phaseVolume;

    [Header("OutputData")]
    public TextMeshProUGUI bodySurface;
    public TextMeshProUGUI cardiacOutput;
    public TextMeshProUGUI cardiacIndex;

    // Start is called before the first frame update
    void Start()
    {
        age.onValueChanged.AddListener(delegate { CheckElderAlert(); });
        weight.onValueChanged.AddListener(delegate { CheckObeseAlert(); CalculateBodySurface(); CalculateCardiacSpent(); CalculateCardiacIndex(); });
        height.onValueChanged.AddListener(delegate { CalculateBodySurface(); CalculateCardiacSpent(); CalculateCardiacIndex(); });
        cardiacIllnessHistory.onValueChanged.AddListener(delegate { CheckCardiacIllnessAlert(); });
        FCM.onValueChanged.AddListener(delegate { CalculateCardiacSpent(); CalculateCardiacIndex(); });
        phase.onValueChanged.AddListener(delegate { CheckPhase(); });

        CalculateBodySurface();
        CalculateCardiacSpent();
        CalculateCardiacIndex();
    }

    #region Alerts

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

    #endregion

    #region Phases

    public void EnablePhasesData()
    {
        phase.gameObject.SetActive(true);
        phaseVolume.transform.parent.gameObject.SetActive(true);
    }   

    public void CheckPhase()
    {
        if(selectedHeartSpace == null)
        {
            return;
        }

        Phase phaseName = phase.value switch
        {
            0 => Phase.fastFill,
            1 => Phase.slowFill,
            2 => Phase.finalFill,
            _ => Phase.finalFill
        };;;

        if (phaseName == Phase.fastFill)
        {
            if (!String.IsNullOrEmpty(selectedHeartSpace.fastFillData.atrialPression))
            {
                atrialPression.text = selectedHeartSpace.fastFillData.atrialPression;
                atrialPression.transform.parent.gameObject.SetActive(true);
                ventricularVolume.transform.parent.gameObject.SetActive(false);
                ventricularInitialPression.transform.parent.gameObject.SetActive(false);
                ventricularFinalPression.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                atrialPression.transform.parent.gameObject.SetActive(false);
                ventricularVolume.text =  selectedHeartSpace.fastFillData.ventricularVolume;
                ventricularInitialPression.text = selectedHeartSpace.fastFillData.ventricularInitialPression;
                ventricularFinalPression.text = selectedHeartSpace.fastFillData.ventricularFinalPression;
                ventricularVolume.transform.parent.gameObject.SetActive(true);
                ventricularInitialPression.transform.parent.gameObject.SetActive(true);
                ventricularFinalPression.transform.parent.gameObject.SetActive(true);
            }
            phaseVolume.text = "30-40 %";
        }
        else if (phaseName == Phase.slowFill)
        {
            if (!String.IsNullOrEmpty(selectedHeartSpace.slowFillData.atrialPression))
            {
                atrialPression.text = selectedHeartSpace.slowFillData.atrialPression;
                atrialPression.transform.parent.gameObject.SetActive(true);
                ventricularVolume.transform.parent.gameObject.SetActive(false);
                ventricularInitialPression.transform.parent.gameObject.SetActive(false);
                ventricularFinalPression.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                atrialPression.transform.parent.gameObject.SetActive(false);
                ventricularVolume.text = selectedHeartSpace.slowFillData.ventricularVolume;
                ventricularInitialPression.text = selectedHeartSpace.slowFillData.ventricularInitialPression;
                ventricularFinalPression.text = selectedHeartSpace.slowFillData.ventricularFinalPression;
                ventricularVolume.transform.parent.gameObject.SetActive(true);
                ventricularInitialPression.transform.parent.gameObject.SetActive(true);
                ventricularFinalPression.transform.parent.gameObject.SetActive(true);
            }
            phaseVolume.text = "60 - 80%";
        }
        else if (phaseName == Phase.finalFill)
        {
            if (!String.IsNullOrEmpty(selectedHeartSpace.finalFillData.atrialPression))
            {
                atrialPression.text = selectedHeartSpace.finalFillData.atrialPression;
                atrialPression.transform.parent.gameObject.SetActive(true);
                ventricularVolume.transform.parent.gameObject.SetActive(false);
                ventricularInitialPression.transform.parent.gameObject.SetActive(false);
                ventricularFinalPression.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                atrialPression.transform.parent.gameObject.SetActive(false);
                ventricularVolume.text = selectedHeartSpace.finalFillData.ventricularVolume;
                ventricularInitialPression.text = selectedHeartSpace.finalFillData.ventricularInitialPression;
                ventricularFinalPression.text = selectedHeartSpace.finalFillData.ventricularFinalPression;
                ventricularVolume.transform.parent.gameObject.SetActive(true);
                ventricularInitialPression.transform.parent.gameObject.SetActive(false);
                ventricularFinalPression.transform.parent.gameObject.SetActive(true);
            }
            phaseVolume.text = "100%";
        }   
    }

    #endregion

    #region outputData
    void CalculateBodySurface()
    {
       float SC=  UtilFormulas.BodySurface((int)weight.value, (int)height.value);
       bodySurface.text = SC.ToString();
    }

    void CalculateCardiacSpent()
    {
        float GC = UtilFormulas.CardiacSpent(FCM.value);
        cardiacOutput.text = GC.ToString();
    } 

    void CalculateCardiacIndex()
    {
        float IC = UtilFormulas.CardiacIndex(FCM.value, float.Parse(bodySurface.text));
        cardiacIndex.text = IC.ToString();
    }
    #endregion
}

public enum Phase
{
    fastFill =0,
    slowFill =1,
    finalFill =2,
}
