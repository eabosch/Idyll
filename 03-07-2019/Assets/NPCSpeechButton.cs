using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity.Example;

public class NPCSpeechButton : MonoBehaviour
{

    public NPC npc;
    public enum ActionType
    { Talk, Give}

    public ActionType buttonAction = ActionType.Talk;

    private void Awake()
    {
        if (npc == null)
        {
            npc = this.GetComponentInParent<NPC>();
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked " + this.name, this.gameObject);
        if (buttonAction == ActionType.Talk)
        {
            npc.StartConversation();
        }
        else if (buttonAction == ActionType.Give)
        {
            npc.StartGiveInteraction();
        }
    }
}
