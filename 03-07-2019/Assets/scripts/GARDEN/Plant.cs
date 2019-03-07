using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    Animator anim;

    public string harvestedPlant;
    public Sprite seedling;
    public AnimationClip readyToHarvest;
    public Sprite deadPlant;

    public int timeLimit1 = 0;
    public int timeLimit2 = 0;
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
   }

    // Update is called once per frame
    void Update()
    {

        if(time == timeLimit1)
        {
            GetComponent<SpriteRenderer>().sprite = seedling;
        } else if (time == timeLimit2)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo);
            // readyToHarvest=GetComponent<GameObject>();
            //readyToHarvest = GetComponent<GameObject>.Play("parsnip_grown");
        }

        time++;
    }
}
