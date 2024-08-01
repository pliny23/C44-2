using UnityEngine;

[CreateAssetMenu] //�X�N���v�g����A�Z�b�g���쐬


//ScriptableObject�E�E�E�f�[�^���܂Ƃ߂ĕۑ����邽�߂̔�
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite ime;//�A�C�e���摜
    [SerializeField] private new string name;//�A�C�e����
    [SerializeField] private string info;//����

    public string GetName()//�A�C�e������Ԃ�
    {
        return name;
    }

    public Sprite GetIme()//�A�C�e���摜��Ԃ�
    {
        return ime;
    }

    public string GetInfo()//�A�C�e���̐�����Ԃ�
    {
        return info;
    }
}