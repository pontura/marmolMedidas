﻿﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public static class Utils {
 
     public static void RemoveAllChildsIn(Transform container)
     {
         int num = container.transform.childCount;
         for (int i = 0; i < num; i++) UnityEngine.Object.DestroyImmediate(container.transform.GetChild(0).gameObject);
     }
     public static void Shuffle(List<int> texts)
     {
         if (texts.Count < 2) return;
         for (int a = 0; a < 100; a++)
         {
             int id = Random.Range(1, texts.Count);
             int value1 = texts[0];
            int value2 = texts[id];
             texts[0] = value2;
             texts[id] = value1;
         }
     }
    public static float GetVectorInternalAngle(Vector3 a, Vector3 b, Vector3 c)
    {
        return Vector3.Angle(b - a, c - a);
    }
    public static float GetAngleBetween(Vector3 startPoint, Vector3 endPoint)
    {
        float angle = Mathf.Atan2((endPoint.y - startPoint.y), (endPoint.x - startPoint.x));
        angle *= Mathf.Rad2Deg;
        return angle;
    }
    public static class CoroutineUtil
	{
		public static IEnumerator WaitForRealSeconds(float time)
		{
			float start = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < start + time)
			{
				yield return null;
			}
		}
	}
	public static string FormatNumbers(int num)
	{
        return string.Format ("{0:#,#}",  num);
	}
    public static float RoundNumber(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)1);
        return Mathf.Round(value * mult) / mult;
    }
    public static string ToFormattedString(this double rawNumber)
    {
        string[] letters = new string[] { "", "K", "M", "B", "T", "P", "E", "Z", "Y", "KY", "MY", "BY", "TY", "PY", "EY", "ZY", "YY" };
        int prefixIndex = 0;
        while (rawNumber > 1000)
        {
            rawNumber /= 1000.0f;
            prefixIndex++;
            if (prefixIndex == letters.Length - 1)
            {
                break;
            }
        }
        string numberString = rawNumber.ToString();
        if (prefixIndex < letters.Length - 1)
        {
            numberString = ToThreeDigits(numberString);
        }

        string prefix = letters[prefixIndex];
        return $"{numberString}{prefix}";
    }
    private static string ToThreeDigits(string numString)
    {
        if (numString.Length > 4)
        {
            if (numString.Substring(0, 4).Contains("."))
                numString = numString.Substring(0, 5);
            else
                numString = numString.Substring(0, 4);
        }
        return numString;
    }
    public static string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 1;
    }
}
