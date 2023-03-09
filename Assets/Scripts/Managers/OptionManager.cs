using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField]
    private ItemQuantity itemQuantity;

    [SerializeField]
    private ThemeManager themeManager;

    [SerializeField]
    private Dropdown startingPoleDropDown;

    [SerializeField]
    private Dropdown goalPoleDropDown;
    
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private GameObject message;

    private int startingPoleIndex = 0;
    private int goalPoleIndex = 2;

    public void BeginGame(){
        InstantiateDataGame();
    }

    private void InstantiateDataGame() {
        GameDataInstantiator.Instance.SetGameData(itemQuantity.GetQuantity(), startingPoleIndex, goalPoleIndex, themeManager.GetThemeIndex());
    }

    public void StartingPoleDropDown(int index) {
        startingPoleIndex = GetIndex(startingPoleDropDown.options[index].text);
        CheckDropdown();
    }

    public void GoalPoleDropDown(int index) {
        goalPoleIndex = GetIndex(goalPoleDropDown.options[index].text);
        CheckDropdown();
    }

    private int GetIndex(string text) {
        int index = 0;
            switch(text) {
                case "Left":
                    index = 0;
                    break;
                case "Middle":
                    index = 1;
                    break;
                case "Right":
                    index = 2;
                    break;
        }
        return index;
    }

    private void CheckDropdown(){
        playButton.interactable = startingPoleIndex == goalPoleIndex ? false : true;
        message.SetActive(!playButton.interactable);
    }

}
