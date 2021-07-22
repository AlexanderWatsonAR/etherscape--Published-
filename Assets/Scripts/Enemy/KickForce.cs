using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Forward,
    Backward,
    Left,
    Right,
    Up,
    Down

}

public class KickForce : MonoBehaviour
{
    public Animator animator;
    public float kickForce = 30.0f;
    public string kickAnimation = "NewKickAnimation";
    public Direction kickDirection = Direction.Backward;
    public float kickPoint = 0.3f;

    Vector3 aKickDirection;

    void Start()
    {
        switch(kickDirection)
        {
            case Direction.Forward:
                aKickDirection = transform.forward;
                break;
            case Direction.Backward:
                aKickDirection = transform.forward * -1;
                break;
            case Direction.Left:
                aKickDirection = transform.right * -1;
                break;
            case Direction.Right:
                aKickDirection = transform.right;
                break;
            case Direction.Up:
                aKickDirection = transform.up;
                break;
            case Direction.Down:
                aKickDirection = transform.up * -1;
                break;
        }

        if (aKickDirection.y < 0.0f)
            aKickDirection.y = aKickDirection.y * -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody.name == "Player")
        {
            // Checks if the kick is on the down swing.
            if(animator.GetCurrentAnimatorStateInfo(0).IsName(kickAnimation) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime > kickPoint)
            {
                collision.rigidbody.AddForce(new Vector3(aKickDirection.x * kickForce, aKickDirection.y * kickForce, aKickDirection.z * kickForce), ForceMode.Impulse);
                if (GetComponent<AudioSource>() != null)
                    GetComponent<AudioSource>().Play();
            }
        }
    }
}
