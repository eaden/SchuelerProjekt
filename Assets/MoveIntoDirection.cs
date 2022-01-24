using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIntoDirection : MonoBehaviour
{
    GameObject otherOne;
    Vector3 startposition;
    Vector3 u;
    Vector3 unorm;
    Vector3 v;
    Vector3 vnorm;
    bool ansturm = true;
    bool kurzzurueck = false;
    float kurzzurueckTimer = 0.15f;
    float kurzzurueckTimerLimit = 0.15f;
    bool stoppen = false;
    bool rueckzug = false;
    float stoppenTimer = 0.5f;
    float stoppenTimerLimit = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        otherOne = GameObject.Find("OtherSprite");
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        u = otherOne.transform.position - transform.position;
        unorm = u.normalized;
        if(rueckzug)
        {
            v = startposition- transform.position;
            vnorm = v.normalized;
            transform.Translate(vnorm * Time.deltaTime *5);
            if (v.magnitude < 0.5f)
            {
                rueckzug = false;
            }
        }
        if(stoppen)
        {
            stoppenTimer -= Time.deltaTime;
            if(stoppenTimer < 0)
            {
                stoppen = false;
                stoppenTimer = stoppenTimerLimit;
                rueckzug = true;
            }
        }
        if (kurzzurueck)
        {
            transform.Translate(-unorm * Time.deltaTime*5);
            kurzzurueckTimer -= Time.deltaTime;
            if(kurzzurueckTimer < 0)
            {
                kurzzurueck = false;
                kurzzurueckTimer = kurzzurueckTimerLimit;
                stoppen = true;
            }
        }
        if (ansturm)
        {
            transform.Translate(unorm * Time.deltaTime*7);
            if (u.magnitude < 0.5f)
            {
                ansturm = false;
                kurzzurueck = true;
            }

        }
    }
}
