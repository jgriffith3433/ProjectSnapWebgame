using System;
using System.Collections.Generic;
using MHLab.ReactUI.Core;
using UnityEngine;

namespace MHLab.ReactUI.Scripts
{
    public enum ResourceType
    {
        Health,
        Mana,
        Stamina
    }

    [Serializable]
    public class PlayerSpawnedPayload : UIEventPayload
    {
        public float health;
        public float mana;
        public float stamina;
    }

    public class PlayerUsedPotion : UIEventPayload
    {
        
    }

    public class PlayerPickedPotion : UIEventPayload
    {
        
    }
    
    public class Player : MonoBehaviour
    {
        private Dictionary<ResourceType, float> _resources = new Dictionary<ResourceType, float>();
        private Potion _potion;

        protected void Start()
        {
            _resources.Add(ResourceType.Health, 1f);
            _resources.Add(ResourceType.Mana, 1f);
            _resources.Add(ResourceType.Stamina, 1f);
            
            var e = new UIEvent<PlayerSpawnedPayload>("player_spawned", new PlayerSpawnedPayload()
            {
                health = 1f,
                mana = 1f,
                stamina = 1f
            });
            UIEventDispatcher.Instance.Dispatch(e);
            
            UIEventDispatcher.Instance.RegisterHandler<PlayerUsedPotion>("player_used_potion", UsedPotionCallback);
        }

        public float GetResource(ResourceType type)
        {
            return _resources[type];
        }
        
        public float ChangeResource(ResourceType type, float amount)
        {
            var current = GetResource(type);
            current += amount;
            current = Mathf.Clamp(current, 0, 1);

            _resources[type] = current;
            
            var e = new UIEvent<ResourceEventPayload>(GetResourceEventName(type), new ResourceEventPayload()
            {
                value = current
            });
            UIEventDispatcher.Instance.Dispatch(e);
            UIBridge.DebugLog(type + " resource " + (amount >= 0 ? "increased" : "decreased"));
            
            
            return current;
        }

        private string GetResourceEventName(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Health: 
                    return "health_change";
                case ResourceType.Mana: 
                    return "mana_change";
                case ResourceType.Stamina: 
                    return "stamina_change";
            }

            return "";
        }

        public void PickupPotion(Potion potion)
        {
            _potion = potion;
            var e = new UIEvent<PlayerPickedPotion>("player_picked_potion", new PlayerPickedPotion()
            {
            });
            UIEventDispatcher.Instance.Dispatch(e);
            UIBridge.DebugLog("Player picked a potion!");
        }
        
        public void UsePotion()
        {
            if (_potion != null)
            {
                _potion.gameObject.SetActive(true);
                _potion = null;
                ChangeResource(ResourceType.Health, 0.3f);
            }
        }
        
        private void UsedPotionCallback(string eventName, UIEventPayload e)
        {
            UsePotion();
        }
    }
}