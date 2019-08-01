using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;
using CodeMonkey.Utils;

public class HealthScript : MonoBehaviour {

    // health bar
    private Vector2 buttonLocation;
    public HealthBar healthBar;
    private HealthSystem healthSystem;
    public Button healthB;
    // health bar prefab
    //public Transform pfHealthBar;


    // Use this for initialization
    void Start () {
        
        buttonLocation = new Vector2(transform.position.x, transform.position.y + 100f);
        // health bar
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        Debug.Log("Player base health: " + healthBar.healthSystem.GetHealth());
        // health bar debug
        // standard UI 
        healthBar.healthSystem.Damage(10);
        Debug.Log("Current: " + healthBar.healthSystem.GetHealth());
        healthB.onClick.AddListener(() => {
            healthBar.healthSystem.Damage(10);
            Debug.Log("Current: " + healthBar.healthSystem.GetHealth());

        });
        // green button 
        CMDebug.ButtonUI(buttonLocation, "damage", () => {
            healthBar.healthSystem.Damage(10);
            Debug.Log("Current: " + healthBar.healthSystem.GetHealth());

        });
    }
	
	// Update is called once per frame
	void Update () {

    }
}
