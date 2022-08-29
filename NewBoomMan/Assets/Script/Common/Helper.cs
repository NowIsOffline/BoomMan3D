using UnityEngine;
public static class Helper
{
     /// <summary>
        /// 四舍五入
        /// </summary>
        /// digits:保留几位小数
     public static float Round(this float value, int digits = 1)
    {
        float multiple = Mathf.Pow(10, digits);
        float tempValue = value * multiple + 0.5f;
        tempValue = Mathf.FloorToInt(tempValue);
        float finalValue = tempValue / multiple;
        return finalValue;
    }

    public static int RoundToInt(this float value)
    {
        float tempValue = value.Round(0);
        int finalValue = Mathf.FloorToInt(tempValue);
        return finalValue;
    }

    public static bool IsVector3Equal(Vector3 a,Vector3 b){
        return a.Equals(b);
    }
}