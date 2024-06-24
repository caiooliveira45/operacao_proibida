using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placement : MonoBehaviour
{
    [SerializeField]
    private GameObject _textObject;
    private TextMeshProUGUI _tmp;

    [SerializeField]
    private GameObject _interactIndicator;
    
    private bool _playerNear;

    public PuzzleManager puzzle;
    public string puzzleChar;
    public MathPiece attachedPiece;
    public bool isStatic;

    // Start is called before the first frame update
    void Start()
    {
        _tmp = _textObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.InteractPressed && _playerNear && !isStatic)
        {
            PuzzleInventory.instance.PlacementLogic(this);
        }

        if (attachedPiece != null)
            puzzleChar = attachedPiece.puzzleChar;
        else
            puzzleChar = "";
        _tmp.text = puzzleChar;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isStatic)
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
