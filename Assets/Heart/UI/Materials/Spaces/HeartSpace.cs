using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HeartSpace", order = 1)]
public class HeartSpace : ScriptableObject
{
   public string heartSpaceName;
   public HeartSpaceData fastFillData;
   public HeartSpaceData slowFillData;
   public HeartSpaceData finalFillData;
}

[System.Serializable]
public struct HeartSpaceData
{
    public Phase phase;
    public float atrialPressionMin;
    public float atrialPressionMax;
    public float ventricularVolumeMin;
    public float ventricularVolumeMax;
    public float ventricularInitialPressionMin;
    public float ventricularInitialPressionMax;
    public float ventricularFinalPressionMin;
    public float ventricularFinalPressionMax;
}