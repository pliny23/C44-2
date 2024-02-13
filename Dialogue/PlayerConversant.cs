using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        //[SerializeField] Dialogue currentDialogue;

        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;

        public event Action onConversationUpdated;

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;

            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated();
        }
        public void Quit()
        {
            currentDialogue = null;
            TriggerExitAction();
            currentNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }
        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetText();
        }

        public Sprite GetImage()
        {
            Sprite image = currentNode.GetImage();
            if (image != null)//次のノードのImageがnullでない場合
            {
                return image;
            }
            return currentNode.GetImage();
        }

        public IEnumerable<DialogueNode> GetChoices()//プレイヤーが選択できる選択肢を取得
        {
            return currentDialogue.GetPlayerChildren(currentNode);
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
            // 現在のノードが持つプレイヤーが選択できる選択肢の数
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();//GetPlayerChildrenはDialogue.csで定義
            if (numPlayerResponses == 1)//numPlayerResponsesが1の時
            {
                DialogueNode[] Children = currentDialogue.GetPlayerChildren(currentNode).ToArray();
                if (Children.Length > 0)
                {
                    // playerChildrenが空でない場合の処理
                }
                // int randomIndex = UnityEngine.Random.Range(0, children.Count());
                // TriggerExitAction();
                // currentNode = children[randomIndex];//エラー箇所
                // TriggerEnterAction();
                // onConversationUpdated();
            }

            else if (numPlayerResponses > 1)
            {
                // 更新イベントを呼び出す
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
            // else
            // {
            //     DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            // }

            // プレイヤーのレスポンスがない場合、AIのレスポンスからランダムに次のノードを選択する
            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[randomIndex];//エラー箇所
            TriggerEnterAction();
            onConversationUpdated();
        }

        public bool HasNext()
        {
            // 現在のノードに子ノードがあるかどうかを確認する
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
