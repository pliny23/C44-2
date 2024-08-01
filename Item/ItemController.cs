using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerClickHandler
{
    Inventory inventory;
    public Item item; //�N���b�N�����A�C�e���̏���ێ�����ϐ�
    private ModeMonitor modeMonitor;


    private void Awake()
    {
        //�C���x���g���̃A�C�e�����X�g��ǂݍ���
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        modeMonitor = GameObject.Find("GameManager").GetComponent<ModeMonitor>(); //������

    }

    public void OnPointerClick(PointerEventData eventData)//�A�C�e�����N���b�N�����Ƃ�
    {
        //�C���x���g���ɃA�C�e����n��
        //(���̃A�C�e�����C���x���g���ɂǂ��ǉ����邩�ȂǍׂ��������͂����ɏ����Ȃ�)
        if (!modeMonitor.isTalking)//��b���̓A�C�e�������Ȃ�
        {
            inventory.AddItem(item);
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
