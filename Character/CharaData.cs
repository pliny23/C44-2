using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu] //�X�N���v�g����A�Z�b�g���쐬

public class CharaData : ScriptableObject
{
    [SerializeField] private Sprite ime;//�摜
    [SerializeField] private new string name;//���O
    [SerializeField] private string info;//����
    [SerializeField] private List<string> favorites;//�D���Ȃ���
    [SerializeField] private List<string> skills;//�X�L��

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