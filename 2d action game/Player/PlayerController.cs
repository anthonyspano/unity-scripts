using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* TBI:
 * 
 * 
 * 
 */


// composition
[RequireComponent(typeof(TopDownWalk))]
[RequireComponent(typeof(ComboDir))]
[RequireComponent(typeof(ActionCancel))]
public class PlayerController : MonoBehaviour
{
    ComboDir m_Combo;
    TopDownWalk m_Walk;
    ActionCancel m_Cancel;

    public float dashSpeed;

    void Start()
    {
        m_Walk = gameObject.GetComponent<TopDownWalk>();
        m_Combo = gameObject.GetComponent<ComboDir>();
        m_Cancel = gameObject.GetComponent<ActionCancel>();
    }

    void Update()
    {
        if(m_Cancel.Cancel())
        {
            // dash
            m_Walk.AttackMotion(dashSpeed);
        }
    }

}
