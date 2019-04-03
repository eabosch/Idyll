using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public GameObject plantPrefab;
    //public string currentPlant;

    public void sew_plant(Vector3 pos)
    {
        Instantiate(plantPrefab, pos, Quaternion.identity);
    }

    void OnMouseDown()
    {
        Equipments.instance.set_selected_seed(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string get_plant_name()
    {
        return plantPrefab.name;
    }
}
