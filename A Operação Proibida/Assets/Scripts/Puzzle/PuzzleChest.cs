using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChest : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private GameObject _interactIndicator;
    [SerializeField]
    private GameObject _roomLock;
    [SerializeField]
    private GameObject _mathPuzzle;

    private bool _playerNear;

    public bool isInteracted;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (InputManager.instance.InteractPressed && _playerNear && !isInteracted)
        {
            OpenChest();
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

    private void OpenChest()
    {
        _animator.SetBool("IsOpen", true); 
        _interactIndicator.SetActive(false);
        
        if (_roomLock != null)
            _roomLock.SetActive(true);
        if (_mathPuzzle != null)
            _mathPuzzle.SetActive(true);

        isInteracted = true;
    }
}
