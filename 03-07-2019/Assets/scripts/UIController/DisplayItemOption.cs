using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemOption : MonoBehaviour
{
    bool itemIconOn = true;
    public void Open()
    {
        Debug.Log("Open()");
        if (itemIconOn)
        {
            gameObject.SetActive(false);
            itemIconOn = false;
        }
        else
        {
            gameObject.SetActive(true);
            itemIconOn = true;
        }
    }

    public void Close()
    {
        //gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
