using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    float time = 60;
    void Update()
    {
        if (!DataHolder.isHintsActive)
            time -= Time.deltaTime;
        if (time <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            time = 0;
            return;
        }
        int tme = (int)time;
        text.text = "00:" + (tme / 10).ToString() + (tme % 10).ToString();
    }
}
