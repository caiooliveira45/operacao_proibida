using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> panels;

    public void Pause(GameObject panel)
    {
        PauseManager.instance.Pause();
        OpenPanel(panel);
    }

    public void Unpause(GameObject panel)
    {
        PauseManager.instance.Unpause();
        ClosePanel(panel);
    }

    public void CloseAll()
    {
        for (int i = 0; i < panels.Count; i++)
            panels[i].SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SwitchPanels(GameObject newPanel)
    {
        gameObject.SetActive(false);
        newPanel.SetActive(true);
    }
}
