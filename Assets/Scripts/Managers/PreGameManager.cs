using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameManager : MonoBehaviour
{
    [SerializeField]
    private ItemManager itemManager;

    [SerializeField]
    private PoleManager poleManager;

    [SerializeField]
    private MovesManager movesManager;

    [SerializeField]
    private GameObject howToPlay;

    [SerializeField]
    private ThemeManager themeManager;

    public static PreGameManager Instance;

    public static int itemQuantity;
    public static int startingPoleIndex;
    public static int goalPoleIndex;
    public static int themeIndex;

    void Awake() {
        if(Instance != this) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        InstantiateGameData();

        if(!PlayerPrefs.HasKey("FirstTime")) {
            PlayerPrefs.SetString("FirstTime", "firstTime");
            howToPlay.SetActive(true);
        } else if (PlayerPrefs.HasKey("FirstTime")) {
            howToPlay.SetActive(false);
            InstantiateGame();
        }
    }
   
    public void InstantiateGameData() {
        themeManager.SetTheme(themeIndex);
        itemManager.SetItem(themeIndex);
        itemManager.SetQuantity(itemQuantity);
        movesManager.SetMinimumMoves(itemQuantity);
        poleManager.SetStartingPole(startingPoleIndex);
        poleManager.SetGoalPole(goalPoleIndex);
    }

    public void InstantiateGame() {
        if(poleManager != null && itemManager != null) {
            poleManager.GeneratePositionsToPoles(itemManager.GetItemQuantity(), itemManager.GetItem_YOffset());
            itemManager.StartSpawnItems(poleManager.GetStartingPole());
        }
    }
}