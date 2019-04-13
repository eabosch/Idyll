using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventoryOptionsMenu : MonoBehaviour
{
    bool inventoryMenuOn = false;
    public void Open()
    {
        if (!inventoryMenuOn)
        {
            gameObject.SetActive(true);
            inventoryMenuOn = true;
        }
        else
        {
            gameObject.SetActive(false);
            inventoryMenuOn = false;
        }
    }

    public void Close()
    {
        //gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
