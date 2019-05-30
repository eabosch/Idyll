using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
    CHANGES MATERIAL/COLOR FOR ALL OBJECTS WITH THIS MATERIAL

     */

public class DayNightColorer : MonoBehaviour
{
    [SerializeField]
    List<DayNightMaterial> _dayNightMaterials;

    [System.Serializable]
    public class DayNightMaterial
    {
        public Material material;
        public Color dayColor = Color.white;
        public Color nightColor = Color.white;
    }

    void Update()
    {
        foreach (DayNightMaterial m in _dayNightMaterials)
        {
            m.material.color = 
                Color.Lerp(m.dayColor, m.nightColor, IdyllTime.dayNightBlend);
            
           
            //3 100
        }
    }

    private void OnApplicationQuit()
    {
        foreach (DayNightMaterial m in _dayNightMaterials)
        {
            m.material.color = m.dayColor;
        }
    }
}
