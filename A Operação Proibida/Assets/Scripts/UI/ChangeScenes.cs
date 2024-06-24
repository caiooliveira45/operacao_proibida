using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void ChangeSceneTo(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
