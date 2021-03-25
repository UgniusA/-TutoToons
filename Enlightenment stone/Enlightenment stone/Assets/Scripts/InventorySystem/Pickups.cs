using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    private Inventory inventory;
    public GameObject itemButton;

    public bool canPickUp = true;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (canPickUp)
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null)
            {
                for (int i = 0; i < inventory.inventorySlots.Length; i++)
                {
                    if (inventory.inventorySlots[i].isFull == false)
                    {
                        inventory.inventorySlots[i].isFull = true;
                        GameObject item = Instantiate(itemButton, inventory.inventorySlots[i].slot.transform, false);
                        if (item.GetComponent<Useitems>() != null)
                        {
                            item.GetComponent<Useitems>().i = i;
                        }
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            canPickUp = true;
        }
    }
}
