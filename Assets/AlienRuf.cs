using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRuf : MonoBehaviour
{

    bool posEingeholt = false;
    bool sichtbar = false;
    // Start is called before the first frame update
    void Start()
    {

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
                if(gameObject.name == "AlienKlein")
                    AudioManager.Instance.Play2("ohSuessEinKleinesAlien"); 
                if (gameObject.name == "AlienGross")
                    AudioManager.Instance.Play1("hilfeEinRiesigesAlien");
                posEingeholt = true;
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
}
