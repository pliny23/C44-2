using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;//�C���x���g��
    public GameObject panel; //UI�̃p�l��
    public List<ItemPanel> itemPanels = new List<ItemPanel>();
    private ModeMonitor modeMonitor;


    private void Awake()
    {
        //inventory�ϐ��ɃC���x���g���̃C���X�^���X���擾
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        modeMonitor = GameObject.Find("GameManager").GetComponent<ModeMonitor>(); //������

    }

    public void OnOpenButtonClick()//�{�^�����N���b�N������
    {
        if (!modeMonitor.isTalking)//��b���͊J���Ȃ�
        {
            panel.SetActive(!panel.activeSelf);//panel��active�Ȃ��A�N�e�B�u�ɁA��A�N�e�B�u�Ȃ�A�N�e�B�u�ɂ���
        }
    }

    private void Update()
    {
        UpdateItemPanel();
    }

    //�A�C�e���p�l�����X�V����
    public void UpdateItemPanel()
    {
        List<Item> items = inventory.items;
        for (int i = 0; i < itemPanels.Count; i++)
        {
            //�C���x���g���̃A�C�e���̐��ȉ��̃p�l���̂ݍX�V����
            if (i < items.Count)
            {
                //�p�l���ɃA�C�e���̏���n��
                itemPanels[i].OnUpdateItemPanel(items[i]);
            }
            else
            {
                //�C���x���g���ɂ����A�C�e�����Ȃ���Ύc��̃p�l�����\���ɂ���
                itemPanels[i].Hide();
            }
        }
    }
}
