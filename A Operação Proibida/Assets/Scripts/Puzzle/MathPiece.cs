using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathPiece : MonoBehaviour
{
    [SerializeField]
    private GameObject _textObject;
    private TextMeshProUGUI _tmp;

    [SerializeField]
    private GameObject _interactIndicator;

    private bool _playerNear;

    public string puzzleChar;
    
    // Start is called before the first frame update
    void Start()
    {
        _tmp = _textObject.GetComponent<TextMeshProUGUI>();

        _tmp.text = puzzleChar;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.InteractPressed && _playerNear)
        {
            PuzzleInventory.instance.AddNewPiece(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerNear = true;
            _interactIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerNear = false;
            _interactIndicator.SetActive(false);
        }
    }

}
