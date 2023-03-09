using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuantity : MonoBehaviour
{

    [SerializeField]
    private Text quantityText;
    
    private int quantity; 

    private void Start() {
        quantity = 3;    
    }

    public void AddQuantity() {
        quantity++;
        if(quantity > 12) {
            quantity = 3;
        }
        UpdateQuantity();
    }
    
    public void SubtractQuantity() {
        quantity--;
        if(quantity < 3) {
            quantity = 12;
        }
        UpdateQuantity();
    }

    private void UpdateQuantity() {
        quantityText.text = quantity.ToString();
    }

    public int GetQuantity() {
        return quantity;
    }
}
