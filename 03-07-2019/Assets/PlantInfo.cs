using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInfo : MonoBehaviour
{
    /*
    public float xInteractionOffset = 0;
    public float interactDistance = 1.5f;
    */

    public bool mouseOver = false;

    public GameObject plantInfoText;
    bool isPlantInfoOn;

    [SerializeField]
     GameObject plantNameText;
    [SerializeField]
    GameObject plantDescriptionText;

    public string plantName;
    [TextArea]
    public string plantDescription;

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

        if (plantNameText != null)
        {
            plantNameText.SetActive(isPlantInfoOn || mouseOver);
        }

        // <<<< Get rid of this part??? <<<<<
        if (plantDescriptionText != null)
        {
            plantDescriptionText.SetActive(isPlantInfoOn);
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    }

    public void ShowPlantInfo()
    {
        //plantInfoText.SetActive(true);
        isPlantInfoOn = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void HidePlantInfo()
    {
        //plantInfoText.SetActive(false);
        isPlantInfoOn = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
    void OnMouseDown()
    {
        PlantInfoPanel.instance.ShowPlantInfo(this);
        // <<<< Get rid of this part??? <<<<<
        if (isPlantInfoOn)
        {
            HidePlantInfo();
        }
        else
        {
            ShowPlantInfo();
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    }
}
