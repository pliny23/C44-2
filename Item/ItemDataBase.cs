using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]//�X�N���v�g����A�Z�b�g���쐬

//�A�C�e���f�[�^���W�񂵂ĊǗ�����N���X
//GetItemDatas() ���\�b�h��ʂ��āA�O���̃X�N���v�g���f�[�^�x�[�X���̂��ׂẴA�C�e���f�[�^�ɃA�N�Z�X�ł���
public class ItemDataBase : ScriptableObject
{
    //�C�e���f�[�^�̃��X�g���i�[���邽�߂̃t�B�[���h
    [SerializeField] private List<ItemData> itemDatas;

    //���ׂẴA�C�e���f�[�^���擾
    public List<ItemData> GetItemDatas()//<ItemData>���w�肷�邱�ƂŁAGetItemDatas���\�b�h�́uItemData�v�^�̃��X�g��Ԃ����Ƃ����m�ɂȂ�A�^���S�����m�ۂ����
    {
        return itemDatas;
    }
}