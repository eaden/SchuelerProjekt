using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIntoDirection : MonoBehaviour
{
    SteinSchereManager ssm;
    bool started = false;
    GameObject firstOne;
    GameObject otherOne;
    Vector3 startposition;
    Vector3 u;
    Vector3 unorm;
    Vector3 v;
    Vector3 vnorm;
    bool ansturm = false;
    bool kurzzurueck = false;
    float kurzzurueckTimer = 0.15f;
    float kurzzurueckTimerLimit = 0.15f;
    bool stoppen = false;
    bool rueckzug = false;
    float stoppenTimer = 0.15f;
    float stoppenTimerLimit = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        ssm = SteinSchereManager.Instance;
    }

    public void LetsMove(GameObject first, GameObject second)
    {
        firstOne = first;
        startposition = firstOne.transform.position;
        otherOne = second;
        started = true;
        ansturm = true;
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
                firstOne.transform.Translate(vnorm * Time.deltaTime * 10, Space.World);
                if (v.magnitude < 0.2f)
                {
                    rueckzug = false;
                    firstOne.transform.position = startposition;
                    started = false;
                    ssm.moveAnimationFertig = true;  
                }
            }
            /*
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
            */
            /*
            if (kurzzurueck)
            {
                firstOne.transform.Translate(-unorm * Time.deltaTime * 9, Space.World);
                kurzzurueckTimer -= Time.deltaTime;
                if (kurzzurueckTimer < 0)
                {
                    kurzzurueck = false;
                    kurzzurueckTimer = kurzzurueckTimerLimit;
                    stoppen = true;
                }
            }
            */
            if (ansturm)
            {
                firstOne.transform.Translate(unorm * Time.deltaTime * 25, Space.World);
                if (u.magnitude < 0.5f)
                {
                    ansturm = false;
                    //kurzzurueck = true;
                    rueckzug = true;
                    AudioManager.Instance.Play2("explosionKurz");
                }

            }
        }

    }
}
