using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;//インベントリ
    public GameObject panel; //UIのパネル
    public List<ItemPanel> itemPanels = new List<ItemPanel>();
    private ModeMonitor modeMonitor;


    private void Awake()
    {
        //inventory変数にインベントリのインスタンスを取得
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        modeMonitor = GameObject.Find("GameManager").GetComponent<ModeMonitor>(); //初期化

    }

    public void OnOpenButtonClick()//ボタンをクリックしたら
    {
        if (!modeMonitor.isTalking)//会話中は開かない
        {
            panel.SetActive(!panel.activeSelf);//panelがactiveなら非アクティブに、非アクティブならアクティブにする
        }
    }

    private void Update()
    {
        UpdateItemPanel();
    }

    //アイテムパネルを更新する
    public void UpdateItemPanel()
    {
        List<Item> items = inventory.items;
        for (int i = 0; i < itemPanels.Count; i++)
        {
            //インベントリのアイテムの数以下のパネルのみ更新する
            if (i < items.Count)
            {
                //パネルにアイテムの情報を渡す
                itemPanels[i].OnUpdateItemPanel(items[i]);
            }
            else
            {
                //インベントリにもうアイテムがなければ残りのパネルを非表示にする
                itemPanels[i].Hide();
            }
        }
    }
}
