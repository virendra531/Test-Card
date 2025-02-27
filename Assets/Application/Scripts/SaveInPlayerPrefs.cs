using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveInPlayerPrefs : MonoBehaviour
{
    public TMP_InputField inputFieldRow;
    public TMP_InputField inputFieldColumn;

    
    public void SaveRow()
    {
        Save("row", inputFieldRow.text);
    }
    public void SaveColumn()
    {
        Save("column", inputFieldColumn.text);
    }

    public void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt("highscore", score);
    }
}
