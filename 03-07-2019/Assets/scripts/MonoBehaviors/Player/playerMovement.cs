using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
	
	
public class playerMovement : MonoBehaviour
{
	public float maxSpeed = 10f;
	bool facingRight = true;
	
	Animator anim;
	
	void Start(){
		anim = GetComponent<Animator>();
		
		
		//destination position
	}
	
	// use Fupdate for physics stuff
	void FixedUpdate(){
		float move = Input.GetAxis("Horizontal");
		
		//** transition from one animation to the next (idling to walking)
		anim.SetFloat("Speed", Mathf.Abs(move));
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(move *maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if(move > 0 && !facingRight){
			Flip();
		} else if (move < 0 && facingRight){
			Flip();
		}
	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
