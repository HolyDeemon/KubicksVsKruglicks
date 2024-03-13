using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("KubickPlayground");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
