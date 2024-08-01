using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using RPG.Control;//PlayerController で定義　

//会話が発生対象にアタッチ、会話を発生させるコード

namespace RPG.Dialogue//ここはdialogurを指定するだけ　実行はModemonitorでやる　dialogue情報を引き渡す
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] List<Dialogue> dialogues = null;
        // [SerializeField] GameObject player;
        PlayerConversant playerConversant;
        ModeMonitor modeMonitor;

        void Start()
        {
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            playerConversant = gameManager.GetComponent<PlayerConversant>();
            modeMonitor = gameManager.GetComponent<ModeMonitor>();
        }


        public void FetchDialogue()//シリアライズで指定されたダイアログをリストから読み込む
        {
            if (dialogues != null && dialogues.Count > 0)
            {
                // リストの最初のダイアログを使用
                Dialogue selectedDialogue = dialogues[0];
                playerConversant.StartDialogue(this, selectedDialogue);
                //ModeMonitorに会話の開始を知らせる
                modeMonitor.StopCameraMovement();
            }
            else
            {
                Debug.LogWarning("ダイアログリストが空です。");
            }
        }
    }
}