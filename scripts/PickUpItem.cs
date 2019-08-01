using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

    public Item item;
    public Transform anchor;
    private bool isGrabbed = false;
    [SerializeField]
    private float power;
    public string toss;
    public string grab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetAxisRaw(toss) == 1 && isGrabbed)
        {
            Throw();
        }

        if (isGrabbed)
        {            
            item.transform.parent = transform;
            item.transform.position = anchor.position;
        }
    }

    void Grab()
    {
        isGrabbed = true;
        item.isTriggered = true;
    }

    void Throw()
    {
        // add force in direction of facing
        isGrabbed = false;
        item.isThrown = true;
        item.transform.parent = null;
        item.rb.AddForce(new Vector2(power * transform.localScale.x, 0f), ForceMode2D.Impulse);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (Input.GetAxisRaw(grab) == 1)
                Grab();
        }
    }
}
