using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _winUI;
    [SerializeField]
    private GameObject _invUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PauseManager.instance.Pause();
            _winUI.SetActive(true);
            _invUI.SetActive(false);
        }
    }
}
