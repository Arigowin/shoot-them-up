using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public Text levelText;
    public int level = 99;

    void Start()
    {
        level = 0;

        SetLevelText();
    }

    void SetLevelText()
    {
        levelText.text = "Level : " + level.ToString();
    }

    public void LevelAdd()
    {
        level++;
        SetLevelText();
    }
}
