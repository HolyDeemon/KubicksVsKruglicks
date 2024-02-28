using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealt : MonoBehaviour
{
    public Text text;
    public HealthManager player;

    private void Update()
    {
        text.text = ("currentHp:" + player.currentHp);
    }
}
