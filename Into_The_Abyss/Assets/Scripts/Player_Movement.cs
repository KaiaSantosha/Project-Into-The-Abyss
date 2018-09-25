using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    int layerMask = 1 << 8;

    private bool isGrounded;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D myRigidbody2D;
    private Vector2 Movement = new Vector2(0, 0);

    public GameObject GroundCheck;
    public GameObject HeadCheck;
    public GameObject InteractCheck;
    public LayerMask Ground;
    public LayerMask InteractablesLayer;

    private Animator myAnimator;

    private SpriteRenderer mySpriteRendeer;


    private float h;
    private bool isClimbing;

    private Vector2 direction;

	// Use this for initialization
	void Start () {
        myAnimator = gameObject.GetComponent<Animator>();
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        mySpriteRendeer = gameObject.GetComponent<SpriteRenderer>();
        myRigidbody2D.gravityScale = 1;
        myRigidbody2D.angularDrag = 0;
        isClimbing = false;
	}
	
	// Update is called once per frame
	void Update () {

        Move();
        if (Physics2D.Raycast(GroundCheck.transform.position, Vector2.down, 0.01f, Ground))
        {
            myAnimator.SetBool("isGrounded", true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                myAnimator.SetTrigger("Pickup");
            }

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Stealth();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else
                myAnimator.SetBool("canJump", false);
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
            myAnimator.SetBool("canJump", false);
        }
    }

    void Stealth()
    {
        if (mySpriteRendeer.sortingOrder == 1)
            mySpriteRendeer.sortingOrder = -1;
        else if (mySpriteRendeer.sortingOrder == -1)
            mySpriteRendeer.sortingOrder = 1;
    }

    public void InteractOBJ()
    {
        if (Physics2D.Raycast(InteractCheck.transform.position, direction, 1.0f, InteractablesLayer).collider != null)
        {
            GameObject InteractedOBJ = Physics2D.Raycast(InteractCheck.transform.position,direction, 1.0f, InteractablesLayer).collider.gameObject;
            InteractedOBJ.SetActive(false);
            gameObject.GetComponent<Attack_Script>().ammoCount++;
        }
           
        //var DetectedCols = Physics2D.RaycastAll(InteractCheck.transform.position, Vector2.right, 1.0f, InteractablesLayer);
        //List<GameObject> PossibleObjects = new List<GameObject>();
        //foreach (var detection in DetectedCols)
        //{
        //    PossibleObjects.Add(detection.collider.gameObject);
        //}
    }

    void Move()
    {
        if(isClimbing == false)
        {
            float InputHorizontal = Input.GetAxisRaw("Horizontal");
            h = InputHorizontal * speed;
           // Debug.Log(myRigidbody2D.velocity);
            Movement = Vector2.Lerp(myRigidbody2D.velocity, new Vector2(h, myRigidbody2D.velocity.y), 0.4f);

            if (InputHorizontal != 0)
                myAnimator.SetBool("IsMoving", true);
            else
                myAnimator.SetBool("IsMoving", false);

            if (InputHorizontal > 0)
            {
                Quaternion rotation = new Quaternion(0, 0, 0, 0);
                transform.rotation = rotation;
                InteractCheck.transform.rotation = new Quaternion(0, 0, 0, 0);
                direction = Vector2.right;
            }
            else if (InputHorizontal < 0)
            {
                Quaternion rotation = new Quaternion(0, 180, 0, 0);
                transform.rotation = rotation;
                InteractCheck.transform.rotation = new Quaternion(0, 180, 0, 0);
                direction = Vector2.left;
            }
        }
        myRigidbody2D.velocity = Movement;
    }

    void Jump()
    {
        if (Physics2D.Raycast(HeadCheck.transform.position, Vector2.up, 1f, Ground).collider != null)
            return;

        else if(isClimbing == false)
        {
            myAnimator.SetBool("canJump", true);
            Movement = new Vector2(myRigidbody2D.velocity.x, jumpSpeed);
            myRigidbody2D.velocity = Movement;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.Space) && other.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("InCollider");
            isClimbing = true;
            this.transform.Translate(new Vector2(0, speed * Time.deltaTime));
            //  Movement = Vector2.Lerp(myRigidbody2D.velocity, new Vector2(0, speed /** Time.deltaTime*/), 0.4f);
        }
        else
        {
            isClimbing = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("OutCollider");
            isClimbing = false;
            //   Movement = Vector2.Lerp(myRigidbody2D.velocity, new Vector2(0, 0 /** Time.deltaTime*/), 0.4f);
            //  myRigidbody2D.velocity = Movement;


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(InteractCheck.transform.position, direction);
    }
}
