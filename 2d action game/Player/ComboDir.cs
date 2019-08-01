using System;
//using System.Threading;
using System.Collections;
//using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;


/* Script for executing a basic 3-hit combo system
 * TODO:
 * Insert temp anims for each direction, name them appropriately
 * add dash to each attack
 * dash resets cooldown
 * 
 * 
 * enum of direction
 * depending on enum, different call
 */

public class ComboDir : MonoBehaviour
{
    float attackTimer;
    float attackRate;
    
    float lastMoveX;
    float lastMoveY;


    public enum CurrentState { NoAttack, First, Last };
    public CurrentState AttackState;
    bool stopTimerReset;

    public bool[] Stack;
    bool combo;
    [SerializeField]
    float comboTimer;
    public float comboSpeed;
    int comboCount;
    int maxCombo;

    bool isSameState;
    Animator anim;
    public Animation animated;

    float x;
    float y;

    public float deadZone;
    AnimatorClipInfo myClip;

    Transform strike;

    string attackAnim;
    AnimatorClipInfo[] myClips;

 

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        maxCombo = 3;
        Stack = new bool[maxCombo];
        strike = transform.Find("Strike");
        attackTimer = attackRate;
        Hide();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (x > deadZone || x < -deadZone)
        {
            lastMoveY = 0;
            if (x > deadZone)
                lastMoveX = 1;
            else
                lastMoveX = -1;
        }

        else if (y > deadZone || y < -deadZone)
        {
            lastMoveX = 0;
            if (y > deadZone)
                lastMoveY = 1;
            else
                lastMoveY = -1;
        }

        

        // combo input tracker
        comboTimer -= Time.deltaTime;
        if (comboTimer <= 0)
        {
            comboCount = 0;
            comboTimer = comboSpeed;
            isSameState = false;
            // reset the stack
            for (int i = 0; i < Stack.Length; i++)
            {
                Stack[i] = false;
            }
        }

        // enum here to disable repeats and mark last state
        if (Stack[0])
        {
            // keep from repeating coroutine
            Stack[0] = false;
            
            attackAnim = "attack1";
            StartCoroutine(Attack(attackAnim));
        }


        // Pressing K fills a bool[] up to a point
        if (Input.GetKeyDown(KeyCode.K))
        {
            // enables only one boost per attack
            if (!isSameState)
            {
                isSameState = true;
            }

            // reset timer on key press unless max stacks has been reached
            if (Stack[maxCombo - 1] == false)
            {
                comboTimer = comboSpeed;
                // how many times the user has committed to the combo
                Stack[comboCount] = true;
                //Debug.Log(comboCount + ": " + Stack[comboCount]);
                comboCount++;
            }

            // stop the combo timer on the last state
            if (AttackState == CurrentState.Last)
            {
                // prevent the combo timer from resetting on 'k' presses
                stopTimerReset = true;
            }
        }


    }


    private IEnumerator Attack(string attackStep)
    {

        // start animation - the layer that calls this function
        anim.SetBool(attackStep, true);
        strike.gameObject.SetActive(true);

        // wait for the clip to play
        yield return null;
        // current animation clip
        myClips = anim.GetCurrentAnimatorClipInfo(0);

        yield return new WaitUntil(() => !animated.IsPlaying(myClips[0].clip.name));
        //yield return new WaitForSeconds(0.1f);

        // reset back to default anim state
        anim.SetBool(attackStep, false);

        strike.gameObject.SetActive(false);

    }


    private void Hide()
    {
        strike.gameObject.SetActive(false);
    }

    private bool canAttack()
    {
        if (attackTimer < 0)
            return true;

        else
        {
            attackTimer -= Time.deltaTime;
            return false;
        }
    }

    // rest of script gets called by animation events
    private void SecondAttack()
    {
        // if next stack index is valid, start the next animation
        if (Stack[1])
        {
            Stack[1] = false;
            StartCoroutine(Attack("attack2"));
            anim.SetBool("attack2", true);
        }

        isSameState = false;

    }

    private void ThirdAttack()
    {
        if (Stack[2])
        {
            StartCoroutine(Attack("attack3"));
            anim.SetBool("attack3", true);
        }

        isSameState = false;

    }

    private void AttackLimit()
    {
        Debug.Log("done");
        stopTimerReset = true;
        AttackState = CurrentState.NoAttack;
    }

    private bool SecondMoveReady()
    {
        if (Stack[0] == false && Stack[1] == true)
        {
            return true;
        }
        else
            return false;

    }

    private bool ThirdMoveReady()
    {
        if (Stack[1] == false && Stack[2] == true)
        {
            return true;
        }
        else
            return false;

    }

    private void TestAttack()
    {
        // if next stack index is valid, start the next animation
        if (Stack[1])
        {
            Stack[1] = false;
            StartCoroutine(Attack("attack2"));
            anim.SetBool("attack2", true);
        }

        isSameState = false;

    }

    public void ResetPlayerState()
    {
        // reset back to default anim state
        for(int i = 1; i <= maxCombo; i++)
        anim.SetBool("attack" + i, false);
        // disable hitbox
        strike.gameObject.SetActive(false);
    }

}


