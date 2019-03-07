using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    private Seeds selectedSeed;

    public void set_selected(Seeds seed)
    {
        selectedSeed = seed;
    }

    public Seeds get_selected()
    {
        return selectedSeed;
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
