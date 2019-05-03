/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

namespace Yarn.Unity.Example {
    public class PlayerCharacter : MonoBehaviour {

        // STUFF I ADDED
        private Rigidbody2D myRigidbody;
        private Animator myAnimator;
        public GameObject playerCamera;

        [SerializeField]
        private float movementSpeed;

        private bool facingRight;

        private Vector3 offset;

        private float cameraX;
        private float playerX;


  
        //STUFF I ADDED

        public static PlayerCharacter instance;

        public float minPosition = -5.3f;
        public float maxPosition = 5.3f;

        public float moveSpeed = 1.0f;

        public float interactionRadius = 2.0f;

        public float movementFromButtons {get;set;}

        public NPC currentConversationPartner = null;

        /// Draw the range at which we'll start talking to people.
        void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;

            // Flatten the sphere into a disk, which looks nicer in 2D games
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1,1,0));

            // Need to draw at position zero because we set position in the line above
            Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
        }

        private void Awake()
        {
            instance = this;
        }

        //STUFF I ADDED
        void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponentInChildren<Animator>();

            

            facingRight = false;


            offset = playerCamera.transform.position - this.transform.position;

            cameraX = playerCamera.transform.position.x;
            playerX = this.transform.position.x;

            //NPCOptions = GameObject.FindWithTag("NPC").GetComponent<NPC>().NPCOptions;
        }
        //STUFF I ADDED

        public IEnumerator nextPlane(Vector3 pos)
        {

            Vector3 newPos = new Vector3(pos.x, pos.y, this.transform.position.z);
            yield return new WaitForSeconds(0.5f);
            this.transform.position = newPos;

        }


        /// Update is called once per frame
        void Update () {

            

            float cheatSpeedMultiplier = 1;
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                cheatSpeedMultiplier = 4;
            }

            // Remove all player control when we're in dialogue
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
                return;
            }


            // Move the player, clamping them to within the boundaries 
            // of the level.
            var movement = Input.GetAxis("Horizontal");
            movement += movementFromButtons;
            movement *= cheatSpeedMultiplier;
            movement *= (moveSpeed * Time.deltaTime);

            var newPosition = transform.position;
            newPosition.x += movement;
            //newPosition.x = Mathf.Clamp(newPosition.x, minPosition, maxPosition);

            transform.position = newPosition;


            float horizontal = Input.GetAxis("Horizontal");
            HandleMovement(horizontal);

            //// Detect if we want to start a conversation
            //if (Input.GetKeyDown(KeyCode.Space)) {
            //    CheckForNearbyNPC ();
            //}

            Flip(horizontal);
        }

        void LateUpdate()
        {
            playerCamera.transform.position = this.transform.position + offset;
        }

        /// Find all DialogueParticipants
        /** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
        public void CheckForNearbyNPC ()
        {
            List<NPC> allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
            NPC target = allParticipants.Find (delegate (NPC p) {
                return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius;
            });
            if (target != null) {
                // Kick off the dialogue at this node.
                TalkToNPC(target);
            }
        }

        public void OnItemUsed(TradeableItem item)
        {
            if (currentConversationPartner != null 
                &&
                YarnVariables.instance.NpcCanReceive())
                //&& currentConversationPartner.state == NPC.State.ReceivingItem)
            {
                Debug.Log("Gave " + item.name + " to " + currentConversationPartner.name);

                currentConversationPartner.ReceiveItem(item);
                //TalkToNPC(NPC target, string overrideTalkNode = null)
            }
        }


        public void TalkToNPC(NPC target, string overrideTalkNode = null)
        {
            currentConversationPartner = target;
            //NPCOptions.SetActive(false);
            if (overrideTalkNode == null)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
            }
            else
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(overrideTalkNode);
            }
            myAnimator.SetFloat("speed", (0));

        }

        private void HandleMovement(float horizontal)
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); //x = -1, y = 0;

            myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        private void Flip(float horizontal)
        {
            if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
            {
                facingRight = !facingRight;

                Vector3 theScale = transform.localScale;

                theScale.x *= -1;

                transform.localScale = theScale;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("NPC"))
            { 
               // NPCOptions.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
               // NPCOptions.SetActive(false);
            }
        }

        //public SingleInventorySlot receivedItem;

        //[YarnCommand("receiveItem")]
        public void ReceiveItem(string itemName)
        {
            Debug.Log("You've received " + itemName + "from " + currentConversationPartner.name);
            Equipments.instance.AddItemToPlayerInventory(itemName);
        }
    }
}
