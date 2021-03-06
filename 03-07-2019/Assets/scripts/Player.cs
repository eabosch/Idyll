using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D myRigidbody;
    private Animator myAnimator;

	public Transform transformLoc;
	public Transform transformLoc2;
	public Transform transformLoc3;

    public GameObject playerCamera;

	[SerializeField]
	private float movementSpeed;

	private bool facingRight;

    private Vector3 offset;

    private float cameraX;
    private float playerX;


	public GameObject player;
	// Use this for initialization
	void Start () 
	{
		facingRight = false;
		myRigidbody = GetComponent<Rigidbody2D> ();
        myAnimator = GetComponent<Animator>();

       offset = playerCamera.transform.position - player.transform.position;

        cameraX = playerCamera.transform.position.x;
        playerX = player.transform.position.x;


    }
	
	// Update is called once per frame
	public IEnumerator nextPlane(Vector3 pos){

		Vector3 newPos = new Vector3 (pos.x, pos.y, player.transform.position.z);
		yield return new WaitForSeconds (0.5f);
		player.transform.position = newPos;

	}

	void Update () {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("quitIt!");
        }

        float horizontal = Input.GetAxis ("Horizontal");
		HandleMovement (horizontal);

		if (Input.GetKeyDown (KeyCode.W))
        {
				Debug.Log ("Going up!");
			    StartCoroutine (nextPlane(transformLoc.transform.position));
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnplantedRow.GetClosestRowAtPosition(this.transform.position);
        }

		Flip (horizontal);

        cameraX = Mathf.Lerp(cameraX, playerX, 5.0f * Time.deltaTime);
	}

    void LateUpdate()
    {
        playerCamera.transform.position = player.transform.position + offset; 
    }



    private void HandleMovement(float horizontal)
	{
		myRigidbody.velocity = new Vector2(horizontal * movementSpeed,myRigidbody.velocity.y); //x = -1, y = 0;

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void Flip(float horizontal)
	{
		if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
			{
			facingRight = !facingRight;

			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
			}
	}
}
