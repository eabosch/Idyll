using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    private Renderer myRenderer;

    //public float speed = 0.5f;

	public float speed;
	Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        //myRenderer = GetComponent<Renderer>();
		startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate ((new Vector3 (-1, 0, 0)) * speed * Time.deltaTime);
		
       // Vector2 offset = new Vector2(Time.time * speed, 0);

       // myRenderer.material.mainTextureOffset = offset;
    }


}
