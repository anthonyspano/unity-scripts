using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moving a character in a top-down game
public class TopDownWalk : MonoBehaviour
{
    private Animator myAnim;
    private Vector2 lastMove;
    private bool playerMoving;
    private Vector3 moveDir;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float deadZone;

    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        // start player facing down
        lastMove.y = -1.0f; 
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        playerMoving = false;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(horizontal * Time.deltaTime * moveSpeed, vertical * Time.deltaTime * moveSpeed, 0f);
        transform.Translate(moveDir);

        if (horizontal > deadZone || horizontal < -deadZone)
        {
            playerMoving = true;
            lastMove = new Vector2(horizontal, 0f);
        }

        if (vertical > deadZone || vertical < -deadZone)
        {
            playerMoving = true;
            lastMove = new Vector2(0f, vertical);

        }

        // Animation controls
        myAnim.SetFloat("MoveX", horizontal);
        myAnim.SetFloat("MoveY", vertical);
        myAnim.SetBool("PlayerMoving", playerMoving);
        myAnim.SetFloat("LastMoveX", lastMove.x);
        myAnim.SetFloat("LastMoveY", lastMove.y);
    }
}
