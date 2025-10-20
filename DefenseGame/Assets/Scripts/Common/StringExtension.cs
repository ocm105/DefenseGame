using System.Text;
using UnityEngine;

public static class StringExtension
{
    public static string StringMerge(params string[] strs)
    {
        StringBuilder builder = new StringBuilder();

        foreach (string str in strs)
        {
            builder.Append(str);
        }
        return builder.ToString();
    }
}
