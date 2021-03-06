﻿/*

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
using System.Collections.Generic;
/// attached to the non-player characters, and stores the name of the
/// Yarn node that should be run when you talk to them.
namespace Yarn.Unity.Example
{
    public class NPC : MonoBehaviour
    {
        public enum State
        { Idle, TalkingToPlayer, ReceivingItem, Dead }

        public State _state = State.Idle;

        public State state
        {
            get
            {
                return _state;
            }

            set
            {
                Debug.Log("state changed from " + _state + " to " + value);
                _state = value;
            }
        }

        public float xInteractionOffset = 0;
        public float interactDistance = 1.5f;
        public string characterName = "";
        //public bool isRipInteractible;
        //public bool isAnnieInteractable;

        public bool isNPCInteractable;
        public GameObject NPCOptions;

        const float NPC_DEATH_TIMELIMIT = 72; //3days*24hrs
        const float PLAYER_PLANT_STOP_GROWING_TIME = 24;

        public float timeOfLastInteractionWithPlayer = 0;

        [FormerlySerializedAs("startNode")]
        public string talkToNode = "";

        [Header("Optional")]
        public GameObject humanVisuals;
        public GameObject plantVisuals;
        //public GameObject wildPlant;

        [Header("Neglect Display")]
        public GameObject noInteractionDay1;
        public GameObject noInteractionDay2;
        public Animator wildPlantsAnimator;


        [Header("Optional")]
        public TextAsset scriptToLoad;


        public static Dictionary<string, NPC> _allNpcs = new Dictionary<string, NPC>();

        public float timeSinceLastInteraction => IdyllTime.GetTotalGameHoursPassed() - timeOfLastInteractionWithPlayer;

        public int daysPassed => IdyllTime.GetGameDay();

        private void Awake()
        {
            _allNpcs[this.name] = this;
            IdyllTime.OnDayFinish += () => { KillNPCIfNotEnoughtSocialInteraction();
                GrowWildPlantIfNoInteractionOnThatDay();
            };
        }

        void KillNPCIfNotEnoughtSocialInteraction()
        {
            if (timeSinceLastInteraction > NPC_DEATH_TIMELIMIT)
            {
                state = State.Dead;
                humanVisuals.SetActive(false);
                ClearWildPlants();

                this.delayedFunction( () => plantVisuals.SetActive(true), 2f);
                //this.delayedFunction(turnOnPlantVisuals, 2f);    
            //this.xuTween((float t) => 
                //{
                //    plantVisuals.transform.localScale = Vector3.one * t;
                //}, 2);
                //
                //foreach (SpriteRenderer r in this.GetComponentsInChildren<SpriteRenderer>())
                //{
                //    r.color = Color.black;
                //}
            }

        }

        void ClearWildPlants()
        {
            //noInteractionDay1.SetActive(false);
            //noInteractionDay2.SetActive(false);
            if (wildPlantsAnimator != null)
            {
                wildPlantsAnimator.SetInteger("growth_level", 0);
            }
            //wildPlantsAnimator?.SetInteger("growth_level", 0);
            //if (wildPlantsAnimator != null && wildPlantsAnimator.avatar != null)
            //{ }
            //if (wildPlantsAnimator?.avatar != null)
            //{ }

        }

        void GrowWildPlantIfNoInteractionOnThatDay()
        {
            bool wildPlantsCouldntGrow = state == State.TalkingToPlayer || state == State.ReceivingItem;
            if (wildPlantsCouldntGrow || wildPlantsAnimator == null)
            {
                return;
            }

            if (timeSinceLastInteraction >= 72)
            {
                noInteractionDay2.SetActive(false);
            }
            else if(timeSinceLastInteraction>= 48)
            {
                //noInteractionDay1.SetActive(false);
                //noInteractionDay2.SetActive(true);
                wildPlantsAnimator.SetInteger("growth_level", 2);
            } else if (timeSinceLastInteraction >= 24)
            {
                //noInteractionDay1.SetActive(true);
                //noInteractionDay2.SetActive(false);
                wildPlantsAnimator.SetInteger("growth_level", 1);
            }
        }

        public static NPC GetNPCByName(string npcName)
        {
            if (_allNpcs.ContainsKey(npcName))
            {
                return _allNpcs[npcName];
            }
            return null;
        }

        void Start()
        {
            //NPCOptions.SetActive(true);

            if (scriptToLoad != null) {
                FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
            }
        }


        public float PlantSocialHealth()
        {
            if (timeSinceLastInteraction > PLAYER_PLANT_STOP_GROWING_TIME)
            {
                // return 0 if have not interacted with NPC within time limit, 1 if interacted 

                Debug.Log("Player has not interacted with " + this.characterName + " for a day");
                return 0;
            }
            else
            {
                return 1;
            }
        }



        // Update is called once per frame
        void Update()
        {
            Debug.Log(daysPassed + " days have passed");

           bool interactButtonsShouldBeOn = false;

            if (this.characterName == "Rip")
            {
                if (daysPassed >= 2)
                {
                    NPCOptions.SetActive(true);
                    isNPCInteractable = true;
                } 
            } else
           {
               isNPCInteractable = true;
            }

            if (isNPCInteractable)
           {
                if (state == State.Idle)
                {
                    Vector3 currentPlayerPosition = PlayerCharacter.instance.transform.position;
                    float xDistanceToPlayer = Mathf.Abs(currentPlayerPosition.x - (this.transform.position.x + xInteractionOffset));
                    interactButtonsShouldBeOn = xDistanceToPlayer < interactDistance;



                }
                else if (state == State.TalkingToPlayer)
                {
                    if (!YarnHelper.isDialogueRunning)
                    {
                        state = State.Idle;
                    }
                }

                if (NPCOptions.activeSelf != interactButtonsShouldBeOn)
                {
                    NPCOptions.SetActive(interactButtonsShouldBeOn);
                }

                if (state == State.TalkingToPlayer || state == State.ReceivingItem)
                {
                    timeOfLastInteractionWithPlayer = IdyllTime.GetTotalGameHoursPassed();
                }
           }


        }


        
  
        public void StartConversation()
        {
            this.state = State.TalkingToPlayer;
            PlayerCharacter.instance.TalkToNPC(this);
            ClearWildPlants();
        }

        public void StartGiveInteraction()
        {
            this.state = State.TalkingToPlayer;
            PlayerCharacter.instance.TalkToNPC(this, this.characterName + ".Give");
            ClearWildPlants();
        }




        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(this.transform.position + Vector3.right * xInteractionOffset, interactDistance);
        }

        internal void ReceiveItem(TradeableItem item)
        {
            //
            string destinationYarnNode = this.characterName + ".Receives";
            EZYarnVariables.ItemForNpc = item.itemName;
            PlayerCharacter.instance.TalkToNPC(this, destinationYarnNode);
        }
    }



}
