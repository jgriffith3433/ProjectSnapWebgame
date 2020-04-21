using UnityEngine;

namespace MHLab.ReactUI.Scripts
{
    public class Potion : MonoBehaviour
    {
        protected void Update()
        {
            transform.Rotate(Vector3.up, 2.5f, Space.World);
        }
        
        protected void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();

            if (player != null)
            {
                player.PickupPotion(this);
                this.gameObject.SetActive(false);
            }
        }
    }
}