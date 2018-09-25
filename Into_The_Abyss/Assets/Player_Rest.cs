using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rest : MonoBehaviour {

    private bool isInRest;


	// Use this for initialization
	void Start () {
        isInRest = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && isInRest)
            gameObject.GetComponent<Player_Health>().ToggleRest();

        else if (Input.GetKeyDown(KeyCode.F))
            gameObject.GetComponent<Player_Health>().Eat();

        else if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Attack_Script>().ThrowOBJ();
        }

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RestArea"))
            isInRest = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RestArea"))
            isInRest = false;
    }
}
