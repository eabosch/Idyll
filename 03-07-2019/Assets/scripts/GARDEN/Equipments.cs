using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InventoryItemType { Seed, Harvestable, BirdSong };

public class Equipments : MonoBehaviour
{
    public static Equipments instance;

    public Transform _seedSlotsParent;
    public Transform _harvestableListParent;
    public Transform _birdsongListParent;
    public SingleInventorySlot[] _seedInventorySlots;
    public SingleInventorySlot[] _harvestableInventoryList;
    public SingleInventorySlot[] _birdsongInventoryList;
    List<SingleInventorySlot> _allInventorySlotsAllTypes = new List<SingleInventorySlot>();

    //**public NPCRelationshipStatus relationshipGlobalVariable = new NPCRelationshipStatus();

    [SerializeField]
    GameObject[] _seedPrefabs;
    List<GameObject> _allInvetoryItemPrefabs = new List<GameObject>();

    private Seeds selectedSeed;
    public GameObject currentEquippedTool = null;

    //public void set_selected_seed(Seeds seed)
    //{
    //    selectedSeed = seed;
    //}

    //Awake gets called first,
    private void Awake()
    {
        instance = this;

        _seedInventorySlots = _seedSlotsParent.GetComponentsInChildren<SingleInventorySlot>(true);
        _harvestableInventoryList = _harvestableListParent.GetComponentsInChildren<SingleInventorySlot>(true);
        _birdsongInventoryList = _birdsongListParent.GetComponentsInChildren<SingleInventorySlot>(true);

        _allInventorySlotsAllTypes.AddRange(_seedInventorySlots);
        _allInventorySlotsAllTypes.AddRange(_harvestableInventoryList);
        _allInventorySlotsAllTypes.AddRange(_birdsongInventoryList);

        _seedPrefabs = Resources.LoadAll<GameObject>("Seeds");
      
        _allInvetoryItemPrefabs.AddRange(_seedPrefabs);

        //_birdSongs = Resources.LoadAll<GameObject>("BirdSongs");
        //_allInvetoryItemPrefabs.AddRange(_birdSongs);
    }

    GameObject getItemPrefabByName(string prefabName)
    {
        foreach (GameObject itemPrefab in _allInvetoryItemPrefabs)
        {
            if (itemPrefab.name == prefabName)
            {
                return itemPrefab;
            }
        }

        return null;
    }

    public void AddItemToPlayerInventory(string itemName)
    {
        GameObject itemPrefab = getItemPrefabByName(itemName);
        GameObject itemCopy = GameObject.Instantiate(itemPrefab);
        itemCopy.name = itemName;
        itemCopy.GetComponent<CollectableInventoryItem>().CollectItemIfPossible();
    }

    public void RemoveItemFromPlayerInventory(string itemName)
    {
        foreach (SingleInventorySlot slot in _allInventorySlotsAllTypes)
        {
            if (slot.CurrentItem != null && slot.CurrentItem.itemName == itemName)
            {
                //Destroy(slot.CurrentItem);// <=== this line would only destroy the script, not the whole game object!
                Destroy(slot.CurrentItem.gameObject);
                break;//stop going through the list, in case we have more than 1 item with that name
            }
        }
    }

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
            return currentEquippedTool.GetComponent<FarmTool>();//selectedFarmTool;
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



    public SingleInventorySlot GetFreeSeedInventorySlot()
    {
        return GetFreeSlot(_seedInventorySlots);
    }

    public SingleInventorySlot GetFreeHarvastableSlot()
    {
        return GetFreeSlot(_harvestableInventoryList);
    }

    public SingleInventorySlot GetFreeBirdsongSlot()
    {
        return GetFreeSlot(_birdsongInventoryList);
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