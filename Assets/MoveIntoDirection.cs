using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIntoDirection : MonoBehaviour
{
    bool started = false;
    GameObject firstOne;
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
    float stoppenTimer = 0.3f;
    float stoppenTimerLimit = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LetsMove(GameObject first, GameObject second)
    {
        firstOne = first;
        startposition = firstOne.transform.position;
        otherOne = second;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            u = otherOne.transform.position - firstOne.transform.position;
            unorm = u.normalized;
            if (rueckzug)
            {
                v = startposition - firstOne.transform.position;
                vnorm = v.normalized;
                firstOne.transform.Translate(vnorm * Time.deltaTime * 6);
                if (v.magnitude < 0.2f)
                {
                    rueckzug = false;
                    firstOne.transform.position = startposition;
                    started = false;
                }
            }
            if (stoppen)
            {
                stoppenTimer -= Time.deltaTime;
                if (stoppenTimer < 0)
                {
                    stoppen = false;
                    stoppenTimer = stoppenTimerLimit;
                    rueckzug = true;
                }
            }
            if (kurzzurueck)
            {
                firstOne.transform.Translate(-unorm * Time.deltaTime * 7);
                kurzzurueckTimer -= Time.deltaTime;
                if (kurzzurueckTimer < 0)
                {
                    kurzzurueck = false;
                    kurzzurueckTimer = kurzzurueckTimerLimit;
                    stoppen = true;
                }
            }
            if (ansturm)
            {
                firstOne.transform.Translate(unorm * Time.deltaTime * 12);
                if (u.magnitude < 0.5f)
                {
                    ansturm = false;
                    kurzzurueck = true;
                }

            }
        }

    }
}
