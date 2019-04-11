using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplantedRow : MonoBehaviour
{

    static List<UnplantedRow> _allRows = new List<UnplantedRow>();
    public Plant currentPlant = null;
    //public Equipments currentSeed;
    // Start is called before the first frame update
    void Awake()
    {
        _allRows.Add(this);
    }

    public static UnplantedRow GetClosestRowAtPosition(Vector3 playerPosition)
    {
        UnplantedRow closestRow = null;
        float smallestDistance = float.PositiveInfinity;

        //foreach (UnplantedRow maybeRow in _allRows)
        for (int i = 0; i < _allRows.Count; i++)
        {
            UnplantedRow currentRow = _allRows[i];
            Vector3 difference = currentRow.transform.position - playerPosition;
            difference.z = 0;


            float currentDistance =
                difference.magnitude;
                //Mathf.Abs(_allRows[i].transform.position.y - playerPosition.y) 
                //+ 
                //Mathf.Abs(_allRows[i].transform.position.x - playerPosition.x);


            if (currentRow.currentPlant == null && currentDistance < smallestDistance)
            {
                smallestDistance = currentDistance;
                closestRow = _allRows[i];
            }   
        }
        return closestRow;

    }

    private void OnDestroy()
    {
        _allRows.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // Equipments.instance.get_selected_seed().sew_plant(this.gameObject.transform.position + new Vector3(0,0,.1f));
        currentPlant = Equipments.instance.get_selected_seed().sew_plant(this);
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
