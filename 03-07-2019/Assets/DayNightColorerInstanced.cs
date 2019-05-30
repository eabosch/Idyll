using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
    CHANGES MATERIAL/COLOR ON JUST ONE OBJECT

     */
public class DayNightColorerInstanced : MonoBehaviour
{

    [SerializeField]
    Renderer[] _renderers;

    public Color dayColor = Color.white;
    public Color nightColor = Color.white;


    private void Awake()
    {
        _renderers = this.GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        foreach (Renderer r in _renderers)
        {
            foreach (Material m in r.materials)
            {
                m.color = Color.Lerp(dayColor, nightColor, IdyllTime.dayNightBlend);
            }
        }
    }
}
