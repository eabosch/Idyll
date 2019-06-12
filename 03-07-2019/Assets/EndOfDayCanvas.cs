using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayCanvas : MonoBehaviour
{
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    //public GameObject idyllTime;
    public Text displayIdyllDay;
    bool showedOverlayLastFrame = false;
    int stableDaysPassed = -1;
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        float overlayAlpha = Mathf.InverseLerp(.95f, 1f, IdyllTime.dayNightBlend);
        bool showingOverlayThisFrame = overlayAlpha > 0;

        //One time event per day/night cycle
        if (showingOverlayThisFrame && !showedOverlayLastFrame)
        {
            stableDaysPassed = IdyllTime.GetGameDay();
        }
        //map(IdyllTime.dayNightBlend, .95, 1, 0,1);
        canvasGroup.alpha = overlayAlpha;
        displayIdyllDay.text = "End of day " + stableDaysPassed + "\n Tomorrow is day " + (stableDaysPassed+1);
        showedOverlayLastFrame = showingOverlayThisFrame;
    }

    

}
