using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ring : Item
{
    [SerializeField]
    private Transform ringBodyTransform;

    [SerializeField]
    private Transform ringMaskLeft;

    [SerializeField]
    private Transform ringMaskRight;

    [SerializeField]
    private SpriteRenderer ringSprite;

    private Vector3 bodySize;

    void Awake() {
        if(ringBodyTransform != null) {
            bodySize = ringBodyTransform.localScale;
        }

        float red = Random.Range(0.2f, 0.9f);
        float green = Random.Range(0.2f, 0.9f);
        float blue = Random.Range(0.2f, 0.9f);

        ringSprite.color = new Color(red , green, blue, 100);
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
            ringBodyTransform.localScale = bodySize;
            ringMaskLeft.transform.localPosition = new Vector3((ringBodyTransform.localPosition.x - ringBodyTransform.localScale.x) - (ringMaskLeft.localScale.x / 2), ringMaskLeft.localPosition.y, ringMaskLeft.localPosition.z);
            ringMaskRight.transform.localPosition = new Vector3((ringBodyTransform.localPosition.x + ringBodyTransform.localScale.x) + (ringMaskRight.localScale.x / 2), ringMaskRight.localPosition.y, ringMaskRight.localPosition.z);
        }
    }
}
