﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeableItem : MonoBehaviour
{
    public void UseItem()
    {
        Debug.Log("The player tried to use me, my name is " + this.name, this.gameObject);
    }
}
