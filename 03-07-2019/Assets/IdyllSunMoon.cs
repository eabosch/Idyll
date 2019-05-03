using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdyllSunMoon : MonoBehaviour
{
    public AnimationCurve rotationCurve;
    public float zeroToOneDayNightNumber;
    public float correctionOffset;
    //public float sunRotationOffset = 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //0 degrees = beginning of sunrise
        //90        = sun at highest point in sky (noon)
        //180       = sun (sunset)
        //270       = moon has risen
        if (IdyllTime.GetGameTimeHrs() >= 0 && IdyllTime.GetGameTimeHrs() < IdyllTime.sunsetStartTime)
        {
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(90, 270, IdyllTime.dayNightBlend));
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(90, 270, IdyllTime.dayNightBlend));
        }
        
    }
}
