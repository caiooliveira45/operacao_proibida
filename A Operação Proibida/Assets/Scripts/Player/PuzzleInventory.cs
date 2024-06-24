using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleInventory : MonoBehaviour
{
    public static PuzzleInventory instance;

    public MathPiece mathPiece;
    public int Gems { get; private set; }

    [SerializeField]
    private string _mathPieceChar = "";
    [SerializeField]
    private Transform _pieceRemovePlacement;
    private GameObject _pieceDisplayUI;
    private GameObject _gemDisplayUI;
    private TextMeshProUGUI _pieceTMP;
    private TextMeshProUGUI _gemTMP;

    private bool _canPick = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pieceDisplayUI = GameObject.FindGameObjectWithTag("PieceDisplay");
        _gemDisplayUI = GameObject.FindGameObjectWithTag("GemDisplay");
        _pieceTMP = _pieceDisplayUI.GetComponent<TextMeshProUGUI>();
        _gemTMP = _gemDisplayUI.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mathPiece != null)
            _mathPieceChar = mathPiece.puzzleChar;
        else
            _mathPieceChar = "";

        _pieceTMP.text = _mathPieceChar;
        _gemTMP.text = Gems.ToString();
    }

    public void AddNewGem()
    {
        Gems++;
    }

    public void AddNewPiece(MathPiece newPiece)
    {
        if (_canPick)
        {
            if (mathPiece == null)
                AddToInventory(newPiece);
            else
            {
                RemoveFromInventory();
                AddToInventory(newPiece);
            }
            StartCoroutine(Cooldown(1f));
        }
        
    }

    private void AddToInventory(MathPiece newPiece)
    {
        mathPiece = newPiece;
        newPiece.gameObject.SetActive(false);
    }

    private void RemoveFromInventory()
    {
        mathPiece.gameObject.transform.position = _pieceRemovePlacement.position;
        mathPiece.gameObject.SetActive(true);
        mathPiece = null;
    }

    public void PlacementLogic(Placement placement)
    {
        if (placement.attachedPiece == null)
        {
            PlacePiece(placement);
        }
        else if (placement.attachedPiece != null)
        {
            if (mathPiece == null)
            {
                RemovePiece(placement);
            }
            else
            {
                ReplacePiece(placement);
            }
        }
    }

    private void PlacePiece(Placement placement)
    {
        if (mathPiece != null)
        {
            placement.attachedPiece = mathPiece;
            mathPiece = null;
        }
        
    }

    private void ReplacePiece(Placement placement)
    {
        MathPiece auxPiece = placement.attachedPiece;
        placement.attachedPiece = mathPiece;
        mathPiece = auxPiece;
    }

    private void RemovePiece(Placement placement)
    {
        mathPiece = placement.attachedPiece;
        placement.attachedPiece = null;
    }

    private IEnumerator Cooldown(float cd)
    {
        _canPick = false;
        yield return new WaitForSeconds(cd);
        _canPick = true;
    }
}
