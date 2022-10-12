using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList = new List<Item>();

    public List<Item> ItemLists
    {
        get => _itemList;
    }
    public int ArrangeSpaceForItem(RouletteSlice slice)
    {
        int i = 0;
        foreach (Item item in _itemList)
        {
            if (item.ItemType == null)
            {
                item.ItemType = slice.Type;
                item.image.sprite = slice.Image.sprite;
                item.count = slice.Value;
                item.uiText.SetText(item.count.ToString());
                item.image.gameObject.SetActive(true);
                item.uiText.gameObject.SetActive(true);
                return i;
                
            }

            else if (item.ItemType == slice.Type)
            {
                item.count += slice.Value;
                item.uiText.SetText(item.count.ToString());
                return i;
                
            }
            i++;
        }
        return -1;
    }
    public void CopyItems(ItemList doppelganger)
    {
        var i = 0;
        foreach(Item item in _itemList)
        {
            var doppelItem = doppelganger.ItemLists[i];
            if(doppelItem.ItemType != null)
            {
                item.ItemType = doppelItem.ItemType;
                item.image.sprite = doppelItem.image.sprite;
                item.count = doppelItem.count;
                item.uiText.SetText(item.count.ToString());
                item.image.gameObject.SetActive(true);
                item.uiText.gameObject.SetActive(true);
                i++;
            }
            else
            {
                break;
            }
            
        }
    }
}
