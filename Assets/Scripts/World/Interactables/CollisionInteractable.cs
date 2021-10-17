using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BotScheduler.World {
    public class CollisionInteractable : MonoBehaviour
    {
        public UnityEvent<GameObject> OnEnter = new UnityEvent<GameObject>();
        public UnityEvent<GameObject> OnLeave = new UnityEvent<GameObject>();
        public UnityEvent<GameObject> OnChange = new UnityEvent<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            OnEnter.Invoke(other.gameObject);
            OnChange.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnLeave.Invoke(other.gameObject);
            OnChange.Invoke(other.gameObject);
        }
    }
}