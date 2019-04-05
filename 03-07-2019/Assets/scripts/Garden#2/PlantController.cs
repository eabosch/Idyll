using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public GameObject[] plantVersions;
    int curVersion;

    public string harvestPlant;
    public Sprite seedling;
    public Sprite readyToHarvest;
    public Sprite deadPlant;
    // Start is called before the first frame update

    public int timeLimit1 = 0;
    public int timeLimit2 = 0;
    private int time = 0;

    void Start()
    {
        for(int i = 0; i < plantVersions.Length; i++)
        {
            if(i != curVersion)
            {
                plantVersions[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time == timeLimit1)
        {
            GetComponent<SpriteRenderer>().sprite = seedling;
            plantVersions[curVersion].SetActive(false);
            curVersion = curVersion + 1;
            plantVersions[curVersion].SetActive(true);
        }
        else if (time == timeLimit2)
        {
            GetComponent<SpriteRenderer>().sprite = readyToHarvest;
            plantVersions[curVersion].SetActive(false);
            curVersion++;
            plantVersions[curVersion].SetActive(true);
        } else
        {
            GetComponent<SpriteRenderer>().sprite = deadPlant;
            plantVersions[curVersion].SetActive(false);
            curVersion++;
            plantVersions[curVersion].SetActive(true);
        }

        time++;
    }

}
