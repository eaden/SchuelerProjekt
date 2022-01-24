using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isBurning = false;
	[SerializeField]
	private int deathCamTime = 4;
    private int damage = 0;
    private bool invulnerable = false;
    float invulnerableTimer = 2.5f;
    float invulnerableTimerLimit = 2.5f;
    public void PlayerHitSomething()
    {

        Debug.Log("Schaden erhalten");
        IsBurning = true;
        damage++;
        invulnerable = true;
        if(damage > 1)
        {
            // Destruction
        }
    }
    
    public bool IsBurning
    {
        get { return isBurning; }
        set
        {
            // hier soll dann entweder eine kaputte oder eine brennende Stelle am Schiff dargestellt werden
            if(destroyed == null && isBurning && value)
            {
                Debug.Log("Kaputt");    
				cameraMovement.Stop = true;
				destroyed = System.DateTime.Now;
            }
            isBurning = value;
            if(isBurning == true)
            {
                //fire.enabled = true;
            }                
        }
    }
    
    //SpriteRenderer fire;
    Rigidbody2D rigid;
    System.DateTime? destroyed = null;

    bool goingLeft = false;
    bool goingRight = false;
    bool goingDown = false;
    bool goingUp = false;

    float horizontalSpeed = 4f;
    float verticalSpeed = 4f;

	struct DirectedPosition {
		public DirectedPosition(Vector2 point, Vector2 normal)
		{
			Point = point;
			Normal = normal;
		}
		public Vector2 Point {get;}
		public Vector2 Normal {get;}
	};
	DirectedPosition? landing = null;
	Level1CameraMovement cameraMovement;

    void Start() 
    {
		cameraMovement = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Level1CameraMovement>();
        rigid = GetComponent<Rigidbody2D>();
        /*
        foreach (Transform ts in transform)
        {
            if (ts.name == "Fire")
                fire = ts.gameObject.GetComponent<SpriteRenderer>();
        }
        fire.enabled = false;
        */
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("Collision Enter");
		if (col.gameObject.tag == "Finish") {
			cameraMovement.Stop = true;
			var contact = col.GetContact(0);
			landing = new DirectedPosition(contact.point, contact.normal);
		}
        else if(!invulnerable)
        {
            PlayerHitSomething();
        }
	}

    void Update()
    {
        if(invulnerable)
        {
            invulnerableTimer -= Time.deltaTime;
            if(invulnerableTimer < 0)
            {
                invulnerable = false;
                invulnerableTimer = invulnerableTimerLimit;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            goingLeft = true;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            goingLeft = false;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            goingRight = true;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            goingRight = false;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            goingDown = true;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            goingDown = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            goingUp = true;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            goingUp = false;
    }

    private void FixedUpdate()
    {
		if(destroyed != null) {
            GetComponent<BoxCollider2D>().enabled = false;
			if ((System.DateTime.Now - destroyed)?.TotalSeconds > deathCamTime) {
				SceneManager.LoadScene("IntroScene");
			}
			return;
		}
		if (landing != null) {
			var landingPoint = landing ?? new DirectedPosition(Vector2.zero, Vector2.zero);
			rigid.velocity = Vector2.zero;
			transform.position = landingPoint.Point;
			transform.rotation = Quaternion.FromToRotation(transform.up, landingPoint.Normal) * transform.rotation;
			return;
		}
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        rigid.velocity = new Vector2(0f, Level1CameraMovement.camMovementSpeed);
        if (goingLeft && !goingRight && (pos.x > 0.02f))
            rigid.velocity = new Vector2(-horizontalSpeed, rigid.velocity.y);
        if (!goingLeft && goingRight && (pos.x < 0.98f))
            rigid.velocity = new Vector2(horizontalSpeed, rigid.velocity.y);
        if (goingDown && !goingUp && (pos.y > 0.05f))
            rigid.velocity = new Vector2(rigid.velocity.x, -verticalSpeed + Level1CameraMovement.camMovementSpeed);
        if (!goingDown && goingUp && (pos.y < 0.95f))
            rigid.velocity = new Vector2(rigid.velocity.x, verticalSpeed + Level1CameraMovement.camMovementSpeed);
    }
    
}
