using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Script : MonoBehaviour {

    private Animator myAnimator;

    private int maxAmmo = 5;
    public int ammoCount;

	// Use this for initialization
	void Start () {
        myAnimator = gameObject.GetComponent<Animator>();
        ammoCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ThrowOBJ()
    {
        if(ammoCount > 0)
        {
            myAnimator.SetTrigger("Throw");
            ammoCount--;

        }
    }
}
