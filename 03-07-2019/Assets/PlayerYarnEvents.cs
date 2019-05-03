using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class PlayerYarnEvents : MonoBehaviour
{
    static System.Action<string, string> OnYarnEvent = (target, argument) => { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [YarnCommand("receiveItem")]
    public void ReceiveItem(string itemName)
    {
        Debug.Log("You've received " + itemName + "from " + PlayerCharacter.instance.name);
        PlayerCharacter.instance.ReceiveItem(itemName);
    }
}
