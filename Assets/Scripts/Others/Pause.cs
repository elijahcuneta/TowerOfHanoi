using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    private void OnEnable() {
        Time.timeScale = 0;
    }

    public void Retry() {
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
        SceneManager.LoadScene("MainMenu");
    }

    private void ResumeTime() {
        Time.timeScale = 1;
    }

    private void OnDisable() {
        Time.timeScale = 1;
    }
}
