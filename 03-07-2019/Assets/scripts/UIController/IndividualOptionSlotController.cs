using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualOptionSlotController : MonoBehaviour
{
    bool itemIconOn = false;
    public void Open()
    {
        if (!itemIconOn)
        {
            gameObject.SetActive(true);
            itemIconOn = true;
        }
        else
        {
            gameObject.SetActive(false);
            itemIconOn = false;
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
