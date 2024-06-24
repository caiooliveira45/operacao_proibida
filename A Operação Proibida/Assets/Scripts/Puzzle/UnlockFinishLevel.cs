using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockFinishLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject _interactIndicator;
    [SerializeField]
    private GameObject _finalLock;
    [SerializeField]
    private GameObject _numberIndicator;
    private TextMeshProUGUI _numberTMP;

    private bool _playerNear;
    private int _currentGems;
    [SerializeField]
    private int _requiredGems;

    public bool isInteracted;

    private void Start()
    {
        _numberTMP = _numberIndicator.GetComponent<TextMeshProUGUI>();
        _numberTMP.text = _requiredGems.ToString();
    }

    private void Update()
    {
        if (InputManager.instance.InteractPressed && _playerNear && !isInteracted)
        {
            _currentGems = PuzzleInventory.instance.Gems;
            OpenFinish();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isInteracted)
        {
            _playerNear = true;
            _interactIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isInteracted)
        {
            _playerNear = false;
            _interactIndicator.SetActive(false);
        }
    }

    private void OpenFinish()
    {
        if (_currentGems >= _requiredGems)
        {
            Debug.Log("Open");
            _finalLock.SetActive(false);
            _interactIndicator.SetActive(false);
            _numberIndicator.SetActive(false);
            isInteracted = true;
        }
        else
        {
            Debug.Log("Closed");
        }
    }
}
