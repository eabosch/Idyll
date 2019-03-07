using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tools : MonoBehaviour
{
	// add a cursor to highlight selected tool object 
	public Transform cursorObj;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
	
	void OnMouseDown(){
	
		if(gameObject.name == "shovel"){
			harvestScript.currentTool = "shovel";
		}
		
		if(gameObject.name == "seeds"){
			harvestScript.currentTool = "seeds";
		}
		
		if(gameObject.name == "bucket"){
			harvestScript.currentTool = "bucket";
		}
		
		// transform.position refers to whatever the script is attached to
		cursorObj.transform.position = transform.position;  
		
		// CHANGE THIS, SPECIFY TOOL NAME (matching to the string)
		Debug.Log(harvestScript.currentTool);
		
	}
}
