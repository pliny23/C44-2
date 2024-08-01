using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

//�f�[�^���W�񂵂ĊǗ�����N���X
public class CharaDataBase : ScriptableObject
{
    //�C�e���f�[�^�̃��X�g���i�[���邽�߂̃t�B�[���h�쐬
    [SerializeField] private List<CharaData> charaDatas;

    //���ׂẴL�����f�[�^���擾
    public List<CharaData> GetCharaDatas()
    {
        return charaDatas;
    }
}