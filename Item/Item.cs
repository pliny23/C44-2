using System;

//�A�C�e���̃f�[�^�Ƃ��̑�����Ǘ�
[Serializable]//�N���X�̃C���X�^���X���V���A�����\�ɂ���
public class Item
{
    public ItemData itemData;//�A�C�e���f�[�^�i�[
    public int count;//�A�C�e����


    public void AddCount(int value)//�A�C�e���𑝂₷
    {
        count += value;
    }
    public int GetCount()//����Ԃ�
    {
        return count;
    }
}
