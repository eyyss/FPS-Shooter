using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode _g;
    public KeyCode _e;
    public bool PickUpKey;
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item")) 
        {
            if (PickUpKey)
                Inventory.Instance.AddItem(other.GetComponent<ItemHolder>().Item,other.transform);
        }
    }
    public void Update()
    {
        PickUpKey = Input.GetKey(_e);
        if (Input.GetKeyDown(_g))
        {
            Inventory.Instance.RemoveItem();
        }
   
    }
}
