using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    public static Equipments instance;

    private Seeds selectedSeed;

    public void set_selected_seed(Seeds seed)
    {
        selectedSeed = seed;
    }

    public Seeds get_selected_seed()
    {
        return selectedSeed;
    }

    //Awake gets called first,
    private void Awake()
    {
        instance = this;
    }

   
    //Then start
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
