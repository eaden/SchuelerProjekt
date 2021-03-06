using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isBurning = false;
	[SerializeField]
	private int deathCamTime = 8;
	[SerializeField]
	private float touchMargin = 0.01f; // Bereich um Klick / touch welcher ignoriert wird um wackeln der Rackete zu vermeiden
    private int damage = 0;
    private bool invulnerable = false;
    float invulnerableTimer = 2.5f;
    float invulnerableTimerLimit = 2.5f;
    int explosionCounter = 0;
    float explosionTimer = 0.3f;
    float explosionTimerLimit = 0.3f;

    bool landeSound = false;
    float landeTimer = 5f;
    bool fadeStart1 = false;
    bool fadeStart2 = false;

    List<SpriteRenderer> schiffRenderer = new List<SpriteRenderer>();
    List<SpriteRenderer> explosionsRenderer = new List<SpriteRenderer>();

    SpriteRenderer sr;
    public void PlayerHitSomething()
    {

        //Debug.Log("Schaden erhalten");
        if(damage < 1)
        {
            AudioManager.Instance.Play2("explosionKurz");
            explosionsRenderer[0].enabled = true;
            StartCoroutine(FadeTo(0.0f, 1.0f, explosionsRenderer[0]));
            explosionCounter++;
        }
        else
            AudioManager.Instance.Play2("explosionDoppel");

        IsBurning = true;
        damage++;
        invulnerable = true;
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
        GameManager.Instance.levelFortschritt = 2;
        AudioManager.Instance.SetLoop3(true);
        AudioManager.Instance.Play3("gesangEins");

		cameraMovement = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Level1CameraMovement>();
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "SpielerSchiffKaputt")
            {
                GameObject spielerSchiffKaputt = transform.GetChild(i).gameObject;
                for (int k = 0; k < spielerSchiffKaputt.transform.childCount; k++)
                {
                    schiffRenderer.Add(spielerSchiffKaputt.transform.GetChild(k).GetComponent<SpriteRenderer>());
                }
            }
            if (transform.GetChild(i).name == "SpielerExplosionen")
            {
                GameObject spielerExplosionen = transform.GetChild(i).gameObject;
                for (int k = 0; k < spielerExplosionen.transform.childCount; k++)
                {
                    explosionsRenderer.Add(spielerExplosionen.transform.GetChild(k).GetComponent<SpriteRenderer>());
                }
            }
        }
        
        /*
        foreach (Transform ts in transform)
        {
            if (ts.name == "Fire")
                fire = ts.gameObject.GetComponent<SpriteRenderer>();
        }
        fire.enabled = false;
        */
    }

    IEnumerator FadeTo(float aValue, float aTime, SpriteRenderer renderer)
    {
        float alpha = renderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            renderer.material.color = newColor;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("Collision Enter: " + col.gameObject.name);
		if (col.gameObject.tag == "Finish") {
			cameraMovement.Stop = true;
			var contact = col.GetContact(0);
			var norm = contact.normal;
			landing = new DirectedPosition(contact.point - norm * 0.3f, new Vector2(-norm.y, norm.x));
		}
        if(col.gameObject.tag != "Finish" && !invulnerable)
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
		/// should also works for touch input :)
		/// https://stackoverflow.com/questions/22459112/in-unity3d-click-touch
		if (Input.GetMouseButton(0))
		{
			Vector3 fingerPos = Input.mousePosition;
			fingerPos.x /= Screen.width;
			fingerPos.y /= Screen.height;
            Vector3 rocketPos = Camera.main.WorldToViewportPoint(transform.position);

			goingLeft = false;
			goingRight = false;
			goingDown = false;
			goingUp = false;

			if (fingerPos.x - rocketPos.x > touchMargin) {
				goingRight = true;
			} else if(fingerPos.x - rocketPos.x < -touchMargin){
				goingLeft = true;
			}

			if (fingerPos.y - rocketPos.y > touchMargin) {
				goingUp = true;
			} else if (fingerPos.y - rocketPos.y < -touchMargin){
				goingDown = true;
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			goingRight = false;
			goingLeft = false;
			goingUp = false;
			goingDown = false;
		}
		else
		{
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
	}


    private void FixedUpdate()
    {
        if (destroyed != null)
        {
            explosionTimer -= Time.deltaTime;
            if (explosionTimer < 0)
            {
                explosionTimer = explosionTimerLimit;
                explosionCounter++;
                if (explosionCounter < 5)
                {
                    explosionsRenderer[explosionCounter].enabled = true;
                    StartCoroutine(FadeTo(0.0f, 0.6f, explosionsRenderer[explosionCounter]));
                    if (explosionCounter == 2)
                    {
                        sr.enabled = false;
                        schiffRenderer[0].enabled = true;
                    }
                    if (explosionCounter == 4)
                    {
                        schiffRenderer[0].enabled = false;
                        schiffRenderer[1].enabled = true;
                    }
                }
                else
                {
                    if(!fadeStart1)
                    {
                        FadeInOut.Instance.FadeBlackIn(1.8f);
                        fadeStart1 = true;
                    }
                }
            }
            explosionsRenderer[1].enabled = true;
            StartCoroutine(FadeTo(0.0f, 0.6f, explosionsRenderer[1]));
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Feuersteuerung>().ShipWasDestroyed();

            if ((System.DateTime.Now - destroyed)?.TotalSeconds > deathCamTime)
            {
                SceneManager.LoadScene("IntroScene");
            }
            return;
        }
        if(landeSound)
        {
            landeTimer -= Time.deltaTime;
            if(landeTimer < 2)
            {
                if(!fadeStart2)
                {
                    FadeInOut.Instance.FadeBlackIn(1.8f);
                    fadeStart2 = true;
                }
            }
            if(landeTimer < 0)
                SceneManager.LoadScene("AnklickenSpiel");
        }
        if (landing != null)
        {
            if(!landeSound)
            {
                AudioManager.Instance.Play1("superWirSindGelandet");
                AudioManager.Instance.SetLoop3(false);
                AudioManager.Instance.Stop3();
                landeSound = true;
            }
            var landingPoint = landing ?? new DirectedPosition(Vector2.zero, Vector2.zero);
            rigid.velocity = Vector2.zero;
            transform.position = landingPoint.Point;
            transform.rotation = Quaternion.FromToRotation(transform.up, landingPoint.Normal) * transform.rotation;
            return;
        }
        if (destroyed == null)
        {
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
}
