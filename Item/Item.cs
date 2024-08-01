using System;

//アイテムのデータとその操作を管理
[Serializable]//クラスのインスタンスをシリアル化可能にする
public class Item
{
    public ItemData itemData;//アイテムデータ格納
    public int count;//アイテム数


    public void AddCount(int value)//アイテムを増やす
    {
        count += value;
    }
    public int GetCount()//個数を返す
    {
        return count;
    }
}
