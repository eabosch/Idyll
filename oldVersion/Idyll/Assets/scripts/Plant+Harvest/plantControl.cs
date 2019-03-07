using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantControl : MonoBehaviour
{
	public Sprite noPlantObj;
	public Sprite sprout;

	// apple
	public Sprite apple_1;
	public Sprite apple_2;
	
	// cranberry 
	public Sprite cranberry_1;
	public Sprite cranberry_2;
	
	//grape
	public Sprite grape_1;
	public Sprite grape_2;
	
	// parsnip
	public Sprite parsnip_1;
	public Sprite parsnip_2;
	
	//artichoke
	public Sprite artichoke_1;
	public Sprite artichoke_2;
	
	
	public float growTime = 0;
	
	//
	public Transform plotObj;
	public string watered = "n";
	
	
	public string currentSeed;

    void Start()
    {
        
    }

    void Update()
    {
		
	
	
	
	
		///////////////////////////////////////
		// if something has been planted, record time
		if (GetComponent<SpriteRenderer>().sprite == sprout){
			growTime += Time.deltaTime;
		}
        
		
		// change grow time (5 sec) placeholder
		if (growTime > 5) {
			// if watered the plant within 5 sec
			if (watered == "y"){
				//apple growth phase 1
				GetComponent<SpriteRenderer>().sprite = apple_1;
			} 
			
//			else if (){
//				//apple growth phase 2
//			}
			else {
			
				// if not watered within time frame, plant dies
				growTime = 0;
				GetComponent<SpriteRenderer>().sprite = noPlantObj;
				
			}
			
		}
		
    }
	
	void OnMouseDown(){
		Debug.Log("clicked on weed");
		
		//REPLACE TOOL NAME
		if(harvestScript.currentTool == "reap"){
			//Destroy (gameObject);
			GetComponent<SpriteRenderer>().sprite = noPlantObj;
		}
		
		if((harvestScript.currentTool == "seeds") && (GetComponent<SpriteRenderer>().sprite == noPlantObj))
		{
			GetComponent<SpriteRenderer>().sprite = apple_1;
		
		}
		
		if(harvestScript.currentTool == "bucket"){
			//Destroy (gameObject);
			//when water the plants, the plot color changes
			plotObj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
			watered = "y";
		}
	}
}
