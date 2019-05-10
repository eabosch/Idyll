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
            SetOpen(true);
        }
        else
        {
            SetOpen(false);
        }
    }

    public void SetOpen(bool open)
    {
        gameObject.SetActive(open);
        inventoryMenuOn = open;
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
