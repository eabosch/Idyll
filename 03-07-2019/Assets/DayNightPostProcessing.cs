using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayNightPostProcessing : MonoBehaviour
{

    [Range(0, 24)]
    public float dayNightBlend = 0;

   
    //[SerializeField]
    //private float aPrivateFloat = 444444;
    public PostProcessVolume processingVol;
    ColorGrading colorGradingLayer = null;


    void Start()
    {
        processingVol = this.GetComponent<PostProcessVolume>();
        processingVol.profile.TryGetSettings(out colorGradingLayer);
    }
    public string ifBranch = "";
    // Update is called once per frame
    void Update()
    {
        SetNightAmount(IdyllTime.dayNightBlend);
        //float transitionToNightStartTime = 16;
        //float transitionToNightEndTime = 20;


        //float transitionToDayStartTime = 3;
        //float transitionToDayEndTime = 7;

        //float gameTime = IdyllTime.GetGameTimeHrs();

        //// ----------DAYTIME TO NIGHTIME TRANSITION -------------
        //if (gameTime >= transitionToDayEndTime && gameTime <= 24)
        //{
        //    ifBranch = "DAY TO NIGHt";
        //    //Use this value with lerp, to control various parameters
        //    float hoursConvertedToZero1 = Mathf.InverseLerp(transitionToNightStartTime, transitionToNightEndTime, gameTime);

        //    //y = Mathf.InverseLerp(a,b, x);
        //    float hoursConvertedToZeroNeg50 = Mathf.Lerp(0, -45, hoursConvertedToZero1);

        //    SetNightAmount(hoursConvertedToZero1);
        //}
        //else // ---------- NIGHTIME TO DAYTIME TRANSITION -------------
        //{
        //    ifBranch = "NIGHt to DAY";
        //    //3 -> 7
        //    float nightPercentage = Mathf.InverseLerp(transitionToDayEndTime, transitionToDayStartTime, gameTime);

        //    SetNightAmount(nightPercentage);
        //}

    }

    public void SetNightAmount (float nightPercentage) //0 == daytime, 1 == nightime
    {
        float hoursConvertedToZeroNeg50 = Mathf.Lerp(0, -45, nightPercentage);
        float brightness = hoursConvertedToZeroNeg50;

        //float blackoutStart = .90f;
        //if (nightPercentage > blackoutStart) 
        //{
        //    float blackOutAmount = Mathf.InverseLerp(blackoutStart, 1, nightPercentage);

        //    brightness = Mathf.Lerp(brightness, -100, blackOutAmount);
        //}
        colorGradingLayer.brightness.value = brightness;
        colorGradingLayer.mixerBlueOutBlueIn.value = Mathf.Lerp(100, 150, nightPercentage);
    }


}
