using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{



    public string harvestedPlant;

    [SerializeField] public GameObject Seedling;
    [SerializeField] public GameObject ReadyToHarvest;
    public GameObject seedling;
    public GameObject readyToHarvest;
    public GameObject deadPlant;

    public int timeLimit1 = 0;
    public int timeLimit2 = 0;
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if(time == timeLimit1)
        {
            seedling = Instantiate(Seedling) as GameObject;
            seedling.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+ 1.5f, this.transform.position.z);
            //Instantiate(seedling, this.transform.position, Quaternion.identity);
            Debug.Log("parsnip changed");

        } else if (time == timeLimit2)
        {
            
            readyToHarvest = Instantiate(ReadyToHarvest) as GameObject;
            readyToHarvest.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
            //GetComponent<SpriteRenderer>().sprite = readyToHarvest;
        }

        time++;
    }
}
