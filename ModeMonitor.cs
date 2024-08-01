using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;


public class ModeMonitor : MonoBehaviour
{
    [SerializeField] Camera mainCamera;//カメラ移動のクラス
    private RPG.Dialogue.PlayerConversant playerConversant;
    private Test2MoveCamera cameraMovement; // カメラ移動のクラスを保持するフィールド
    public bool isTalking = false;//会話中かどうか

    void Start()
    {
        cameraMovement = mainCamera.GetComponent<Test2MoveCamera>();
        playerConversant = GetComponent<RPG.Dialogue.PlayerConversant>();//PlayerConversantクラスを取得
    }

    //このクラスが呼び出されたら、それだけ通して、以降全てのMAP上のクリック＋（メニュー・キャンパスクラス）をoff...........ストップしたい時、会話・アイテムBOX・MAP上のイベント・ムービー
    public void StopCameraMovement()//カメラの停止
    {
        cameraMovement.enabled = false;
        isTalking = true;
    }
    public void StartCameraMovement()//カメラの停止
    {
        cameraMovement.enabled = true;
        isTalking = false;
    }


}
