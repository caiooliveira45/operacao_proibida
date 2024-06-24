using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    [SerializeField]
    private Placement _variablePlacement;
    [SerializeField]
    private Placement _numberPlacement;
    [SerializeField]
    private List<string> _placementsCurrentPieces;

    public int variableValue { get; private set; }
    public bool check = false;

    private void Update()
    {
        CheckDistribution();
    }

    public void CheckDistribution()
    {
        int aux;

        _placementsCurrentPieces[0] = _variablePlacement.puzzleChar;
        _placementsCurrentPieces[1] = _numberPlacement.puzzleChar;
        aux = ConvertToInt(_placementsCurrentPieces[1]);

        if (_placementsCurrentPieces[0] == "X" && aux >= 0)
        {
            variableValue = aux;
        }
        else
        {
            variableValue = -1;
        }
    }

    public void MakePlacementsStatic()
    {
        _variablePlacement.isStatic = true;
        _numberPlacement.isStatic = true;
    }

    private int ConvertToInt(string placementString)
    {
        int number;

        bool success = int.TryParse(placementString, out number);

        if (success)
            return number;
        else
            return -1;
    }
}
