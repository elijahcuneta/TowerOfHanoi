using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pagoda : Item
{
   
    [SerializeField]
    private RectTransform pagodaBodyTransform;

    private Vector2 bodySize;

    [SerializeField]
    private Image textPadImage;

    void Awake() {
        if(pagodaBodyTransform != null) {
            bodySize = pagodaBodyTransform.sizeDelta;
        }

        float red = Random.Range(0.2f, 0.9f);
        float green = Random.Range(0.2f, 0.9f);
        float blue = Random.Range(0.2f, 0.9f);

        textPadImage.color = new Color(red , green, blue, 100);
    }

    public override void SetSize(int size) {
        base.SetSize(size);
        base.UpdateText(size);
        ChangeScale(size);
    }
    
    public override void ChangeScale(int size) {
        base.ChangeScale(size);
        if(bodySize != null) {
            bodySize.x += increaseSizeRate * size;
            bodySize.y += increaseSizeRate * size;
            pagodaBodyTransform.sizeDelta = bodySize;
        }
    }

}
