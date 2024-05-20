using System;
using TMPro;
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
    public Slider atrialPression;
    public Slider ventricularVolume;
    public Slider ventricularInitialPression;
    public Slider ventricularFinalPression;
    public Slider phaseVolume;

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
            FastFill();
        }
        else if (phaseName == Phase.slowFill)
        {
            SlowFill();
        }
        else if (phaseName == Phase.finalFill)
        {
            FinalFill();
        }
    }

    private void FinalFill()
    {
        if (selectedHeartSpace.finalFillData.atrialPressionMin !=0)
        {
            atrialPression.minValue = selectedHeartSpace.finalFillData.atrialPressionMin;
            atrialPression.maxValue = selectedHeartSpace.finalFillData.atrialPressionMax;

            atrialPression.transform.parent.gameObject.SetActive(true);
            ventricularVolume.transform.parent.gameObject.SetActive(false);
            ventricularInitialPression.transform.parent.gameObject.SetActive(false);
            ventricularFinalPression.transform.parent.gameObject.SetActive(false);
        }
        else
        {           
            ventricularVolume.minValue = selectedHeartSpace.finalFillData.ventricularVolumeMin;
            ventricularVolume.maxValue = selectedHeartSpace.finalFillData.ventricularVolumeMax;
            ventricularInitialPression.minValue = selectedHeartSpace.finalFillData.ventricularVolumeMin;
            ventricularInitialPression.maxValue = selectedHeartSpace.finalFillData.ventricularVolumeMax;
            ventricularFinalPression.minValue = selectedHeartSpace.finalFillData.ventricularVolumeMin;
            ventricularFinalPression.maxValue = selectedHeartSpace.finalFillData.ventricularVolumeMax;

            atrialPression.transform.parent.gameObject.SetActive(false);
            ventricularVolume.transform.parent.gameObject.SetActive(true);
            ventricularInitialPression.transform.parent.gameObject.SetActive(false);
            ventricularFinalPression.transform.parent.gameObject.SetActive(true);
        }
        phaseVolume.minValue = 100f;
        phaseVolume.maxValue = 100f;
    }

    private void SlowFill()
    {
        if (selectedHeartSpace.slowFillData.atrialPressionMin != 0)
        {
            atrialPression.minValue = selectedHeartSpace.slowFillData.atrialPressionMin;
            atrialPression.maxValue = selectedHeartSpace.slowFillData.atrialPressionMax;
            atrialPression.transform.parent.gameObject.SetActive(true);
            ventricularVolume.transform.parent.gameObject.SetActive(false);
            ventricularInitialPression.transform.parent.gameObject.SetActive(false);
            ventricularFinalPression.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            ventricularVolume.minValue = selectedHeartSpace.slowFillData.ventricularVolumeMin;
            ventricularVolume.maxValue = selectedHeartSpace.slowFillData.ventricularVolumeMax;
            ventricularInitialPression.minValue = selectedHeartSpace.slowFillData.ventricularVolumeMin;
            ventricularInitialPression.maxValue = selectedHeartSpace.slowFillData.ventricularVolumeMax;
            ventricularFinalPression.minValue = selectedHeartSpace.slowFillData.ventricularVolumeMin;
            ventricularFinalPression.maxValue = selectedHeartSpace.slowFillData.ventricularVolumeMax;

            atrialPression.transform.parent.gameObject.SetActive(false);
            ventricularVolume.transform.parent.gameObject.SetActive(true);
            ventricularInitialPression.transform.parent.gameObject.SetActive(true);
            ventricularFinalPression.transform.parent.gameObject.SetActive(true);
        }
        phaseVolume.minValue = 60f;
        phaseVolume.maxValue = 80f;
    }

    private void FastFill()
    {
        if (selectedHeartSpace.fastFillData.atrialPressionMin != 0)
        {
            atrialPression.minValue = selectedHeartSpace.fastFillData.atrialPressionMin;
            atrialPression.maxValue = selectedHeartSpace.fastFillData.atrialPressionMax;
            atrialPression.transform.parent.gameObject.SetActive(true);
            ventricularVolume.transform.parent.gameObject.SetActive(false);
            ventricularInitialPression.transform.parent.gameObject.SetActive(false);
            ventricularFinalPression.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            ventricularVolume.minValue = selectedHeartSpace.fastFillData.ventricularVolumeMin;
            ventricularVolume.maxValue = selectedHeartSpace.fastFillData.ventricularVolumeMax;
            ventricularInitialPression.minValue = selectedHeartSpace.fastFillData.ventricularVolumeMin;
            ventricularInitialPression.maxValue = selectedHeartSpace.fastFillData.ventricularVolumeMax;
            ventricularFinalPression.minValue = selectedHeartSpace.fastFillData.ventricularVolumeMin;
            ventricularFinalPression.maxValue = selectedHeartSpace.fastFillData.ventricularVolumeMax;
            atrialPression.transform.parent.gameObject.SetActive(false);
            ventricularVolume.transform.parent.gameObject.SetActive(true);
            ventricularInitialPression.transform.parent.gameObject.SetActive(true);
            ventricularFinalPression.transform.parent.gameObject.SetActive(true);
        }
        phaseVolume.minValue = 30;
        phaseVolume.maxValue = 40;
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
        float IC = UtilFormulas.CardiacIndex(float.Parse(cardiacOutput.text), float.Parse(bodySurface.text));
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
