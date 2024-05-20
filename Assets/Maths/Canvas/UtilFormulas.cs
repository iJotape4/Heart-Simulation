using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class UtilFormulas 
{
    static int systolicVolume = 70;
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
        float GC=  cardiacFrequency * systolicVolume;
        return GC;
    }

    public static float CardiacIndex(float cardiacSpent, float bodySurface)
    {
        float IC = cardiacSpent / bodySurface;
        return IC;
    }
}
