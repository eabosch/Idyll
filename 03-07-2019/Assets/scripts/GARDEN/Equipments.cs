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
    public GameObject currentEquippedTool = null;

    //public void set_selected_seed(Seeds seed)
    //{
    //    selectedSeed = seed;
    //}

    public void set_equipped_tool(GameObject currentTool)
    {
        currentEquippedTool = currentTool;
    }

    public void unequip_tool()
    {
        currentEquippedTool = null;
    }
    

    public Seeds get_equipped_seed()
    {
        if (currentEquippedTool != null)
        {
            return currentEquippedTool.GetComponent<Seeds>();//selectedSeed;
        }
        return null;
    }

    public FarmTool get_equipped_farm_tool()
    {
        if (currentEquippedTool != null)
        {
            return currentEquippedTool.GetComponent<FarmTool>();//selectedSeed;
        }
        return null;
    }

    public bool tool_is_equipped(FarmToolType farmToolType)
    {
        return 
            get_equipped_farm_tool() != null 
            && 
            get_equipped_farm_tool().type == farmToolType;
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