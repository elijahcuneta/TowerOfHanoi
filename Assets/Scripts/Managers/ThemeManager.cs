using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Theme { Classic, Japan }

public class ThemeManager : MonoBehaviour
{
    [SerializeField]
    private Theme theme = Theme.Classic;

    [SerializeField]
    private Image bgImage;

    [SerializeField]
    private Text[] texts;

    [SerializeField]
    private Image[] images;

    [SerializeField]
    private Sprite[] themeBG;

    [SerializeField]
    private Color[] themeColorText;
    
    [SerializeField]
    private Color[] themeColorImage;

    [SerializeField]
    private Dropdown themeDropDown;

    private void Start() {
        if(!PlayerPrefs.HasKey("ThemeIndex")) {
            PlayerPrefs.SetInt("ThemeIndex", (int)theme);
        } else {
            int themeIndex = PlayerPrefs.GetInt("ThemeIndex");
            theme = (Theme)themeIndex;
            themeDropDown.value = themeIndex;
        }

        UpdateTheme();
    }

    public void SetTheme(int index) {
        theme = (Theme)index;
        PlayerPrefs.SetInt("ThemeIndex", index);
        UpdateTheme();
    }
    
    public int GetThemeIndex(){
        return (int)theme;
    }

    public void UpdateTheme(){
        int themeIndex = (int)theme;

        bgImage.sprite = themeBG[themeIndex];

        foreach(Text t in texts) {
            t.color = themeColorText[themeIndex];
        }

        foreach(Image img in images) {
            img.color = themeColorImage[themeIndex];
        }
    }
    
}
