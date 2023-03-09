using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemState { Stasis, Moving };
public class Item : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve speedCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField]
    protected Text numberText;
    [SerializeField]
    protected ParticleSystem splash;
    [SerializeField]
    protected AudioSource hitSFX;
    [SerializeField]
    protected float yOffset;
    [SerializeField]
    protected float increaseSizeRate = 50;
    [SerializeField]
    protected float timeToDestination = 1f;
    
    private ItemState itemState = ItemState.Stasis;

    private float timer;
    private float seconds;

    protected int size;

    private void Start() {
        timer = seconds = 0f;
        splash.gameObject.SetActive(false);
    }
    
    public virtual void SetSize(int size) { this.size = size; }
    public virtual int GetSize() { return size; }
    
    public virtual float GetYOffset() { return yOffset; }

    protected virtual void UpdateText(int size){
        return;
        if(size > 0) {
            numberText.text = size.ToString();
        }
    }
    
    public virtual void ChangeScale(int size) {
        transform.localScale = Vector3.one; //reset the scale since after instantiating, the item changed its parent to game canvas
    }

    public virtual void ChangePosition(List<Transform> newPosition) {
        if(transform.parent != null) {
            transform.SetParent(transform.root);
        }

        StartCoroutine(AnimateItem(newPosition));
    }
   
    private IEnumerator AnimateItem(List<Transform> targetPositions) {
        itemState = ItemState.Moving;
        for(int i = 0; i < targetPositions.Count; i++) {
            timer = seconds = 0;
            while(timer <= timeToDestination) {
                timer += Time.deltaTime;
                seconds = timer / timeToDestination;
                transform.position = Vector3.Lerp(transform.position, targetPositions[i].position, speedCurve.Evaluate(seconds));
                yield return new WaitForEndOfFrame();
            }
        }
        Transform endPosition = targetPositions[targetPositions.Count - 1];
        
        transform.position = endPosition.position;
        itemState = ItemState.Stasis;

        SetItemToParent(endPosition, targetPositions.Count);
        StopCoroutine(AnimateItem(targetPositions));
    }

    private void SetItemToParent(Transform newPosition, int positionCount) {
        if(positionCount > 1) {
            PlaySplash();
        }
        
        transform.SetParent(newPosition.transform);
        transform.localPosition = Vector3.zero;
    }

    private void PlaySplash() {
        splash.gameObject.SetActive(true);
        hitSFX.Play();
        splash.Play();
    }

    public ItemState GetItemState() {
        return itemState;
    }
   
}
