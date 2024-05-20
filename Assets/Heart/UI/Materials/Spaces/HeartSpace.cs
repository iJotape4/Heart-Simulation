using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HeartSpace", order = 1)]
public class HeartSpace : ScriptableObject
{
   public HeartSpaceData fastFillData;
   public HeartSpaceData slowFillData;
   public HeartSpaceData finalFillData;
}

[System.Serializable]
public struct HeartSpaceData
{
    public Phase phase;
    public string atrialPression;
    public string ventricularVolume;
    public string ventricularInitialPression;
    public string ventricularFinalPression;
}