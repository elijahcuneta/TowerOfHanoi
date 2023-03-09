using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int quantity;

    [SerializeField]
    private Item[] items;

    private Item item;

    private Stack<Item> stackItems;

    private int itemIndex;

    void Awake()
    {
        stackItems = new Stack<Item>();
        itemIndex = 0;
    }

    public void StartSpawnItems(Pole startingPole) {
        if(startingPole != null) {
            StartCoroutine(SpawnItems(startingPole));
        }
    }

    public void SetItem(int index) {
        item = items[index];
    }

    private IEnumerator SpawnItems(Pole startingPole){
        if(item != null) {
            for(int i = 0; i < quantity; i++) {
                Item newItem = Instantiate(item, transform.localPosition, Quaternion.identity);

                newItem.transform.SetParent(startingPole.GetItemPositionManager().GetItemPositionTransform(i));
                newItem.transform.localPosition = Vector3.zero;

                newItem.SetSize(quantity - i);
                newItem.transform.name = "Item_" + newItem.GetSize(); //for easy reference only.

                stackItems.Push(newItem);
                startingPole.AddItem(newItem);
                yield return new WaitForEndOfFrame();
            }
        }
     
        startingPole.GetItemPositionManager().UpdateItemPositionsState();
    }

    public void SetQuantity(int quantity) {
        this.quantity = quantity;
    }
    
    public int GetItemQuantity(){ return quantity; }

    public float GetItem_YOffset() { 
        if(item != null) { 
            return item.GetYOffset();
        } else {
            return 0f;
        }
    }

    public Stack<Item> GetItems(){ return stackItems; }
}
