using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WateringReminder : MonoBehaviour
{
    public Plant _plant;
    
    public Text _reminderText;


    private void Update()
    {
        //Determine if we need to turn on watering reminder
        if (_plant.IsThirsty())
        {
            if (_plant.currentGrowthLevel == PlantGrowthLevel.JustPlanted)
            {
                _reminderText.text = "water to start";
            }
            else
            {
                _reminderText.text = "Needs water!";
            }
            
        }
        else
        {
            _reminderText.text = "";
        }

    }
}
