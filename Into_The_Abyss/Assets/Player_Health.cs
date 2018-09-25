using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour {

    [SerializeField] private int maxHunger = 10;
    private int curHunger;

    [SerializeField] private int maxRest = 100;
    private int curRest;

    [SerializeField] private int maxHealth = 100;
    private int curHealth;

    [SerializeField] private float restTimer = 15;
    private float curTime;

    public Slider healthSlider;
    public Slider restSlider;
    public Slider hungerSlider;

    private Animator myAnimator;

    public int curFood = 1;

    // Use this for initialization
    void Start () {
        curHunger = maxHunger;
        curRest = maxRest;
        curHealth = maxHealth;

        myAnimator = gameObject.GetComponent<Animator>();
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

        if (curHealth <= 0 || curHunger <= 0 || curRest <= 0)
        {
            Die();
        }
    }

    private void OnGUI()
    {
        healthSlider.value = curHealth;
        hungerSlider.value = curHunger;
        restSlider.value = curRest;
    }

    void Die()
    {
        myAnimator.SetTrigger("Death");
    }

    public void Despawn()
    {
        Destroy(gameObject);
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

    public void ToggleRest()
    {
        if (myAnimator.GetBool("isResting") == false)
        {
            myAnimator.SetBool("isResting", true);
            Debug.Log("REST");
            if (curRest < maxRest)
            {
                curRest = maxRest;
                LoseHunger();
                curTime = 0;
            }
        }
        else if (myAnimator.GetBool("isResting") == true)
            myAnimator.SetBool("isResting", false);


    }

    public void StopRest()
    {

    }

    public void Eat()
    {
        myAnimator.SetTrigger("Eat");
        if(curFood > 0)
        {
            curFood--;
            curHunger = maxHunger;
        }
    }
}
