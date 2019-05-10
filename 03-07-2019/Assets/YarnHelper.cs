using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnHelper : MonoBehaviour
{
    [SerializeField]
    DialogueRunner _dialogRunner;
    public DialogueRunner dialogRunner => _dialogRunner;

    public static bool isDialogueRunning => instance.dialogRunner.isDialogueRunning;
    


    public static YarnHelper instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
    }
}

public class EZYarnVariables
{

    //public static string itemForNpcVar() { return "$item_for_npc"; }
    public static string ItemForNpc  //C# properties
    {
        get
        {
            return YarnVariables.instance.GetValue("$item_for_npc").AsString;
        }
        set
        {
            YarnVariables.instance.SetValue("$item_for_npc", value);
        }
    }


    //above is same as writing...
    //public static string itemForNpcVar 
    //{
    //    get
    //    {
    //        return "$item_for_npc";
    //    }
    //}


    static float timeChanged = 0;
    static int _APropertyRealValue = 0; 
    public static int AProperty
    {
        get
        {
            return _APropertyRealValue;
        }

        set
        {
            timeChanged = UnityEngine.Time.time;
            _APropertyRealValue = value;
        }
    }

    void Main()
    {
        EZYarnVariables.AProperty = 0;
        int x = EZYarnVariables.AProperty;
        EZYarnVariables.AProperty++;
    }

}
