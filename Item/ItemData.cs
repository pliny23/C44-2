using UnityEngine;

[CreateAssetMenu] //スクリプトからアセットを作成


//ScriptableObject・・・データをまとめて保存するための箱
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite ime;//アイテム画像
    [SerializeField] private new string name;//アイテム名
    [SerializeField] private string info;//説明

    public string GetName()//アイテム名を返す
    {
        return name;
    }

    public Sprite GetIme()//アイテム画像を返す
    {
        return ime;
    }

    public string GetInfo()//アイテムの説明を返す
    {
        return info;
    }
}