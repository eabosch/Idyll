using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float timeToBecomeSeedling = 2;
    public float timeToBecomeReadyToHarvest = 6;

    public int timeLimit1 = 0;
    public int timeLimit2 = 0;
    private int time = 0;
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


        growthCounter += Time.deltaTime  * currentGrowthRate;

        if (currentGrowthLevel == PlantGrowthLevel.Seedling && growthCounter >= timeToBecomeReadyToHarvest)
        {
            currentGrowthLevel = PlantGrowthLevel.ReadyToHarvest;
            SetPlantGrowthLevel(currentGrowthLevel);
        }

        if (currentGrowthLevel == PlantGrowthLevel.JustPlanted && growthCounter >= timeToBecomeSeedling)
        {
            currentGrowthLevel = PlantGrowthLevel.Seedling;
            SetPlantGrowthLevel(currentGrowthLevel);
        }

        bool plantIsAlive = currentGrowthLevel != PlantGrowthLevel.Dead;
        bool plantShouldDie = Input.GetKeyDown(KeyCode.D);
        if (plantIsAlive && plantShouldDie)
        {
            currentGrowthLevel = PlantGrowthLevel.Dead;
            SetPlantGrowthLevel(currentGrowthLevel);
        }


        return;
        if (time == timeLimit1)
        {
            
             //seedling = Instantiate(Seedling) as GameObject;
            //Instantiate(seedling, this.transform.position, Quaternion.identity);
            //seedling.SetActive(true);
            seedling.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+ 1.5f, this.transform.position.z);
            Debug.Log("parsnip changed");

        } else if (time == timeLimit2)
        {

           // seedling.GetComponent<Renderer>().enabled = false;

           // seedling.SetActive(false);
            // DeletePlant(timeLimit2, seedling);
            //readyToHarvest = Instantiate(ReadyToHarvest) as GameObject;
            readyToHarvest.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
           // Instantiate(readyToHarvest, this.transform.position, Quaternion.identity);

        }
        
        time++;
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


private void DeletePlant(int t, GameObject plant)
    {
        if (t == time)
        {
            Destroy(plant);
        }
    }
}

