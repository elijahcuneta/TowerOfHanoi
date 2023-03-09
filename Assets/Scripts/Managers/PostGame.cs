using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostGame : MonoBehaviour
{
    [SerializeField]
    private MovesManager movesManager;

    [SerializeField]
    private TimeManager timeManager;

    [SerializeField]
    private ItemManager itemManager;

    [SerializeField]
    private PoleManager poleManager;

    [SerializeField]
    private Text movesText;

    [SerializeField]
    private Text minimumMovesText;

    [SerializeField]
    private Text timeText;

    private void OnEnable() {
        UpdateUI();    
    }

    private void UpdateUI() {
        movesText.text = "Moves: " + movesManager.GetTotalMoves().ToString();
        minimumMovesText.text = "Minimum Moves: " + movesManager.GetMinimumMoves().ToString();
        timeText.text = "Time: " + timeManager.GetTimeText();
    }
}
