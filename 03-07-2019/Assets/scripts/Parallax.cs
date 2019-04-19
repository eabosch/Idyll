using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    public float backgroundSize;

    public Transform[] backgroundBlocks;

    [SerializeField]
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;


    Vector3 blockStartPosition;
    float blockWidth;
    public Transform blockCopy;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);

        leftIndex = 0;
        rightIndex = layers.Length-1;

        blockWidth = blockCopy.position.x - this.transform.position.x;//layers[1].position.x - layers[0].position.x;

        blockStartPosition = this.transform.position;
    }

    private void scrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void scrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex < 0)
            leftIndex = 0;
    }

    public float dbgCameraGridPosition = 0;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    scrollLeft();

        //if (Input.GetKeyDown(KeyCode.D))
        //    scrollRight();

        //-------------------------
        //int lastLeft = leftIndex;
        //layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        //rightIndex = leftIndex;
        //leftIndex++;
        //if (leftIndex < 0)
        //    leftIndex = 0;

        //layers[0]

        //float currentXPosition = cameraTransform.transform.position.x - startX;

        float decimalCameraPosition = cameraTransform.position.x - blockStartPosition.x;
        float cameraPositionInUnitsOfBackroundBlocks = Mathf.Round(decimalCameraPosition / blockWidth);

        Vector3 updatedPosition = blockStartPosition;
        updatedPosition.x += cameraPositionInUnitsOfBackroundBlocks * blockWidth;
        this.transform.position = updatedPosition;
        
        

        dbgCameraGridPosition = cameraPositionInUnitsOfBackroundBlocks;
    }



    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawCube(this.transform.position + Vector3.right);
    //}
}
