using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AI
public class EnemyBehaviour : MonoBehaviour
{
    public GameObject character;
    Animator animator;
    Vector3 startPosition;
    Quaternion startForwardRotation;
    Vector3 target;
    float movementSpeed;
    float rotationSpeed;
    bool returnHome;
    bool stopWalking;
    bool isAttacking;
    bool justAttacked;

    void Start()
    {
        animator = character.GetComponent<Animator>();
        startPosition = new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z);
        startForwardRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        rotationSpeed = 0.4f;
        movementSpeed = 1.5f;
        isAttacking = false;
    }

    void Update()
    {
        if (returnHome)
        {
            float distance = Vector3.Distance(character.transform.position, target);

            if (distance > 0.1f)
            {
                StartWalking();
                Pursue(Vector3.Normalize(character.transform.position - target));
            }
            else if (character.transform.rotation.y < -0.001f || character.transform.rotation.y > 0.001f)
            {
                rotationSpeed = 0.8f;

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("NewSmallWalkAnimation"))
                {
                    animator.Play("NewSmallWalkAnimation");
                }
                character.transform.rotation = Rotate(startForwardRotation);
            }
            else
            {
                StopWalking();
                rotationSpeed = 0.4f;
                returnHome = false;
                isAttacking = false;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("NewStopKickAnimation") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            isAttacking = false;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            returnHome = false;

            target = other.transform.position;
            Vector3 temp = new Vector3(character.transform.position.x, other.transform.position.y, character.transform.position.z);

            float distance = Vector3.Distance(temp, target);

            if (distance > 4.0f && !isAttacking)
            {
                Pursue(Vector3.Normalize(temp - target));
            }

            if (distance <= 4.0f)
            {
                Attack();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        returnHome = true;
        target = startPosition;
    }

    void Pursue(Vector3 direction)
    {
        StartWalking();

        character.transform.rotation = Rotate(Quaternion.LookRotation(direction));
        character.transform.position -= direction * (movementSpeed * Time.deltaTime);
    }

    Quaternion Rotate(Quaternion target)
    {
        return Quaternion.Slerp(character.transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    void Attack()
    {
        StopWalking();

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("NewStartKickAnimation") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("NewKickAnimation") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("NewStopKickAnimation") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("NewStopWalkingAnimation"))
        {
            animator.Play("NewStartKickAnimation");
            isAttacking = true;
        }
    }

    void StartWalking()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("NewStartWalkingAnimation") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("NewWalkingAnimation") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("NewStopKickAnimation") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("NewKickAnimation"))
        {
            animator.Play("NewStartWalkingAnimation");
        }
    }

    void StopWalking()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("NewWalkingAnimation"))
        {
            animator.Play("NewStopWalkingAnimation");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("NewSmallWalkAnimation"))
        {
            animator.Play("NewStopSmallWalkingAnimation");
        }
    }
}
