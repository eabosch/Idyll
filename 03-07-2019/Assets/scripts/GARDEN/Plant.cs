using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public PlantGrowthLevel currentGrowthLevel = PlantGrowthLevel.JustPlanted;


    public string harvestedPlant;

    //[SerializeField] public GameObject Seedling;
    //[SerializeField] public GameObject ReadyToHarvest;
    public GameObject justPlanted;
    public GameObject seedling;
    public GameObject readyToHarvest;
    public GameObject deadPlant;

    public float growthCounter = 0;
    public float thirstDeathLevel = -5;

    public bool IsThirsty()
    {
        return wateredAmount < 0;
    }
    public float wateredAmount = 0;

    public float timeToBecomeSeedling = 2;
    public float timeToBecomeReadyToHarvest = 6;


    public UnplantedRow myRow = null;
   

    public int timeLimit1 = 0;
    public int timeLimit2 = 0;
    private int time = 0;

    [Header("--Item Related--")]
    public GameObject collectibleCollider;
    public GameObject inventoryItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SetPlantGrowthLevel(currentGrowthLevel);
    }

    // Update is called once per frame
    void Update()
    {
        float currentGrowthRate = 1;
        bool didntWaterLastCycle = false;
        if (didntWaterLastCycle)
        {
            currentGrowthRate = currentGrowthRate / 2;
        }

        float friendlinessToCharacterScore = 1; //.5f if you were mean, 1.5f if you were nice
        currentGrowthRate += friendlinessToCharacterScore;

        if (IsThirsty())
        {
            currentGrowthRate = 0; //Stop growing, if un-watered
        }

        growthCounter += Time.deltaTime  * currentGrowthRate;
        wateredAmount -= Time.deltaTime;

        if (currentGrowthLevel == PlantGrowthLevel.Seedling && growthCounter >= timeToBecomeReadyToHarvest)
        {
            currentGrowthLevel = PlantGrowthLevel.ReadyToHarvest;
            SetPlantGrowthLevel(currentGrowthLevel);
            if (collectibleCollider != null)
            {
                collectibleCollider.SetActive(true);
            }
        }

        if (currentGrowthLevel == PlantGrowthLevel.JustPlanted && growthCounter >= timeToBecomeSeedling)
        {
            currentGrowthLevel = PlantGrowthLevel.Seedling;
            SetPlantGrowthLevel(currentGrowthLevel);
        }

        bool plantIsAlive = currentGrowthLevel != PlantGrowthLevel.Dead;


        CheckForDeathUpdate();
        
        time++;
    }

    void CheckForDeathUpdate()
    {
        
        if (currentGrowthLevel != PlantGrowthLevel.Dead)
        {
            bool plantShouldDie = currentGrowthLevel != PlantGrowthLevel.JustPlanted && wateredAmount < thirstDeathLevel;//Input.GetKeyDown(KeyCode.D);

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
                this.wateredAmount = 10;//seconds, before becoming thirsty
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
                    slot.SetCurrentItem(harvestInventoryItem);
                    harvestInventoryItem.transform.localScale = Vector3.one; //fix too huge items
                    Destroy(this.gameObject);
                }
            }
        }
        

    }


    private void DeletePlant(int t, GameObject plant)
    {
        if (t == time)
        {
            Destroy(plant);
        }
    }
}

