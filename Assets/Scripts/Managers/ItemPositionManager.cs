using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPositionManager : MonoBehaviour
{
    [SerializeField]
    private ItemPosition itemPosition;

    [SerializeField]
    private Transform startingPosition;

    [SerializeField]
    private Transform itemPickedPosition;

    private List<ItemPosition> itemPositions;

    void Awake() {
        itemPositions = new List<ItemPosition>();
    }

    public void GenerateItemPositionsToPole(int maximumItemCount, float yOffset) {
        if(itemPosition != null) {
            for(int i = 0; i < maximumItemCount; i++) {
                ItemPosition itemPos = Instantiate(itemPosition, startingPosition.position + (Vector3.up * (yOffset * i)), Quaternion.identity);
                itemPos.transform.SetParent(transform);
                itemPos.transform.localScale = Vector3.one;
                itemPositions.Add(itemPos);
            }
        }
        UpdateItemPositionsState();
    }

    public void UpdateItemPositionsState() {
        if(itemPositions.Count > 0) {
            foreach(ItemPosition ip in itemPositions) {
                ip.UpdateItemPositionState();
            }
        }
    }

    public Transform GetItemPositionTransform(int index) {
        if(itemPositions.Count > 0) {
            return itemPositions[index].transform;
        }
        return null;
    }

    public Transform GetTopAvailablePosition() {
        if(itemPositions.Count > 0) {
            foreach(ItemPosition ip in itemPositions) {
                if(!ip.GetPositionTakenState()) {
                    return ip.transform;
                }
            }
        }
        return null;
    }

    public Transform GetItemPickedPosition(){
        if(itemPickedPosition != null) {
            return itemPickedPosition;
        }
        return null;
    }
}
