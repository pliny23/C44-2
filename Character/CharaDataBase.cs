using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

//データを集約して管理するクラス
public class CharaDataBase : ScriptableObject
{
    //イテムデータのリストを格納するためのフィールド作成
    [SerializeField] private List<CharaData> charaDatas;

    //すべてのキャラデータを取得
    public List<CharaData> GetCharaDatas()
    {
        return charaDatas;
    }
}