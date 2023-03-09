using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    private Text timeText = null;

    private int minutes;
    private int seconds;

    private string timeString;

    private void Start() {
        minutes = seconds = 0;
        timeString = "";
        StartCoroutine(StartTime());
    }

    private void SetTimeInUI() {
        timeString = minutes.ToString() + ":" + seconds.ToString("00");
        timeText.text = timeString;
    }

    private void TimeConverter() {
        if(seconds >= 60) {
            seconds = 0;
            minutes++;
        }
    }

    private IEnumerator StartTime() {
        float timer = 0;
        while(true) {
            timer += Time.deltaTime;
            if(timer >= 1) {
                timer = 0;
                seconds++;
                TimeConverter();
                SetTimeInUI();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public string GetTimeText(){
        return timeString;
    }
    
    public void ResetTime(){
        seconds = minutes = 0;
    }
}
