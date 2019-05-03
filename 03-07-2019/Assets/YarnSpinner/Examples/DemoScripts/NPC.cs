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
using UnityEngine.Serialization;
using System;
/// attached to the non-player characters, and stores the name of the
/// Yarn node that should be run when you talk to them.
namespace Yarn.Unity.Example {
    public class NPC : MonoBehaviour
    {
        public enum State
        { Idle, TalkingToPlayer, ReceivingItem, GivingItem}

        public State state = State.Idle;

        public float xInteractionOffset = 0;
        public float interactDistance = 1.5f;
        public string characterName = "";

        public GameObject NPCOptions;

        [FormerlySerializedAs("startNode")]
        public string talkToNode = "";

        [Header("Optional")]
        public TextAsset scriptToLoad;

        // Use this for initialization
        void Start ()
        {
            NPCOptions.SetActive(true);

            if (scriptToLoad != null) {
                FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
            }


        }

        // Update is called once per frame
        void Update ()
        {
            bool interactButtonsShouldBeOn = false;

            if (state == State.Idle)
            {
                Vector3 currentPlayerPosition = PlayerCharacter.instance.transform.position;
                float xDistanceToPlayer = Mathf.Abs(currentPlayerPosition.x - (this.transform.position.x + xInteractionOffset));
                interactButtonsShouldBeOn = xDistanceToPlayer < interactDistance;

            }
            else if (state == State.TalkingToPlayer)
            {

            }

            if (NPCOptions.activeSelf != interactButtonsShouldBeOn)
            {
                NPCOptions.SetActive(interactButtonsShouldBeOn);
            }
        }
        public void StartConversation()
        {
            this.state = State.TalkingToPlayer;
            PlayerCharacter.instance.TalkToNPC(this);
        }

        public void StartGiveInteraction()
        {
            this.state = State.TalkingToPlayer;
            PlayerCharacter.instance.TalkToNPC(this, this.characterName + ".Give");
        }

        public void StartReceiveInteraction()
        {
            this.state = State.ReceivingItem;
            PlayerCharacter.instance.TalkToNPC(this, this.characterName + ".Parsnip");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(this.transform.position + Vector3.right * xInteractionOffset, interactDistance);
        }

        internal void ReceiveItem(TradeableItem item)
        {
            PlayerCharacter.instance.TalkToNPC(this, this.characterName + ".FirstGift");
        }
    }



}
