using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlantInfoPanel : MonoBehaviour
{
    public GameObject container;
    public Text plantNameText;
    public Text plantDescriptionText;
    public static PlantInfoPanel instance;
    void Awake()
    {
        instance = this;
    }

    public void ShowPlantInfo(PlantInfo info)
    {
        container.SetActive(true);
        this.plantNameText.text = info.plantName;
        this.plantDescriptionText.text = info.plantDescription;
    }

    
}
