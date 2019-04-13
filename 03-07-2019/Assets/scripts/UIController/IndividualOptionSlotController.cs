﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndividualOptionSlotController : MonoBehaviour
{
    [SerializeField] private DisplayItemOption displayItem;
    // Start is called before the first frame update
    void Start()
    {
        displayItem.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnOpenSettings()
    {
        displayItem.Open();
        Debug.Log("Open Item Display");
    }
}
