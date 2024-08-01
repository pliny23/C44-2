using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//�A�C�e�����X�g���Ǘ����A�A�C�e���̒ǉ��⌟�����s��
public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();//�A�C�e���Ǘ��p���X�g�iItem �N���X�̗v�f���i�[�j

    public bool HasItem(ItemData itemData)    //�A�C�e���������Ă��邩���ׂ�
    {
        return items.Any(i => i.itemData == itemData);
    }

    public Item FindItem(ItemData itemData)   //�����ς݃A�C�e����T��
    {
        return items.Find(i => i.itemData == itemData);
    }

    public void AddItem(Item item)//�A�C�e���ǉ�
    {
        //�����ς݂̃A�C�e�����擾����
        Item ownedItem = FindItem(item.itemData);
        //�����ς݂Ȃ�
        if (ownedItem != null)
        {
            //�ǉ��������n��
            ownedItem.AddCount(item.count);
        }
        else
        {
            //�����Ă��Ȃ���ΐV�K�ɒǉ�
            items.Add(item);
        }
    }
}