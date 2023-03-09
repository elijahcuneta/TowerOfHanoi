using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    private Pole startingPole;
    private Pole goalPole;

    [SerializeField]
    private GameObject postGamePanel;

    private List<Pole> poles;
    private int quantity;

    void Awake() {
        quantity = 0;

        if(GetComponentsInChildren<Pole>() != null) {
            poles = new List<Pole>(GetComponentsInChildren<Pole>());
        }

        if(postGamePanel != null) {
            postGamePanel.SetActive(false);
        }
    }

    public void SetStartingPole(int poleIndex) {
        startingPole = poles[poleIndex];
    }

    public void SetGoalPole(int poleIndex) {
        goalPole = poles[poleIndex];
        goalPole.ShowFlag(true);
    }

    public void GeneratePositionsToPoles(int maximumItemCount, float yOffset) {
        quantity = maximumItemCount;
        foreach(Pole p in poles) {
            p.GenerateItemPositionsToPole(maximumItemCount, yOffset);
        }
    }

    public void NextPole() {
        if(!poles.Exists( p => p.GetPoleState() == PoleState.Highlighted)) { // if there is no highlighted pole, return the leftmost pole
            poles[0].ChangeState(PoleState.Highlighted);
        } else {
            int highlightedPoleIndex = poles.IndexOf(GetHighlightedPole());
            highlightedPoleIndex = highlightedPoleIndex >= poles.Count - 1 ? 0 : ++highlightedPoleIndex; //if the highlighted is the rightmost, go back to leftmost
            RemoveAllHighlight();
            poles[highlightedPoleIndex].ChangeState(PoleState.Highlighted);
        }
    }

    public void PrecedingPole() {
        if(!poles.Exists( ps => ps.GetPoleState() == PoleState.Highlighted)) { // if there is no highlighted pole, return the leftmost pole
            poles[0].ChangeState(PoleState.Highlighted);
        } else {
            int highlightedPoleIndex = poles.IndexOf(GetHighlightedPole());
            highlightedPoleIndex = highlightedPoleIndex <= 0 ? poles.Count - 1 : --highlightedPoleIndex; //if the highlighted is the rightmost, go back to leftmost
            RemoveAllHighlight();
            poles[highlightedPoleIndex].ChangeState(PoleState.Highlighted);
        }
    }

    public void RemoveAllHighlight() {
        foreach(Pole p in poles) {
            p.ChangeState(PoleState.Normal);
        }
    }

    public Pole GetHighlightedPole() {
        int getHighlightedPoleIndex = poles.FindIndex(p => p.GetPoleState() == PoleState.Highlighted);
        if(getHighlightedPoleIndex >= 0) {
            return poles[getHighlightedPoleIndex];
        }
        return null;
    }

    public Pole GetStartingPole() {
        if(startingPole != null) {
            return startingPole;
        }
        return null;
    }

    public bool IsItemMoving() {
        foreach(Pole p in poles) {
            if(p.IsItemMoving()) {
                return true;
            }
        }
        return false;
    }

    public void UpdateAllPolePosition() {
        foreach(Pole p in poles) {
            p.GetItemPositionManager().UpdateItemPositionsState();
        }
    }

    public void IsGameDone() {
        if(goalPole != null) {
            if(goalPole.GetItemCount() == quantity && postGamePanel != null) {
                StartCoroutine(ShowPostGamePanel());
            }
        }
    }

    private IEnumerator ShowPostGamePanel() {
        while(IsItemMoving()) {
            yield return new WaitForEndOfFrame();
        }
        postGamePanel.SetActive(true);
        StopCoroutine(ShowPostGamePanel());
    }
}
