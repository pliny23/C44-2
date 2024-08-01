using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu] //スクリプトからアセットを作成

public class CharaData : ScriptableObject
{
    [SerializeField] private Sprite ime;//画像
    [SerializeField] private new string name;//名前
    [SerializeField] private string info;//説明
    [SerializeField] private List<string> favorites;//好きなもの
    [SerializeField] private List<string> skills;//スキル

    public string GetName()
    {
        return name;
    }

    public Sprite GetIme()
    {
        return ime;
    }

    public string GetInfo()
    {
        return info;
    }
    public List<string> GetFavorites()
    {
        return favorites;
    }
    public List<string> GetSkills()
    {
        return skills;
    }
}