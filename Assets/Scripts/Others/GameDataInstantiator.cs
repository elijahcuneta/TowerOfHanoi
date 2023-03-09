using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataInstantiator : MonoBehaviour
{
    public static GameDataInstantiator Instance;

    void Awake() {
        if(Instance != this) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void SetGameData(int itemQuantity, int startingPoleIndex, int goalPoleIndex, int themeIndex) {
        PreGameManager.itemQuantity = itemQuantity;
        PreGameManager.startingPoleIndex = startingPoleIndex;
        PreGameManager.goalPoleIndex = goalPoleIndex;
        PreGameManager.themeIndex = themeIndex;

        SceneManager.LoadScene(1);
    }
   
}
