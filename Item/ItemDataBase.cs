using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]//スクリプトからアセットを作成

//アイテムデータを集約して管理するクラス
//GetItemDatas() メソッドを通じて、外部のスクリプトがデータベース内のすべてのアイテムデータにアクセスできる
public class ItemDataBase : ScriptableObject
{
    //イテムデータのリストを格納するためのフィールド
    [SerializeField] private List<ItemData> itemDatas;

    //すべてのアイテムデータを取得
    public List<ItemData> GetItemDatas()//<ItemData>を指定することで、GetItemDatasメソッドは「ItemData」型のリストを返すことが明確になり、型安全性が確保される
    {
        return itemDatas;
    }
}