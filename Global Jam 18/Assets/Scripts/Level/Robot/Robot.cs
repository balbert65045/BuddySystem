using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Sprite[] robotSprites;
    public SpriteRenderer currentSprite;
    Vector3 currentPosition;
    Vector3 targetPosition;

    public Direction facingDirection;
    public Cell currentCell;

    public bool locked = false;
    Animator animator;

    float moveEndTime;
    float moveStartTime;
    public float moveDuration = .4f;

    public bool dead = false;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (moveEndTime > Time.time)
        {
            Move(Mathf.Min(1, (Time.time - moveStartTime) / moveDuration));
        }
    }

    public void Turn(Direction targetDirection)
    {
        //Set direction realtive to facing direction and swap the sprite
        switch (facingDirection)
        {
            case Direction.UP:
                switch (targetDirection)
                {
                    case Direction.UP:
                        //Do Nothing
                        break;

                    case Direction.DOWN:
                        facingDirection = Direction.DOWN;
                        break;

                    case Direction.LEFT:
                        facingDirection = Direction.LEFT;
                        break;

                    case Direction.RIGHT:
                        facingDirection = Direction.RIGHT;
                        break;
                }
                break;

            case Direction.DOWN:
                switch (targetDirection)
                {
                    case Direction.UP:
                        //Do Nothing
                        break;

                    case Direction.DOWN:
                        facingDirection = Direction.UP;
                        break;

                    case Direction.LEFT:
                        facingDirection = Direction.RIGHT;
                        break;

                    case Direction.RIGHT:
                        facingDirection = Direction.LEFT;
                        break;
                }
                break;

            case Direction.LEFT:
                switch (targetDirection)
                {
                    case Direction.UP:
                        //Do Nothing
                        break;

                    case Direction.DOWN:
                        facingDirection = Direction.RIGHT;
                        break;

                    case Direction.LEFT:
                        facingDirection = Direction.DOWN;
                        break;

                    case Direction.RIGHT:
                        facingDirection = Direction.UP;
                        break;
                }
                break;

            case Direction.RIGHT:
                switch (targetDirection)
                {
                    case Direction.UP:
                        //Do Nothing
                        break;

                    case Direction.DOWN:
                        facingDirection = Direction.LEFT;
                        break;

                    case Direction.LEFT:
                        facingDirection = Direction.UP;
                        break;

                    case Direction.RIGHT:
                        facingDirection = Direction.DOWN;
                        break;
                }
                break;
        }
        //Set sprite based on facingDirection
        currentSprite.sprite = robotSprites[(int)facingDirection];
        currentSprite.flipX = facingDirection == Direction.LEFT;
    }

    public void ForceDirection(Direction direction)
    {
        facingDirection = direction;
        //Set sprite based on facingDirection
        currentSprite.sprite = robotSprites[(int)facingDirection];
        currentSprite.flipX = facingDirection == Direction.LEFT;
    }

    //ONCE MOVE IS COMPLETE
    //  -Might want to check the new cell for special properties(good and bad) via a method call to Cell

    //Move method called from Robot's update method
    public void Move(float percent)
    {
        transform.position = Vector3.Lerp(currentPosition, targetPosition, percent);
    }

    //Initial move method called from LevelManager
    public void Move(Vector3 targetPosition)
    {
        currentPosition = transform.position;
        this.targetPosition = targetPosition + new Vector3(0, 0.2f, 0);
        moveStartTime = Time.time;
        moveEndTime = Time.time + moveDuration;
    }

    public void FallOff()
    {

    }

    public void Explode()
    {
        animator.SetTrigger("Explode");
    }
}