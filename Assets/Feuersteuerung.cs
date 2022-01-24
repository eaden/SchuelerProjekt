using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feuersteuerung : MonoBehaviour
{
    List<SpriteRenderer> rendererListe = new List<SpriteRenderer>();
    int rendererCount = 0;
    int currentRenderer = 0;
    int direction = 1;
    float zaehler = 0.05f;
    float zaehlerStartpunkt = 0.05f;
    public bool destroyed = false;
    // Start is called before the first frame update

    public void ShipWasDestroyed()
    {
        for (int i = 0; i < rendererListe.Count; i++)
        {
            rendererListe[i].enabled = false;
        }
        destroyed = true;
    }

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject aktuellesKind = transform.GetChild(i).gameObject;
            if (aktuellesKind.name.Contains("Feuer"))
                rendererListe.Add(aktuellesKind.GetComponent<SpriteRenderer>());
        }
        rendererCount = rendererListe.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (rendererCount > 0 && !destroyed)
        {
            zaehler -= Time.deltaTime;
            if(zaehler < 0)
            {
                zaehler = zaehlerStartpunkt;
                rendererListe[currentRenderer].enabled = false;
                if ((currentRenderer == 0 && direction < 0) || currentRenderer == rendererCount - 1)
                    direction *= -1;
                currentRenderer += direction;
                rendererListe[currentRenderer].enabled = true;

            }
        }
    }
}
