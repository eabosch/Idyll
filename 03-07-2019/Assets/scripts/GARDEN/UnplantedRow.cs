using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplantedRow : MonoBehaviour
{
    //public Equipments currentSeed;
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
        Equipments.instance.get_selected_seed().sew_plant(this.gameObject.transform.position + new Vector3(0,0,.1f));
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
