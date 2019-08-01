using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script for executing a basic 3-hit combo system
 * TODO:
 * polish
 */

public class ComboAttack : MonoBehaviour
{
    float attackTimer;
    float attackRate;
    int size;
    GameObject[] enemies;
    bool attackingDown = false;
    Animator myAnim;
    float lastMoveX;
    float lastMoveY;
    Transform strikeN;
    Transform strikeS;
    Transform strikeE;
    Transform strikeW;

    [SerializeField]
    float boostSpeed;


    private enum CurrentState { NoAttack, First, Last };
    [SerializeField]
    private CurrentState AttackState;
    private bool stopTimerReset;

    public bool[] Stack;
    private bool combo;
    [SerializeField]
    private float comboTimer;
    public float comboSpeed;
    private int comboCount;
    private int maxCombo;

    private bool isSameState;
    private Animator anim;
    public Animation animated;
    private Func<bool> fun;

    private float x;
    private float y;

    public float deadZone;
    private AnimatorClipInfo[] myClips;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        maxCombo = 3;
        Stack = new bool[maxCombo];
        strikeN = transform.Find("Attacks/StrikeN");
        strikeS = transform.Find("Attacks/StrikeS");
        strikeE = transform.Find("Attacks/StrikeE");
        strikeW = transform.Find("Attacks/StrikeW");
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
            if (x > deadZone)
                lastMoveX = 1;
            else
                lastMoveX = -1;
        }

        else if (y > deadZone || y < -deadZone)
        {
            if (y > deadZone)
                lastMoveY = 1;
            else
                lastMoveY = -1;
        }


        // set timer for combo
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


        // if attack1
        // enum here to disable repeats and mark last state
        if (Stack[0])
        {
            //AttackMotion(1, 0);
            // keep from repeating coroutine
            Stack[0] = false;
            StartCoroutine(Attack("attack1"));
        }


        // set bool stack to true
        if (Input.GetKeyDown(KeyCode.K))
        {
            // enables only one boost per attack
            if (!isSameState)
            {
                //AttackMotion(1, 0);
                isSameState = true;
            }

            // reset timer on key press unless max stacks has been reached
            if(Stack[2] == false)
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
        if (lastMoveX == -1f)
        {
            // start animation - the layer that calls this function
            anim.SetBool(attackStep, true);
            strikeW.gameObject.SetActive(true);
            // wait until the current animation playing is finished
            //anim.GetCurrentAnimatorStateInfo
            Debug.Log(anim.GetLayerIndex("attack_1"));
            yield return new WaitUntil(() => !animated.IsPlaying(attackStep));
            // end of animation
            strikeW.gameObject.SetActive(false);
            // clear the animation state when done
            anim.SetBool(attackStep, false);
        }

        if (lastMoveX == 1f)
        {
            // start animation - the layer that calls this function
            anim.SetBool(attackStep, true);
            strikeE.gameObject.SetActive(true);
            // wait until the current animation playing is finished
            //yield return new WaitForSeconds(0.3f);
            
            // slight delay for the animator to change state
            yield return null;
            // retrieve the animation clip from the animator
            myClips = anim.GetCurrentAnimatorClipInfo(0);

            //ebug.Log("length: " + myClips[0].clip.length);
            //yield return new WaitForSeconds(0.4f);
            yield return new WaitUntil(() => !animated.IsPlaying(myClips[0].clip.name));
            // end of animation
            strikeE.gameObject.SetActive(false);
            // clear the animation state when done
            anim.SetBool(attackStep, false);
        }

        else if (lastMoveY == 1f)
        {
            // start animation - the layer that calls this function
            anim.SetBool(attackStep, true);
            strikeN.gameObject.SetActive(true);
            // wait until the current animation playing is finished
            yield return new WaitUntil(() => !animated.IsPlaying(attackStep));
            // end of animation
            strikeN.gameObject.SetActive(false);
            // clear the animation state when done
            anim.SetBool(attackStep, false);
        }

        else
        {
            //attackingDown = true;
            // start animation - the layer that calls this function
            anim.SetBool(attackStep, true);
            strikeS.gameObject.SetActive(true);
            // wait until the current animation playing is finished
            yield return new WaitUntil(() => !animated.IsPlaying(attackStep));
            // end of animation
            strikeS.gameObject.SetActive(false);
            // clear the animation state when done
            anim.SetBool(attackStep, false);
        }

    }


    IEnumerator AttackDir(GameObject strike)
    {
        strike.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        //yield return null;
        strike.SetActive(false);

        // TODO: implement animations for other directions
        // strike downward animation
        attackingDown = false;
    }

    private void AttackMotion(float x, float y)
    {
        // sudden forward motion when attacking; 20 seems like a good number
        float boostX = x * boostSpeed;
        float boostY = y * boostSpeed;
        // move in that direction
        transform.Translate(new Vector3(boostX, boostY, 0));
    }

    private void Hide()
    {
        strikeN.gameObject.SetActive(false);
        strikeS.gameObject.SetActive(false);
        strikeE.gameObject.SetActive(false);
        strikeW.gameObject.SetActive(false);
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
        Debug.Log("third done");
        stopTimerReset = true;
        AttackState = CurrentState.NoAttack;
    }
}

