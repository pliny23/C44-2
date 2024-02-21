using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;
        [SerializeField]
        bool isFullImage = false;
        [SerializeField]
        string text;
        [SerializeField]
        bool winL;//吹き出し
        [SerializeField]
        bool winR;//吹き出し
        [SerializeField]
        bool winN;//吹き出し
        [SerializeField]
        Sprite imageL;
        [SerializeField]
        Sprite imageR;
        [SerializeField]
        Sprite fullImage;
        [SerializeField]
        List<string> children = new List<string>();
        [SerializeField]
        Rect rect = new Rect(0, 0, 220, 135);//エディタノードのサイズ
        [SerializeField]
        string onEnterAction;
        [SerializeField]
        string onExitAction;//会話を終了させる

        public Rect GetRect()
        {
            if (isPlayerSpeaking)
            {
                return new Rect(rect.position.x, rect.position.y, rect.width, 62);//選択肢ノードの高さ
            }
            if (isFullImage)
            {
                return new Rect(rect.position.x, rect.position.y, rect.width, 150);//一枚絵ノードの高さ
            }
            else
            {
                return rect;
            }
        }

        public string GetText()
        {
            return text;
        }
        public bool GetWinL()//---------------吹き出し
        {
            return winL;
        }
        public bool GetWinR()//---------------吹き出し
        {
            return winR;
        }
        public bool GetWinN()//---------------吹き出し
        {
            return winN;
        }

        public Sprite GetImageL()
        {
            return imageL;
        }

        public Sprite GetImageR()
        {
            return imageR;
        }

        public Sprite GetFullImage()
        {
            return fullImage;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public bool IsFullImage()
        {
            return isFullImage;
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void SetImageL(Sprite newImageL)
        {
            if (newImageL != imageL)
            {
                Undo.RecordObject(this, "Update Dialogue ImageL");
                imageL = newImageL;
                EditorUtility.SetDirty(this);
            }
        }

        public void SetImageR(Sprite newImageR)
        {
            if (newImageR != imageR)
            {
                Undo.RecordObject(this, "Update Dialogue ImageR");
                imageR = newImageR;
                EditorUtility.SetDirty(this);
            }
        }

        public void SetFullImage(Sprite newFullImage)
        {
            if (newFullImage != fullImage)
            {
                Undo.RecordObject(this, "Update Dialogue FullImage");
                fullImage = newFullImage;
                EditorUtility.SetDirty(this);
            }
        }


                public void SetWinL(bool newWinL)//------------------------------チェックボックス
        {
            if (newWinL != winL)
            {
                Undo.RecordObject(this, "Update Dialogue WinL");
                winL = newWinL;
                EditorUtility.SetDirty(this);
            }
        }
                public void SetWinR(bool newWinR)//------------------------------チェックボックス
        {
            if (newWinR != winR)
            {
                Undo.RecordObject(this, "Update Dialogue WinR");
                winR = newWinR;
                EditorUtility.SetDirty(this);
            }
        }
                public void SetWinN(bool newWinN)//------------------------------チェックボックス
        {
            if (newWinN != winN)
            {
                Undo.RecordObject(this, "Update Dialogue WinN");
                winN = newWinN;
                EditorUtility.SetDirty(this);
            }
        }


        public void SetPlayerIsSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Changed Dialogue Node Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        public void SetIsFullImage(bool newIsFullImage)
        {
            Undo.RecordObject(this, "Changed Dialogue Node FullImage");
            isFullImage = newIsFullImage;
            EditorUtility.SetDirty(this);
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
#endif
    }
}