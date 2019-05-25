using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInfo : MonoBehaviour
{
    /*
    public float xInteractionOffset = 0;
    public float interactDistance = 1.5f;
    */

    public GameObject plantInfoText;
    bool isPlantInfoOn;
    void Start()
    {
        HidePlantInfo();

    }

    void Update()
    {
        /*
        bool plantInfoHighlightShouldBeOn = false;
        Vector3 currentPlayerPosition = Yarn.Unity.Example.PlayerCharacter.instance.transform.position;
        float xDistanceToPlayer = Mathf.Abs(currentPlayerPosition.x - (this.transform.position.x + xInteractionOffset));
        plantInfoHighlightShouldBeOn = xDistanceToPlayer < interactDistance;
        */ 
    }

    public void ShowPlantInfo()
    {
        plantInfoText.SetActive(true);
        isPlantInfoOn = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void HidePlantInfo()
    {
        plantInfoText.SetActive(false);
        isPlantInfoOn = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnMouseDown()
    {
        if (isPlantInfoOn)
        {
            HidePlantInfo();
        }
        else
        {
            ShowPlantInfo();
        }
    }
}
