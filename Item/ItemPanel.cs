using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Image images; //�摜
    public Text nam; //���O
    public Text info; //����
    public Text count; //��

    //�A�C�e���p�l�����X�V
    public void OnUpdateItemPanel(Item item)
    {
        gameObject.SetActive(true);
        ItemData itemData = item.itemData; //Item�̏��
        images.sprite = itemData.GetIme(); //�摜��\��
        nam.text = itemData.GetName(); //���O��\��
        info.text = itemData.GetInfo(); //������\��
        count.text = item.GetCount().ToString(); //����\��
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
