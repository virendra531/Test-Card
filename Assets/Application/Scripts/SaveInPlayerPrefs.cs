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
        int rowValue;
        if (int.TryParse(inputFieldRow.text, out rowValue))
        {
            Save("row", rowValue);
        }
        else
        {
            Debug.LogError("Invalid input for row");
        }
    }
    public void SaveColumn()
    {
        int columnValue;
        if (int.TryParse(inputFieldColumn.text, out columnValue))
        {
            Save("column", columnValue);
        }
        else
        {
            Debug.LogError("Invalid input for column");
        }
    }

    public void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt("highscore", score);
    }
}
