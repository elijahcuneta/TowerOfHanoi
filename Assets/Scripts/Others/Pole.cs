using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PoleState { Normal, Highlighted }

public class Pole : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameInputManager gameInputManager;

    [SerializeField]
    private ItemPositionManager itemPositionManager;

    [SerializeField]
    private GameObject highlightPole;

    [SerializeField]
    private GameObject flag;

    private PoleState poleState = PoleState.Normal;

    private Stack<Item> items;

    void Awake() {
        items = new Stack<Item>();
        flag.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData) {
        CheckPole();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ChangeState(PoleState.Highlighted);
    }

    public void OnPointerExit(PointerEventData eventData) {
        ChangeState(PoleState.Normal);
    }

    private void CheckPole() {
        if(gameInputManager != null) {
            gameInputManager.CheckPole(this);
        }
    }

    private void HighlightPole(bool highlight) {
        if(highlightPole != null) {
            highlightPole.SetActive(highlight);
        }
    }

    public void GenerateItemPositionsToPole(int maximumItemCount, float yOffset) {
        if(itemPositionManager != null) {
            itemPositionManager.GenerateItemPositionsToPole(maximumItemCount, yOffset);
        }
    }

    public void AddItem(Item item) {
        items.Push(item);
    }

    public Item GetTopItem() {
        if(items.Count > 0) {
            return items.Pop();
        }
        return null;
    }

    public Item PeekTopItem() {
        if(items.Count > 0) {
            return items.Peek();
        }
        return null;
    }

    public ItemPositionManager GetItemPositionManager() {
        if(itemPositionManager != null) {
            return itemPositionManager;
        }
        return null;
    }

    public int GetItemCount() {
        return items.Count;
    }

    public PoleState GetPoleState() {
        return poleState;
    }

    public void ChangeState(PoleState poleState) {
        this.poleState = poleState;
        UpdateState();
    }

    public void UpdateState() {
        switch(poleState) {
            case PoleState.Normal:
                HighlightPole(false);
                break;
            case PoleState.Highlighted:
                HighlightPole(true);
                break;
        }
    }

    public bool IsItemMoving() {
        foreach(Item i in items) {
            if(i.GetItemState() == ItemState.Moving) {
                return true;
            }
        }
        return false;
    }

    public void ShowFlag(bool show) {
        flag.SetActive(show);
    }
   
}