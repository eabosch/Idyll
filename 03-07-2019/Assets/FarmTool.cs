using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmToolType { Scythe, WateringCan, Shovel }
public class FarmTool : MonoBehaviour
{

    public FarmToolType type;

    public void OnClicked()
    {
        Equipments.instance.set_equipped_tool(this.gameObject);
    }
}
