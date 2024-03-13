using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerManager : HealthManager
{
    public Text inventory;
    public string bulletEquped = "Default";

    public Dictionary<string, List<float>> bulletDB = new Dictionary<string, List<float>>() // [0] - ���� �� ���������, [1] - ���������, [2] - ��, [3] - �������, [4]- ��� �� ����; 
    {
        {"Default", new List<float>{5f, 1000f, 1f, 0f, 1f}},
        {"Auto", new List<float>{4f, 20f, 0.2f, 5f, 1f}},
        {"Shotgun", new List<float>{10f, 5f, 2f, 45f, 10f}},
        {"explode", new List<float>{10f, 20f, 4f, 0f, 1f}}
    };

    public Dictionary<string, float> Reloads = new Dictionary<string, float>()
    {
        {"Default", 0f},
        {"Auto", 0f},
        {"Shotgun", 0f},
        {"explode", 0f}
    };

    public Dictionary<string, int> bulletCount = new Dictionary<string, int>()
    {
        {"Default", -1},
        {"Auto", 0},
        {"Shotgun", 0},
        {"explode", 0}
    };

    //public Collider playerCollider;


    public string UpdateGun(string NAME)
    {
        string inventoryText = "";

        //      is default weapon picked
        if (bulletEquped == NAME)
        {
            inventoryText += "-->";
        }
        //      default weapon name
        inventoryText += " " + NAME + " Impact - ";
        if (NAME == "Default")
        {
            inventoryText += "INLIMITED\r\n";
        }
        else
        {
            inventoryText += bulletCount[NAME] + "\r\n";
        }
        //      default weapon cooldown
        float cooldownPercent = (bulletDB[NAME][2] - Reloads[NAME] ) / bulletDB[NAME][2] * 100;
        for (int i = 0; i < cooldownPercent; i++)
        {
            inventoryText += "|";
        }
        inventoryText += "\r\n______________________________________________\r\n\r\n";

        return (inventoryText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "drop")
        {
            bulletCount[other.gameObject.GetComponent<DropScript>().Name] += other.gameObject.GetComponent<DropScript>().count;
        }
        Destroy(other.gameObject);

    }

}
