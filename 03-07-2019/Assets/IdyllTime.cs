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

    //System.Action a function with no arguments, and no return value
    // void AFunction()
    //{
    //  print("this one would be ok");
    //}

        //a function you can change
    public static System.Action OnDayFinish = () => { }; //equivalent to js function(){ }
    //Annie's NPC script's 


    public static float GetTotalGameHoursPassed()
    {
        return timeInSeconds / secondsPerGameHour;
    }
    public static float GetGameClockTimeHrs()
    {
       
        return GetTotalGameHoursPassed() % 24;
    }

    public static int GetGameDay()
    {
        return (int)(GetTotalGameHoursPassed() / 24);
    }
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int dayBeforeIncrement = GetGameDay();
        timeInSeconds += Time.deltaTime * timeSpeed;
        if (GetGameClockTimeHrs() >= 24)
        {
            //timeInSeconds = 0;
        }

        int dayAfterIncrement = GetGameDay();

        if (dayAfterIncrement != dayBeforeIncrement)
        {
            OnDayFinish(); //notify other scripts who have +=subsribed to this function
        }

        dbgTimeHrs = GetGameClockTimeHrs();

        float gameTime = GetGameClockTimeHrs();


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
