using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesManager : MonoBehaviour
{

    [SerializeField]
    private Text movesText;

    private int moves;
    private float minimumMoves;

    void Start()
    {
        moves = 0;   
    }
    public void AddMoves() {
        moves++;
        UpdateUI();
    }

    public void SetMinimumMoves(int itemQuantity) {
        minimumMoves = Mathf.Pow(2, itemQuantity) - 1;
        UpdateUI();
    }

    public int GetTotalMoves() {
        return moves;
    }

    private void UpdateUI(){
        movesText.text = moves.ToString() + " / " + minimumMoves.ToString();
    }

    public float GetMinimumMoves() {
        return minimumMoves;
    }

    public void ResetMoves(){
        moves = 0;
        UpdateUI();
    }


}
