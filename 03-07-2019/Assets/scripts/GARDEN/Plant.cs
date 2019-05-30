using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity.Example;
/*

To make the plant collectable:
- Add a rigidbody2d, set bodytyper to Kinematic (so it doesn't fall)

- Add a child, named 'collectable collider', with a boxCollider2d, turn child object off

- on the plant script, drag and drop 'collectable collider' on to the 'collectible collider' slot

- Find 'parsnip_item' in the project tab, duplicate it, change it's src image

- on the plant script, drag and drop the new prefab_item onto the 'Inventory Item Prefab' slot

- Apply changes to the plant prefab

*/


public enum PlantGrowthLevel
{
    JustPlanted,
    Seedling,
    ReadyToHarvest,
    Dead
}



public class Plant : MonoBehaviour
{
    // reminder for player to water plants
    public Text reminderText;
    public PlantGrowthLevel currentGrowthLevel = PlantGrowthLevel.JustPlanted;


    public string harvestedPlant;

    public GameObject justPlanted;
    public GameObject seedling;
    public GameObject readyToHarvest;
    public GameObject deadPlant;

    public float growthCounter = 0;
    const float THIRST_DEATH_LEVEL = -2;

    public string guardianNPCName = "";

    public bool IsThirsty()
    {
        return wateredAmount <= 0 && currentGrowthLevel != PlantGrowthLevel.Dead;
    }

    public float getWaterLevel()
    {
        return wateredAmount;
    }

    public float wateredAmount = 0;


    // time to grow to the next phase
    public float daysToBecomeSeedling = 1;
    public float daysToBecomeReadyToHarvest = 2;

    public UnplantedRow myRow = null;


    [Header("--Item Related--")]
    public GameObject collectibleCollider;
    public GameObject inventoryItemPrefab;

    [Header("--DEBUG--")]
    public float currentGrowthRateDBG = 0;
    public float lastSeenSocialHealthDBG = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetPlantGrowthLevel(currentGrowthLevel);
        this.wateredAmount = 0;

