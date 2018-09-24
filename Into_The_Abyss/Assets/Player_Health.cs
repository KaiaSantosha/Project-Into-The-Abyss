using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour {

    [SerializeField] private int maxHunger = 10;
    private int curHunger;

    [SerializeField] private int maxRest = 100;
    private int curRest;

    [SerializeField] private int maxHealth = 100;
    private int curHealth;

    [SerializeField] private float restTimer = 15;
    private float curTime;

    // Use this for initialization
    void Start () {
        curHunger = maxHunger;
        curRest = maxRest;
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;

        if (curTime >= restTimer)
        {
            LoseRest();
            curTime = 0;
        }

        curHunger = Mathf.Clamp(curHunger, 0, maxHunger);
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        curRest = Mathf.Clamp(curRest, 0, maxRest);
       // Debug.Log(curRest);
        Debug.Log(curHunger);
    }

    void LoseHealth()
    {
        curHealth -= 1;
    }

    void LoseHunger()
    {
        curHunger -= 4;
    }

    void LoseRest()
    {
        curRest -= 1;
    }

    public void Rest()
    {
        Debug.Log("REST");
        if(curRest < maxRest)
        {
            curRest = maxRest;
            LoseHunger();
            curTime = 0;
        }

    }

    public void Eat()
    {
        curHunger = maxHunger;
    }
}
