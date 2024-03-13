using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealt : MonoBehaviour
{
    public Text Hp;
    public bool Pulsing;

    private float pulsingTick = 0;
    public float pulsingRate = 0.5f;
    private void Update()
    {
        pulsingTick = Mathf.Clamp(pulsingTick + Time.deltaTime, 0, pulsingRate);
        if (Pulsing && pulsingTick >= pulsingRate)
        {
            Hp.color = Color.red;
            pulsingTick = 0;
        }
        Hp.color = Color.Lerp(Hp.color, Color.white, Time.deltaTime* 5f);

    }

    public void ChangeHP(float value)
    {

        Hp.text = value + " HP";
    }

    public void Warning()
    {
        Pulsing = true;
    }
}
