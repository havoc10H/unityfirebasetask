using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chessman : MonoBehaviour
{
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isWhite;
    private Vector3 desiredPosition;
    private Vector3 desiredScale = new(100, 100, 100);
    private float duration = 0.0f;
    private bool setPos = false;
    // public bool isMoved = false;
    public int rookPos = -1; //0, 1, 2, 3
    private bool dragging; // anim method
    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMoves()
    {
        return new bool[8, 8];
    }

    public bool Move(int x, int y, ref bool[,] r)
    {
        
        return false;
    }

    private void Update() {
        if (dragging) {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 7);
        } else {                
            if (setPos) {
                StartCoroutine(SetPositionWithAnimation());
            } 
        }
        setPos = false;
        
        // Debug.Log(Time.deltaTime * 7);
    }

    public virtual void SetPositionAnim(Vector3 position, bool is_dragging = false, bool force = false ) {
        dragging = is_dragging;
        desiredPosition = position;
        
        // float distance = Vector3.Distance(position, transform.position);
        // Debug.Log("Distance between position1 and position2: " + distance);
        if(force) {
            transform.position = desiredPosition;
        }
        setPos = true;
    }

    public virtual void SetScaleAnim(Vector3 scale, bool is_dragging = false, bool force = false ) {
        desiredScale = scale;
        dragging = is_dragging;
        if(force) {
            transform.localScale = desiredScale;
        }
        setPos = true;
    }

    IEnumerator SetPositionWithAnimation()
    {
        duration = 0.0f;
        Vector3 pos = transform.position + Vector3.up * 1.2f;
        while( duration < 0.2f ) {
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 7);
            // transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, t);
            duration += Time.deltaTime;
            yield return null;
        }
        duration = 0.0f;
        while( duration < 0.4f ) {
            transform.position = Vector3.Lerp(transform.position, desiredPosition + Vector3.up * 1.2f, Time.deltaTime * 7);
            // transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, t);
            duration += Time.deltaTime;
            yield return null;
        }
        duration = 0.0f;
        while( duration < 0.2f ) {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 7);
            // transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, t);
            duration += Time.deltaTime;
            yield return null;
        }
        transform.position = desiredPosition;
    }
}
