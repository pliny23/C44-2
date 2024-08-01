using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Image images; //画像
    public Text nam; //名前
    public Text info; //説明
    public Text count; //個数

    //アイテムパネルを更新
    public void OnUpdateItemPanel(Item item)
    {
        gameObject.SetActive(true);
        ItemData itemData = item.itemData; //Itemの情報
        images.sprite = itemData.GetIme(); //画像を表示
        nam.text = itemData.GetName(); //名前を表示
        info.text = itemData.GetInfo(); //説明を表示
        count.text = item.GetCount().ToString(); //個数を表示
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
