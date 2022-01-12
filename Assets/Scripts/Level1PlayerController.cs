using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isBurning = false;
    public bool IsBurning
    {
        get { return isBurning; }
        set
        {
            if(isBurning && value)
            {
                Debug.Log("Kaputt");    
            }
            isBurning = value;
            if(isBurning == true)
            {
                fire.enabled = true;
            }                
        }
    }
    [SerializeField]
    private bool isBoosting = false;
    public bool IsBoosting
    {
        get { return isBoosting; }
        set
        {
            isBoosting = value;
            if (isBoosting == true)
            {
                boost.enabled = true;
                boostTime = boostTimeLimit;
            }
            else
            {
                boost.enabled = false;
            }

        }
    }
    SpriteRenderer fire;
    SpriteRenderer boost;
    Rigidbody2D rigid;
    bool jump = false;
    bool destroyed = false;
    float jumpTime = 0f;
    float jumpTimeLimit = 0.5f;
    float boostTime = 1.0f;
    float boostTimeLimit = 1.0f;

    bool goingLeft = false;
    bool goingRight = false;
    bool goingDown = false;
    bool goingUp = false;

    float horizontalSpeed = 2f;
    float verticalSpeed = 2f;



    // Start is called before the first frame update
    void Start() 
    {
        rigid = GetComponent<Rigidbody2D>();
        foreach (Transform ts in transform)
        {
            if (ts.name == "Fire")
                fire = ts.gameObject.GetComponent<SpriteRenderer>();
            if (ts.name == "Boost")
                boost = ts.gameObject.GetComponent<SpriteRenderer>();
        }
        fire.enabled = false;
        boost.enabled = false;
        //fire = GetComponentInChildren<>
    }

    // Update is called once per frame
    void Update()
    {
        /* // wird nicht verwendet -> kein Flappy Bird
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            IsBoosting = true;
        }
        if(jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;
        }
        */
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
        rigid.velocity = new Vector2(0f, 0f);
        if (goingLeft && !goingRight)
            rigid.velocity = new Vector2(-horizontalSpeed, rigid.velocity.y);
        if (!goingLeft && goingRight)
            rigid.velocity = new Vector2(horizontalSpeed, rigid.velocity.y);
        if (goingDown && !goingUp)
            rigid.velocity = new Vector2(rigid.velocity.x, -verticalSpeed + 0.5f);
        if (!goingDown && goingUp)
            rigid.velocity = new Vector2(rigid.velocity.x, verticalSpeed + 0.5f);

        /* // wird nicht verwendet -> kein Flappy Bird
        if(IsBoosting)
        {
            boostTime -= Time.deltaTime;
            if (boostTime < 0)
            {
                IsBoosting = false;
                Debug.Log("Burning vorbei");
            }
        }
        if(jumpTime <= 0)
        {
            rigid.velocity = new Vector2(1f, -0.6f);
        }
        if(jump)
        {
            Debug.Log("Junp");
            jumpTime = jumpTimeLimit;
            rigid.AddForce(new Vector2(0.0f, 4.8f), ForceMode2D.Impulse);
            jump = false;
        }
        */
    }
}
