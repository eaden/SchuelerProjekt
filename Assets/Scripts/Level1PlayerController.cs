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
            // hier soll dann entweder eine kaputte oder eine brennende Stelle am Schiff dargestellt werden
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
    SpriteRenderer fire;
    Rigidbody2D rigid;
    bool destroyed = false;

    bool goingLeft = false;
    bool goingRight = false;
    bool goingDown = false;
    bool goingUp = false;

    float horizontalSpeed = 2f;
    float verticalSpeed = 2f;

    void Start() 
    {
        rigid = GetComponent<Rigidbody2D>();
        foreach (Transform ts in transform)
        {
            if (ts.name == "Fire")
                fire = ts.gameObject.GetComponent<SpriteRenderer>();
        }
        fire.enabled = false;
    }

    void Update()
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

    private void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        rigid.velocity = new Vector2(0f, 0.5f);
        if (goingLeft && !goingRight && (pos.x > 0.02f))
            rigid.velocity = new Vector2(-horizontalSpeed, rigid.velocity.y);
        if (!goingLeft && goingRight && (pos.x < 0.98f))
            rigid.velocity = new Vector2(horizontalSpeed, rigid.velocity.y);
        if (goingDown && !goingUp && (pos.y > 0.05f))
            rigid.velocity = new Vector2(rigid.velocity.x, -verticalSpeed + 0.5f);
        if (!goingDown && goingUp && (pos.y < 0.95f))
            rigid.velocity = new Vector2(rigid.velocity.x, verticalSpeed + 0.5f);
    }
    
}
