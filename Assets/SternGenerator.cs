using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SternGenerator : MonoBehaviour
{
    public GameObject[] sternSammlung = new GameObject[15];
    //List<GameObject> sterne = new List<GameObject>();
    GameObject aktuellerStern = null;
    float aktuelleGeschwindigkeit;
    float startGeschwindigkeit = 4;
    // Start is called before the first frame update
    void Start()
    {
        NeuerStern();
    }

    void NeuerStern()
    {
        aktuellerStern = (Instantiate(sternSammlung[Random.Range(0, sternSammlung.Length)], transform.position + new Vector3(Random.Range(0f, 5f) / 10f - 0.25f, Random.Range(0f, 5f) / 10f - 0.25f), transform.rotation));
        aktuelleGeschwindigkeit = startGeschwindigkeit + Random.Range(0, 4) - 3;
        if(transform.parent.gameObject.name == "SterngeneratorFlugSzene")
        {
            aktuelleGeschwindigkeit = 0f;//(startGeschwindigkeit/4 + Random.Range(0, 4)/4 - 3/4)*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(aktuellerStern.transform.position);
        if (pos.x > 1f || pos.y < 0)
        {
            Destroy(aktuellerStern);
            NeuerStern();
        }
        if (aktuellerStern != null)
        {
            aktuellerStern.transform.Translate(Vector3.right * Time.deltaTime * aktuelleGeschwindigkeit);
        }
    }
}
