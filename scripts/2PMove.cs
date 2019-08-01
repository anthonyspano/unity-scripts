using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoPMove : MonoBehaviour {

    public float moveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
        }
    }
}
