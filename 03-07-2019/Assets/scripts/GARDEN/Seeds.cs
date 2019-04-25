using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public GameObject plantPrefab;
    //public string currentPlant;


    [SerializeField]
    SpriteRenderer displayIcon;

    private void Awake()
    {
        if (displayIcon == null)
            displayIcon = this.GetComponent<SpriteRenderer>();
    }

    public Plant sew_plant(UnplantedRow destinationRow)
    {
        Vector3 pos = destinationRow.transform.position;
        GameObject newPlant = Instantiate(plantPrefab, pos, Quaternion.identity);
        Plant plantScript = plantPrefab.GetComponent<Plant>();
        plantScript.myRow = destinationRow;
        Destroy(this.gameObject);//destroy seeds gameobject

        return plantScript;
       //** Equipments.instance.relationshipGlobalVariable.gerardRelationshipQuality = -10;
    }

    void OnMouseDown()
    {
        if (this.enabled)//box is checked in inspector
        {
            Equipments.instance.set_selected_seed(this);
            displayIcon.color = Color.green;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Equipments.instance.get_selected_seed() != this)
        {
            displayIcon.color = Color.white;
        }
    }

    public string get_plant_name()
    {
        return plantPrefab.name;
    }
}
