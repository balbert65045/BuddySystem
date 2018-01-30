using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
    public Sprite[] robotSprites;
    public SpriteRenderer spriteRenderer;
    Sprite currentSprite;

    Vector3 currentPosition;
    Vector3 targetPosition;

    float moveStartTime;
    float moveEndTime;
    public float moveDuration;

    void Update()
    {
        if (moveEndTime > Time.time)
        {
            MoveForward(Mathf.Min(1, (Time.time - moveStartTime) / moveDuration));
        }
    }

    public void ChangeSprite(int idx)
    {
        spriteRenderer.sprite = robotSprites[idx];
    }

    public void Move(Vector3 nextPos)
    {
        targetPosition = nextPos;
        currentPosition = transform.position;
        moveStartTime = Time.time;
        moveEndTime = moveStartTime = moveDuration;
        //enabled = true;
    }

    void MoveForward(float percent)
    {
        Debug.Log("Move percent = " + percent);
        //if (Mathf.Abs(percent - 1) < 0.000001)
        //{
        //    percent = 1;
        //    enabled = false;
        //}
        transform.position = Vector3.Lerp(currentPosition, targetPosition, percent);
    }
}
