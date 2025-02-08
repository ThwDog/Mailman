using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit3D
{
    public class InteractionTrigger : MonoBehaviour
    {
        public static InteractionTrigger _instance;
        public static InteractionTrigger instance{get{return _instance;}}
        public LayerMask layers;
        public UnityEvent OnEnter, OnExit;
        new SphereCollider collider;
        /*private DialogueCanvasController dialogue;

        private void Awake()
        {
            if(gameObject.GetComponent<ItemHolder>())
                if(dialogue == null)
                    dialogue = GameObject.Find("DialogueCanvas").gameObject.GetComponent<DialogueCanvasController>();
        }*/

        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<SphereCollider>();
            collider.radius = 5;
            collider.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                OnEnter.Invoke();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                OnExit.Invoke();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
        }

        void OnDrawGizmosSelected()
        {
            //need to inspect events and draw arrows to relevant gameObjects.
        }

        

    } 
}
