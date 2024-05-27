using UnityEngine;

public class UtilFormulas 
{
    static float systolicVolume = 70f;
   public static float CalculateIMC(float weight, float height)
   {
       height /= 100;
       return weight / (height * height);
   }


    public static bool IsObese(float imc)
    {
        return imc > 30;
    }

    public static bool IsObese(float weight, float height)
    {
        return CalculateIMC(weight, height) > 30;
    }

    public static bool isElder(int age)
    {
        return age > 65;
    }

    public static float BodySurface(int weight, int height)
    {
        float SC = 0.007184f* Mathf.Pow(weight, 0.425f) * Mathf.Pow(height, 0.725f);
        return SC;
    }

    public static float CardiacSpent(float cardiacFrequency)
    {
        float GC=  cardiacFrequency * (systolicVolume /1000);
        return GC;
    }

    public static float CardiacIndex(float cardiacSpent, float bodySurface)
    {
        float IC = cardiacSpent / bodySurface;
        return IC;
    }

    public static float AorticPression(float maxPression, float cardiacFrequency)
    {
        float cycle =  cardiacFrequency /60f;
        float aorticPression = 80f + maxPression * (Mathf.Sin(2* Mathf.PI*cycle - (Mathf.PI/4)));
        return aorticPression -10f;
    }

    public static float LeftVentriculePression(float maxPression, float cardiacFrequency)
    {
        float cycle = cardiacFrequency / 60f;
        float leftVentriculePression = maxPression * Mathf.Sin(2 * Mathf.PI * cycle);
     
        return leftVentriculePression<0 ? 0 : leftVentriculePression;
    }

    public static float LeftAtrialPression(float maxPression, float cardiacFrequency)
    {
        float cycle = cardiacFrequency / 60f;
        float leftAtrialPression = 10 + maxPression * Mathf.Sin(2 * Mathf.PI * cycle - (Mathf.PI / 4));
        return leftAtrialPression;
    }

    public static float LeftVentriculeVolume(float maxVolume, float cardiacFrequency)
    {
        float cycle = cardiacFrequency / 60f;
        float leftVentriculeVolume = maxVolume - 60 * (Mathf.Sin(2 * Mathf.PI * cycle));
        return leftVentriculeVolume < 0 ? 0 : leftVentriculeVolume;
    }
}