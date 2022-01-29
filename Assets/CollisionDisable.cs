using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDisable : MonoBehaviour
{
    float collisionTimer = 0.2f;
    float collisionTimerLimit = 0.2f;
    bool collided = false;
    bool deactivateCollider = false;
    Collider2D currentCollider;
    // Start is called before the first frame update
    void Start()
    {
        // den aktuellen Collider ermitteln
        currentCollider = GetComponent<BoxCollider2D>();
        if(currentCollider == null)
            currentCollider = GetComponent<CircleCollider2D>();
        if (currentCollider == null)
            currentCollider = GetComponent<CapsuleCollider2D>();
        if (currentCollider == null)
            currentCollider = GetComponent<PolygonCollider2D>();
        if (currentCollider == null)
        {
            Debug.Log("Hmm hier ist was schief gegangen.");
            Debug.Log(gameObject.name);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if(collided)
        {
            if(!deactivateCollider)
            {
                currentCollider.isTrigger = true;
                deactivateCollider = true;
            }
            collisionTimer -= Time.deltaTime;
            if(collisionTimer < 0)
            {
                currentCollider.isTrigger = false;
                collided = false;
                deactivateCollider = false;
                collisionTimer = collisionTimerLimit;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
    }
}
