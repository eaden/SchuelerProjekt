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
            isBurning = value;
            if(isBurning == true)
                fire.enabled = true;
        }
    }
    SpriteRenderer fire;
    Rigidbody2D rigid;
    bool jump = false;
    float jumpTime = 0f;
    float jumpTimeLimit = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        foreach (Transform ts in transform)
        {
            if (ts.name == "Fire")
                fire = ts.gameObject.GetComponent<SpriteRenderer>();
        }
        fire.enabled = false;
        
        //fire = GetComponentInChildren<>
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if(jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
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
    }
}
