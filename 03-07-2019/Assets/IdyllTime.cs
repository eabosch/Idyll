using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdyllTime : MonoBehaviour
{
    static float timeInSeconds;
    [SerializeField] float dbgTimeHrs = 0;

    public float timeSpeed = 1;
    const float secondsPerGameHour = 60;

    public static float dayNightBlend = 0; // 0 = day, 1 = night

    public static float sunsetStartTime = 16;
    public static float sunsetEndTime = 20;


    public static float sunriseStartTime = 3;
    public static float sunriseEndTime = 7;

    public static float GetGameTimeHrs()
    {
        return timeInSeconds / secondsPerGameHour;
    }
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeInSeconds += Time.deltaTime * timeSpeed;
        if (GetGameTimeHrs() >= 24)
        {
            timeInSeconds = 0;
        }

        dbgTimeHrs = GetGameTimeHrs();

        float gameTime = GetGameTimeHrs();


        // ----------DAYTIME TO NIGHTIME TRANSITION -------------
        if (gameTime >= sunriseEndTime && gameTime <= 24)
        {
            //ifBranch = "DAY TO NIGHt";
            //Use this value with lerp, to control various parameters
            dayNightBlend = Mathf.InverseLerp(sunsetStartTime, sunsetEndTime, gameTime);


            
        }
        else // ---------- NIGHTIME TO DAYTIME TRANSITION -------------
        {
            //ifBranch = "NIGHt to DAY";
            //3 -> 7
            dayNightBlend = Mathf.InverseLerp(sunriseEndTime, sunriseStartTime, gameTime);


        }
    }
}
