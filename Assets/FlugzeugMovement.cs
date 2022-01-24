using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlugzeugMovement : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    Vector3 zielPos;
    bool posEingeholt = false;
    bool sichtbar = false;
    bool startErlaubnis = false;
    float startDelay = 0;
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
            if (!posEingeholt)
            {
                posEingeholt = true;
                startDelay = Random.Range(0,6)/2+0.25f;
            }
        }
        if (posEingeholt)
        {
            if (pos.x > 0 && pos.x < 1)
                sichtbar = true;
            if (sichtbar && (pos.x < 0 || pos.x > 1))
                Destroy(gameObject);
        }

    }
    private void FixedUpdate()
    {
        if (posEingeholt)
        {
            if(!startErlaubnis)
            {
                startDelay -= Time.deltaTime;
                if (startDelay < 0)
                    startErlaubnis = true;
            }
            if(startErlaubnis)
            {
                if(gameObject.CompareTag("Respawn"))
                    transform.Translate(new Vector3(-5, 3, 0) * Time.deltaTime, Space.World);
                else
                    transform.Translate(new Vector3(5, 3, 0) * Time.deltaTime, Space.World);
            }
                
        }

    }
}
