using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipHelper : MonoBehaviour
{
    public GameObject ripInteractionButtons;
    
    [ContextMenu("TurnRipInteractionButtonsOff()")]
    

    public void TurnRipInteractionButtonsOff()
    {
        //ripInteractionButtons = this.gameObject.transform.GetChild(2).gameObject;

        ripInteractionButtons.SetActive(false);
        //this.gameObject.SetActive(false);

    }
    [ContextMenu("TurnRipInteractionButtonsOn()")]
    public void TurnRipInteractionButtonsOn()
    {
        ripInteractionButtons.SetActive(true);
    }
}
