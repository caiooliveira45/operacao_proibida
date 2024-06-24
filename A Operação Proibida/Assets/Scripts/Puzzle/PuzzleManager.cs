using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private List<Placement> _placements;
    [SerializeField]
    private VariableManager _variablePlacer;
    [SerializeField]
    private List<string> _placementsCurrentPieces;
    private bool _calcCorrect = false;
    private bool _isSolved = false;
    private bool _haveVariable = false;

    public bool requireVariable;
    public bool check = true;
    public List<string> placementsCorrectPieces;
    public GameObject lockRoom;

    private void Update()
    {
        //StartCoroutine(Check());
        if (!_isSolved)
            CheckDistribution();
    }

    public void CheckDistribution()
    {
        for (int i = 0; i < _placements.Count; i++)
        {
            _placementsCurrentPieces[i] = _placements[i].puzzleChar;
        }

        _calcCorrect = Calculate();

        if (_calcCorrect)
        {
            lockRoom.SetActive(false);
            PuzzleInventory.instance.AddNewGem();
            for (int i = 0; i < _placements.Count; i++)
            {
                _placements[i].isStatic = true;
            }
            if (_variablePlacer != null)
                _variablePlacer.MakePlacementsStatic();
            _isSolved = true;
        }
    }

    private bool Calculate()
    {
        int a, b, c;
        string operation;
        bool correct;

        _haveVariable = false;
        a = ConvertToInt(_placementsCurrentPieces[0]);
        operation = _placementsCurrentPieces[1];
        b = ConvertToInt(_placementsCurrentPieces[2]);
        c = ConvertToInt(_placementsCurrentPieces[3]);

        if (a < 0 || b < 0 || c < 0)
            correct = false;
        else
        {
            correct = Operation(a, operation, b, c);
        }

        return correct;
    }

    private bool Operation(int a, string op, int b, int c)
    {
        int aux;
        bool check;

        if (op == "+")
        {
            aux = a + b;
            if (c == aux)
                check = true;
            else
                check = false;
        }

        else if (op == "-")
        {
            aux = a - b;
            if (c == aux)
                check = true;
            else
                check = false;
        }

        else if(op == "x")
        {
            aux = a * b;
            if (c == aux)
                check = true;
            else
                check = false;
        }

        else if(op == "/")
        {
            aux = a / b;
            if (c == aux)
                check = true;
            else
                check = false;
        }
        else
            check = false;

        if (check)
            check = CheckVariables();
        return check;
    }

    private bool CheckVariables()
    {
        if (!requireVariable)
            return true;
        else if (requireVariable && _haveVariable)
            return true;
        else if (requireVariable && !_haveVariable)
            return false;
        else
            return false;
    }

    private int ConvertToInt(string placementString)
    {
        int number;

        if (placementString == "X")
        {
            _haveVariable = true;
            return _variablePlacer.variableValue;
        }

        bool success = int.TryParse(placementString, out number);

        if (success)
            return number;
        else
            return -1;
    }

    private IEnumerator Check()
    {
        check = false;
        CheckDistribution();
        yield return new WaitForSeconds(0.1f);
        check = true;
    }

}
