using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    Vector3 zielPos;
    bool posEingeholt = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("DerSpieler");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        // when Spieler in Sichtfeld ist
        if (pos.y < 1)
        {
            if(!posEingeholt)
            {
                playerPos = player.transform.position;
                zielPos = (playerPos - transform.position).normalized;
                posEingeholt = true;
            }
            else
            {
            }
        }

    }
    private void FixedUpdate()
    {
        if(posEingeholt)
        {
            transform.Translate(zielPos * Time.deltaTime*3, Space.World);
            transform.Translate(new Vector3(0, 3, 0) * Time.deltaTime, Space.World);
        }
            
    }
}
