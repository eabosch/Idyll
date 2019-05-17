using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdyllUIClock : MonoBehaviour
{
    public Text IdyllClockText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float raw24HourTime = IdyllTime.GetGameClockTimeHrs(); //0 to 24
                                                          //want something like 1:15 AM, 3:30 PM, 11:59 AM
         //(raw24HourTime) -> (desired display)
        //13.5 -> 1:30 PM
        //1.75 -> 1:45 AM
        //0.25 -> 12:45 AM
        //12.33333 -> 12:20 PM

        float hourFraction = raw24HourTime % 1; //give 13.5 -> 0.5 (part of the number after decimal point)
        // (hourDigits) : (minuteDigits)
        float hourDigits = Mathf.Floor(raw24HourTime) % 12;
        if (hourDigits == 0)
        {
            hourDigits = 12;
        }


        float minutes = Mathf.Floor(hourFraction * 60);
        IdyllClockText.text = hourDigits + " : " + minutes + "\nDay: " + IdyllTime.GetGameDay();
        //Debug.Log(IdyllTime.GetGameTimeHrs();
    }
}
