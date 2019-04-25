using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InventoryItemType { Seed, Harvestable, BirdSong };

public class Equipments : MonoBehaviour
{
    public static Equipments instance;

    public Transform _seedSlotsParent;
    public Transform _harvestableListParent;
    public SingleInventorySlot[] _seedInventorySlots;
    public SingleInventorySlot[] _harvestableInventoryList;
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

        _seedInventorySlots = _seedSlotsParent.GetComponentsInChildren<SingleInventorySlot>(true);
        _harvestableInventoryList = _harvestableListParent.GetComponentsInChildren<SingleInventorySlot>(true);

    }

    public SingleInventorySlot GetFreeSeedInventorySlot()
    {
        return GetFreeSlot(_seedInventorySlots);
    }

    public SingleInventorySlot GetFreeHarvastableSlot()
    {
        return GetFreeSlot(_harvestableInventoryList);
    }

    SingleInventorySlot GetFreeSlot(SingleInventorySlot[] slotList)
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            SingleInventorySlot possibleSlot = slotList[i];
            if (possibleSlot.CurrentItem == null)
            {
                return possibleSlot;
            }
        }

        return null;

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

[System.Serializable] //putting this line before a class defintion
//will make it visible in the the inspector
public class NPCRelationshipStatus
{

    public int gerardRelationshipQuality = 0;
    public int nancyRelationshipQuality = 0;
}