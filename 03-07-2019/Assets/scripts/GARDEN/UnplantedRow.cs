using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplantedRow : MonoBehaviour
{
    public Equipments currentSeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        currentSeed.get_selected().sew_plant(this.gameObject.transform.position);
    }
}
