﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInventoryItem : MonoBehaviour
{
    public InventoryItemType itemType = InventoryItemType.Seed;
    public MonoBehaviour usableItem;

    private void Start()
    {
        
    }

    void OnMouseDown()
    {
        //Equipments.instance.set_selected_seed(this);
        //displayIcon.color = Color.green;
        Debug.Log("Hello");

        SingleInventorySlot slot = null;
        if (itemType == InventoryItemType.Seed)
        {
            slot = Equipments.instance.GetFreeSeedInventorySlot();
            if (slot != null) //!!!!!!! seed objects need to be downscaled, for now !!!!!!!!
            {
                this.transform.localScale *= .4f;
            } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
        else if (itemType == InventoryItemType.Harvestable)
        {
            slot = Equipments.instance.GetFreeHarvastableSlot();
        }
       
        if (slot != null)
        {
            slot.SetCurrentItem(this.gameObject);
            //---------------
            usableItem.enabled = true;
            
            Destroy(this);
        }
        else
        {
            Debug.LogError("No slots available!");
        }
    }
}