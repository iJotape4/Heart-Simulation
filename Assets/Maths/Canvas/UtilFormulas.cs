using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class UtilFormulas 
{
   public static float CalculateIMC(float weight, float height)
   {
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
}
