using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TBI:
 * optimize ResetAnims in ComboDir
 * make template
 * events/delegates?
 */

[RequireComponent(typeof(ComboDir))]
public class ActionCancel : MonoBehaviour
{
    public string axisName;

    private ComboDir CD;

    private void Start()
    {
        CD = GetComponent<ComboDir>();
    }


    public bool Cancel()
    {
        //if(Input.GetAxis(axisName) > 0)
        if (Input.GetKeyDown(KeyCode.P))
        {
            // disable hitbox
            CD.StopCoroutine("Attack");
            // stop player anims
            CD.ResetPlayerState();
            return true;
        }
        else
            return false;
    }

}
