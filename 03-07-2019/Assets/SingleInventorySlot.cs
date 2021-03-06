﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInventorySlot : MonoBehaviour
{
    private void Awake()
    {
        CurrentItem = this.GetComponentInChildren<GenericInventoryItem>();
    }

    public GenericInventoryItem CurrentItem = null;

    public void SetCurrentItem(GenericInventoryItem item)
    {
        this.CurrentItem = item;
        item.transform.position = this.transform.position;
        item.transform.SetParent(this.transform, true);
        
    }

    public void SetCurrentItem(Plant harvestablePlant)
    {
        Debug.Log(harvestablePlant.name);
    }

    public void ThrowAwayItem()
    {
        if (CurrentItem != null)
        {
            Destroy(CurrentItem);
        }
    }

    public void OnUseButtonClicked()
    {
        if (this.CurrentItem != null)
        {
            Seeds seedsScriptOnObject = CurrentItem.GetComponent<Seeds>();
            if (seedsScriptOnObject != null)
            {
                seedsScriptOnObject.UseItem();
            }

            TradeableItem tradeableItem = CurrentItem.GetComponent<TradeableItem>();
            if (tradeableItem != null)
            {
                tradeableItem.UseItem();
            }
        }
    }
}
