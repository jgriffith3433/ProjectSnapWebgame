using System;
using MHLab.ReactUI.Core;
using UnityEngine;

namespace MHLab.ReactUI.Scripts
{
    public class TriggerResourceEvent : MonoBehaviour
    {
        public ResourceType ResourceType;
        private float _timer;
        
        protected void OnTriggerStay(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            _timer += Time.deltaTime;

            if (player != null && _timer >= 1)
            {
                _timer = 0;
                player.ChangeResource(ResourceType, -0.1f);
                
            }
        }
    }
}