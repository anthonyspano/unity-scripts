using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RootMotionScript : MonoBehaviour
{
    Vector3 moveDirection;
    CharacterController cc;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection);
            //moveDirection = moveDirection * animator.GetFloat("WalkSpeed");
            //cc.Move(moveDirection * Time.deltaTime);

            //Vector3 newPosition = transform.position;
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection);
            //moveDirection = animator.GetFloat("WalkSpeed") * Time.deltaTime;
            //Debug.Log(moveDirection);
            //newPosition += moveDirection;
            //transform.position = newPosition;

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                animator.SetFloat("WalkSpeed", 5f);
                Vector3 newPosition = transform.position;
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                if (moveDirection.z > 0)
                    newPosition.z += animator.GetFloat("WalkSpeed") * Time.deltaTime;
                if (moveDirection.x > 0)
                    newPosition.x += animator.GetFloat("WalkSpeed") * Time.deltaTime;
                if (moveDirection.z < 0)
                    newPosition.z -= animator.GetFloat("WalkSpeed") * Time.deltaTime;
                if (moveDirection.x < 0)
                    newPosition.x -= animator.GetFloat("WalkSpeed") * Time.deltaTime;

                transform.position = newPosition;
                Debug.Log(moveDirection);

            }
            else
                animator.SetFloat("WalkSpeed", 0f);



            //Vector3 newPosition = transform.position;
            //newPosition.z += animator.GetFloat("WalkSpeed") * Time.deltaTime;
            //transform.position = newPosition;

            //Debug.Log(newPosition);

        }
    }
}
