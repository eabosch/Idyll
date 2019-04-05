using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //putting this line before a class defintion
//will make it visible in the the inspector
public class NPCRelationshipStatus
{

    public int gerardRelationshipQuality = 0;
    public int nancyRelationshipQuality = 0;
}


public class Equipments : MonoBehaviour
{
    public static Equipments instance;

    //**public NPCRelationshipStatus relationshipGlobalVariable = new NPCRelationshipStatus();


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