        // adds the plant status to OnDayFinish
        IdyllTime.OnDayFinish += ()=> { OnDayFinishPlant(); };
    }

    public int daysAlive = 0;

    // adds the function to inspector menu thingy, can call this function from there
    [ContextMenu("OnDayFinish()")]


    public void OnDayFinishPlant()
    {
        daysAlive++;
        float currentGrowthRate = GetGrowthRate();

        growthCounter += currentGrowthRate;

        // --- Decrease watered amount ----------------
        if (currentGrowthLevel != PlantGrowthLevel.JustPlanted && currentGrowthLevel != PlantGrowthLevel.Dead)
        {
            wateredAmount -= 1;
        }
        

        // Grow from seedling to ready to harvest
        if (currentGrowthLevel == PlantGrowthLevel.Seedling && growthCounter >= daysToBecomeReadyToHarvest)
        {
            currentGrowthLevel = PlantGrowthLevel.ReadyToHarvest;
            SetPlantGrowthLevel(currentGrowthLevel);
            if (collectibleCollider != null)
            {
                collectibleCollider.SetActive(true);
            }
        }

        // Grow from JustPlanted to seedling
        if (currentGrowthLevel == PlantGrowthLevel.JustPlanted && growthCounter >= daysToBecomeSeedling)
        {
            currentGrowthLevel = PlantGrowthLevel.Seedling;
            SetPlantGrowthLevel(currentGrowthLevel);
        }

        CheckForDeathUpdate();

        //SET DEBUG VALUES TO OBSERVE IN INSPECTOR
        currentGrowthRateDBG = currentGrowthRate;

        
    }

    float GetGrowthRate()
    {

        /*
        if (IsThirsty())
        {
            currentGrowthRate = 0; //Stop growing, if un-watered
        }
        */
        //Factors affecting growth rate:
        //WateredAmount, GuardianNpc
        //normal growth rate is 1
        //  2 grows twice as fast
        // .5 half as fast
        //  0 stops growing

        NPC guardianNpc = NPC.GetNPCByName(guardianNPCName);

        bool inGoodWaterState = IsThirsty();
        bool inGoodGuardianNpcState = guardianNpc.PlantSocialHealth() > 0;

        float currentGrowthRate = 1;


        if (inGoodWaterState)
        {
            // if player has visited the plant's guardian NPC
            if (inGoodGuardianNpcState)
            {
                currentGrowthRate = 1f;
            }
            else // player has not visited the plant's guardian NPC
            {
                currentGrowthRate = 1f;
                //currentGrowthRate = 0f;
            }

        } else // if plant has not been watered enough
        {
            // if player has visited the plant's guardian NPC
            if (inGoodGuardianNpcState)
            {
                currentGrowthRate = 1f;
                // currentGrowthRate = 0f;
            }
            else // player has not visited the plant's guardian NPC
            {
                // plant should die instantly
                //currentGrowthRate = 0.5f;
                currentGrowthRate = 1f;
            }
        }
  


        if (guardianNpc != null)
        {
            lastSeenSocialHealthDBG = guardianNpc.PlantSocialHealth();
            if (guardianNpc.PlantSocialHealth() != 0)
            {
                //currentGrowthRate += friendlinessToCharacterScore;
            }
            else
            {
                //currentGrowthRate = 0;
            }
        }

  
        return currentGrowthRate;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void CheckForDeathUpdate()
    {
        /*
        if(currentGrowthLevel!= PlantGrowthLevel.JustPlanted && wateredAmount < -1.0f)
        {
            currentGrowthLevel = PlantGrowthLevel.Dead;

        }
        */


        if (currentGrowthLevel != PlantGrowthLevel.Dead)
        {
            // bool plantShouldDie = (currentGrowthLevel != PlantGrowthLevel.JustPlanted && wateredAmount < thirstDeathLevel);
            
            bool plantShouldDie = (currentGrowthLevel == PlantGrowthLevel.Seedling && wateredAmount < THIRST_DEATH_LEVEL);
            if (plantShouldDie)
            {
                currentGrowthLevel = PlantGrowthLevel.Dead;
                SetPlantGrowthLevel(currentGrowthLevel);
            }

        }
    }


    public void SetPlantGrowthLevel(PlantGrowthLevel desiredGrowthLevel)
    {
        if (justPlanted != null)
        {
            justPlanted.gameObject.SetActive(desiredGrowthLevel == PlantGrowthLevel.JustPlanted);
        }
        seedling.gameObject.SetActive(desiredGrowthLevel == PlantGrowthLevel.Seedling);
        readyToHarvest.gameObject.SetActive(desiredGrowthLevel == PlantGrowthLevel.ReadyToHarvest);
        deadPlant.gameObject.SetActive(desiredGrowthLevel == PlantGrowthLevel.Dead);
    }

    void OnMouseDown()
    {
        // ---- CASE : Plant is Dead ---------------------------
         if (currentGrowthLevel == PlantGrowthLevel.Dead)
        {
            bool okToDelete = Equipments.instance.tool_is_equipped(FarmToolType.Scythe)
                || Equipments.instance.tool_is_equipped(FarmToolType.Shovel);
            if (okToDelete)
            {
                Destroy(this.gameObject);
            }
        }

        // ---- CASE : Plant is not ready to harvest yet -------------------
        else if(currentGrowthLevel != PlantGrowthLevel.ReadyToHarvest)
        {
            if (Equipments.instance.tool_is_equipped(FarmToolType.WateringCan))
            {
                this.wateredAmount = 1; //add 1 water point
            }
        }
        // ---- CASE : Plant is ready to harvest ---------------------------
        else if (currentGrowthLevel == PlantGrowthLevel.ReadyToHarvest)
        {
            bool okToHarvest = Equipments.instance.tool_is_equipped(FarmToolType.Scythe);
            if (okToHarvest)
            {
                SingleInventorySlot slot = Equipments.instance.GetFreeHarvastableSlot();
                if (slot != null)
                {
                    GameObject harvestInventoryItem = GameObject.Instantiate(this.inventoryItemPrefab);
                    slot.SetCurrentItem(harvestInventoryItem.GetComponent<GenericInventoryItem>());
                    harvestInventoryItem.transform.localScale = Vector3.one; //fix too huge items
                    Destroy(this.gameObject);
                }
            }
        }
        

    }

}

