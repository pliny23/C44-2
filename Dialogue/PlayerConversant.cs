using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RPG.Core;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        //[SerializeField] Dialogue currentDialogue;

        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;
        bool isFull = false;
        private ModeMonitor modeMonitor;
        public event Action onConversationUpdated;


        void Start()
        {
            modeMonitor = GetComponent<ModeMonitor>(); //初期化
        }


        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            if (!modeMonitor.isTalking)
            {
                currentConversant = newConversant;
                currentDialogue = newDialogue;

                currentNode = currentDialogue.GetRootNode();
                TriggerEnterAction();
                onConversationUpdated();

                modeMonitor = GetComponent<ModeMonitor>();
            }

        }
        public void Quit()//会話の終了
        {
            currentDialogue = null;
            TriggerExitAction();
            currentNode = null;
            isChoosing = false;
            isFull = false;//フル画像を非表示
            //顔画像false
            currentConversant = null;
            onConversationUpdated();
            modeMonitor.StartCameraMovement();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }
        public bool IsFull()
        {
            return isFull;
        }
        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetText();
        }

        public Sprite GetImageL()
        {

            return currentNode.GetImageL();
        }
        public Sprite GetImageR()
        {

            return currentNode.GetImageR();
        }
        public Sprite GetFullImage()
        {

            return currentNode.GetFullImage();
        }
        public bool GetWinR()
        {

            return currentNode.GetWinR();
        }
        public bool GetWinL()
        {

            return currentNode.GetWinL();
        }

        public IEnumerable<DialogueNode> GetChoices()//プレイヤーが選択できる選択肢を取得
        {
            return currentDialogue.GetPlayerChildren(currentNode);//GetPlayerChildrenはDialogue.csで定義
        }

        public void SelectChoice(DialogueNode chosenNode)//選択肢を選んだ後
        {
            currentNode = chosenNode;// 選択されたノードを現在のノードとして設定
            TriggerEnterAction();
            isChoosing = false;// 選択モードを終了
            Next();
        }

        public void Next()
        {
            //★このコード１行を以前に戻したい
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();

            if (numPlayerResponses > 0)
            {
                // プレイヤーのレスポンスがある場合、選択モードに移行して会話更新イベントを呼び出す
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
            // 現在のノードがフルイメージのレスポンスを持っているかどうかを確認
            int numFullImageResponses = currentDialogue.GetFullImageChildren(currentNode).Count();
            if (numFullImageResponses > 0)
            {
                isFull = true;
            }
            else
            {

                isFull = false;
            }


            // プレイヤーのレスポンスがない場合、AIのレスポンスからランダムに次のノードを選択する
            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[randomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;

        }

        private void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
