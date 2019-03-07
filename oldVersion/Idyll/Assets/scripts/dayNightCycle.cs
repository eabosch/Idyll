using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
	public float total_time;
	public float RG;

    // Start is called before the first frame update
    void Start(){
	
        total_time = 0;
		RG = 1;
    }

    // start with day->night (8 sec cycle now)
    void Update(){
		if (total_time > 1440){ //total_time > 2
	 		total_time = 0;
	 	}
	
		total_time += Time.deltaTime;	
		
		if (total_time >=0 && total_time <= 720 ){ //total_time >=0 && total_time <= 1
			RG -= 0.00002778f; // 0.02f
            
		} 
		
		if (total_time > 720 && total_time <= 1440){ //total_time > 1 && total_time <= 2
			RG += 0.00002778f; //0.02f
		}
		 
		
		GetComponent<SpriteRenderer>().color = new Color(RG, RG, 1); 
    }
}
