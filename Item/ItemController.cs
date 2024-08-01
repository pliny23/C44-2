using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerClickHandler
{
    Inventory inventory;
    public Item item; //クリックしたアイテムの情報を保持する変数
    private ModeMonitor modeMonitor;


    private void Awake()
    {
        //インベントリのアイテムリストを読み込む
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        modeMonitor = GameObject.Find("GameManager").GetComponent<ModeMonitor>(); //初期化

    }

    public void OnPointerClick(PointerEventData eventData)//アイテムをクリックしたとき
    {
        //インベントリにアイテムを渡す
        //(そのアイテムをインベントリにどう追加するかなど細かい処理はここに書かない)
        if (!modeMonitor.isTalking)//会話中はアイテムを取れない
        {
            inventory.AddItem(item);
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
