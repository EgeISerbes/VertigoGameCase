using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
   [SerializeField] private List<Item> _itemList = new List<Item>();

    public void ArrangeSpaceForItem()
    {
        foreach (Item item in _itemList)
        {
            
        }
    }
}
