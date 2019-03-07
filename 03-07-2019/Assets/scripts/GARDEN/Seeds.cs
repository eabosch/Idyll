using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public Equipments currentSeed;
    public GameObject plant;

    public void sew_plant(Vector3 pos)
    {
        Instantiate(plant, pos, Quaternion.identity);
    }

    void OnMouseDown()
    {
        currentSeed.set_selected(GetComponent<Seeds>());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
