using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//アイテムリストを管理し、アイテムの追加や検索を行う
public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();//アイテム管理用リスト（Item クラスの要素を格納）

    public bool HasItem(ItemData itemData)    //アイテムを持っているか調べる
    {
        return items.Any(i => i.itemData == itemData);
    }

    public Item FindItem(ItemData itemData)   //所持済みアイテムを探す
    {
        return items.Find(i => i.itemData == itemData);
    }

    public void AddItem(Item item)//アイテム追加
    {
        //所持済みのアイテムを取得する
        Item ownedItem = FindItem(item.itemData);
        //所持済みなら
        if (ownedItem != null)
        {
            //追加する個数を渡す
            ownedItem.AddCount(item.count);
        }
        else
        {
            //持っていなければ新規に追加
            items.Add(item);
        }
    }
}