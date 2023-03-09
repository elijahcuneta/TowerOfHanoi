using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Picked }

public class GameInputManager : MonoBehaviour
{

    [SerializeField]
    private PoleManager poleManager;

    [SerializeField]
    private KeyCode leftPole_Key = KeyCode.LeftArrow;

    [SerializeField]
    private KeyCode rightPole_Key = KeyCode.RightArrow;

    [SerializeField]
    private KeyCode poleInteract_Key = KeyCode.Space;

    [SerializeField]
    private MovesManager movesManager;

    private PlayerState playerState;
    
    private ItemPositionManager pole_itemPositionManager;
    private ItemPositionManager previousPole_itemPositionManager;

    private Pole previousPole;
    private Item currentItem;

    void Start() {
        playerState = PlayerState.Idle;
    }

    void Update() {
        #if UNITY_STANDALONE_WIN
        if(poleManager != null) {
            if(Input.GetKeyDown(leftPole_Key)) {
                poleManager.PrecedingPole();
            } else if(Input.GetKeyDown(rightPole_Key)) {
                poleManager.NextPole();
            } else if (Input.GetKeyDown(poleInteract_Key)) {
                if(poleManager.GetHighlightedPole() != null) {
                    CheckPole(poleManager.GetHighlightedPole());
                }
            }
        }
        #endif
    }

    public void CheckPole(Pole pole) {
        if(pole == null || (playerState == PlayerState.Idle && pole.GetItemCount() <= 0 ) || poleManager.IsItemMoving()) {
            return;
        }

        pole_itemPositionManager = pole.GetItemPositionManager();

        switch(playerState) {
            case PlayerState.Idle:
                previousPole = pole;
                previousPole_itemPositionManager = previousPole.GetItemPositionManager();
                
                UpdateItemPosition(pole.PeekTopItem(), pole_itemPositionManager.GetItemPickedPosition());
                break;
            case PlayerState.Picked:
                currentItem = previousPole.GetTopItem();
                if(pole.GetItemCount() == 0 || (pole.PeekTopItem() != null && pole.PeekTopItem().GetSize() > currentItem.GetSize())) {
                    pole.AddItem(currentItem);
                    UpdateItemPosition(currentItem, pole_itemPositionManager.GetTopAvailablePosition(), previousPole == pole ? null : pole_itemPositionManager.GetItemPickedPosition());
                    if(previousPole != pole) {
                        movesManager.AddMoves();
                    }
                } else if((pole.PeekTopItem() != null && pole.PeekTopItem().GetSize() < currentItem.GetSize())) {
                    previousPole.AddItem(currentItem);
                    UpdateItemPosition(currentItem, previousPole_itemPositionManager.GetTopAvailablePosition());
                   
                    poleManager.RemoveAllHighlight();
                    previousPole.ChangeState(PoleState.Highlighted);
                }
                
                currentItem = null;
                break;
        }
        poleManager.UpdateAllPolePosition(); //Update whether if there is an available position for item per pole
        UpdatePlayerState();

        if(poleManager != null) {
           poleManager.IsGameDone();
        }
    }

    private void UpdateItemPosition(Item item, Transform newPosition, Transform preNewPosition = null) {
        List<Transform> newPositions = new List<Transform>();
        if(preNewPosition != null) {
            newPositions.Add(preNewPosition);
        }
        newPositions.Add(newPosition);
        item.ChangePosition(newPositions);
    }
    
    private void UpdatePlayerState() {
       if(playerState == PlayerState.Idle) {
           playerState = PlayerState.Picked;
       } else {
           playerState = PlayerState.Idle;
       }
    }
}
