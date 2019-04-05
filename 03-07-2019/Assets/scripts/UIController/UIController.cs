using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private DisplaySeedOptionsMenu displaySeedMenu;
    // Start is called before the first frame update
    void Start()
    {
        displaySeedMenu.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpenSettings()
    {
        displaySeedMenu.Open();
        Debug.Log("Open Seed Options");
    }
}
