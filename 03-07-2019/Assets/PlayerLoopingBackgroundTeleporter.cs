using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity.Example;
public class PlayerLoopingBackgroundTeleporter : MonoBehaviour
{
    public Transform leftTile;
    public Transform rightile;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCharacter playerCharacter = PlayerCharacter.instance;
        float worldCenterX =
        //    this.transform.position.x; //this one, a little safer if your pivot is roughly at the center of the background
        (rightile.transform.position.x + leftTile.transform.position.x) / 2;


        //float trueWorldCenterX = .5f * rightile.transform.position.x - 
        float worldWidth = Mathf.Abs(this.transform.position.x - leftTile.transform.position.x);
        //check if they've walked off the left side
        bool playerHasWalkedOffLeftSideOfWorld = playerCharacter.transform.position.x < worldCenterX - worldWidth / 2;

        bool playerHasWalkedOffRightSideOfWorld = playerCharacter.transform.position.x > worldCenterX + worldWidth / 2;

        if (playerHasWalkedOffLeftSideOfWorld)
        {
            Vector3 updatedPlayerPosition = playerCharacter.transform.position;

            updatedPlayerPosition.x += worldWidth;
            playerCharacter.transform.position = updatedPlayerPosition;
        }

        if (playerHasWalkedOffRightSideOfWorld)
        {
            Vector3 updatedPlayerPosition = playerCharacter.transform.position;

            updatedPlayerPosition.x -= worldWidth;
            playerCharacter.transform.position = updatedPlayerPosition;
        }
    }

    private void OnDrawGizmos()
    {
        float worldCenterX =
//    this.transform.position.x; //this one, a little safer if your pivot is roughly at the center of the background
(rightile.transform.position.x + leftTile.transform.position.x) / 2;


        //float trueWorldCenterX = .5f * rightile.transform.position.x - 
        float worldWidth = Mathf.Abs(this.transform.position.x - leftTile.transform.position.x);

        Gizmos.color = Color.magenta;
        Vector3 drawPos = this.transform.position;
        Gizmos.DrawCube(drawPos - Vector3.forward + Vector3.right * worldWidth / 2, new Vector3(1f, 100f, 1f));

        Gizmos.DrawCube(drawPos - Vector3.forward - Vector3.right * worldWidth / 2, new Vector3(1f, 100f, 1f));
    }
}
