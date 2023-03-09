using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPosition : MonoBehaviour
{
    private bool isPositionTaken;

    public bool GetPositionTakenState () {
        return isPositionTaken;
    }

    public void UpdateItemPositionState () {
        if(transform.childCount > 0) {
            isPositionTaken = true;
        } else {
            isPositionTaken = false;
        }
    }
   
}
