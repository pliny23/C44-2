using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Linq;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        [NonSerialized]
        GUIStyle nodeStyle;
        [NonSerialized]
        GUIStyle playerNodeStyle;
        [NonSerialized]
        GUIStyle FullNodeStyle;
        [NonSerialized]
        DialogueNode draggingNode = null;
        [NonSerialized]
        Vector2 draggingOffset;
        [NonSerialized]
        DialogueNode creatingNode = null;
        [NonSerialized]
        DialogueNode deletingNode = null;
        [NonSerialized]
        DialogueNode linkingParentNode = null;
        Vector2 scrollPosition;
        [NonSerialized]
        bool draggingCanvas = false;
        [NonSerialized]
        Vector2 draggingCanvasOffset;

        const float canvasSize = 4000;
        const float backgroundSize = 50;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(10, 10, 10, 10);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;//背景テクスチャ
            playerNodeStyle.normal.textColor = Color.white;
            playerNodeStyle.padding = new RectOffset(10, 10, 10, 10);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);

            FullNodeStyle = new GUIStyle();
            FullNodeStyle.normal.background = EditorGUIUtility.Load("node5") as Texture2D;//背景テクスチャ
            FullNodeStyle.normal.textColor = Color.white;
            FullNodeStyle.padding = new RectOffset(10, 10, 10, 10); // パディングを設定
            FullNodeStyle.border = new RectOffset(12, 12, 12, 12); // ボーダーを設定
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected.");
            }
            else
            {
                if (!selectedDialogue.GetAllNodes().Any())    //Add this 
                {
                    selectedDialogue.CreateNode(null);  //Add this
                }

                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }

        }

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = node.IsPlayerSpeaking() ? playerNodeStyle : (node.IsFullImage() ? FullNodeStyle : nodeStyle); // プレイヤーが話しているかどうかでスタイルを選択

            GUILayout.BeginArea(node.GetRect(), style);//----------------------------------------GUILayout.BeginArea
            EditorGUI.BeginChangeCheck();

            if (node.IsFullImage())//一枚絵ノードの時
            {
                EditorGUILayout.BeginHorizontal();
                node.SetFullImage(EditorGUILayout.ObjectField("", node.GetFullImage(), typeof(Sprite), false, GUILayout.Width(65), GUILayout.Height(65)) as Sprite);

                GUILayout.Space(10);
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();

                GUIStyle blackTextStyle = new GUIStyle(EditorStyles.label);
                GUIStyle boldTextStyle = new GUIStyle(blackTextStyle);
                boldTextStyle.normal.textColor = Color.black;
                //boldTextStyle.fontStyle = FontStyle.Bold;

                //node.SetWinL(EditorGUILayout.ToggleLeft("左", node.GetWinL(), blackTextStyle, GUILayout.Width(35)));//チェックボックス

                node.SetWinL(GUILayout.Toggle(node.GetWinL(), "L", boldTextStyle, GUILayout.Width(15)));//チェックボックスなし
                if (node.GetWinL())
                {
                    node.SetWinR(false);
                    node.SetWinN(false);
                }

                node.SetWinR(GUILayout.Toggle(node.GetWinR(), "R", boldTextStyle, GUILayout.Width(15)));
                if (node.GetWinR())
                {
                    node.SetWinL(false);
                    node.SetWinN(false);
                }

                node.SetWinN(GUILayout.Toggle(node.GetWinN(), "N", boldTextStyle));
                if (node.GetWinN())
                {
                    node.SetWinL(false);
                    node.SetWinR(false);
                }


                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

                node.SetText(EditorGUILayout.TextArea(node.GetText(), GUILayout.Height(40)));//テキスト２行
            }
            else if (!node.IsPlayerSpeaking() && !node.IsFullImage())//ノーマルノードの時
            {
                GUILayout.BeginHorizontal();
                node.SetImageL(EditorGUILayout.ObjectField("", node.GetImageL(), typeof(Sprite), false, GUILayout.Width(50), GUILayout.Height(50)) as Sprite);
                node.SetImageR(EditorGUILayout.ObjectField("", node.GetImageR(), typeof(Sprite), false, GUILayout.Width(50), GUILayout.Height(50)) as Sprite);
                GUILayout.EndHorizontal();
                node.SetText(EditorGUILayout.TextArea(node.GetText(), GUILayout.Height(40)));//テキスト２行
            }
            else
            {
                // 選択肢のテキストは１行
                node.SetText(EditorGUILayout.TextField(node.GetText()));
            }

            int buttonSizeW = 40; // ボタンのサイズを変数で指定
            int buttonSizeH = 20; // ボタンのサイズを変数で指定
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("x", GUILayout.Width(buttonSizeW), GUILayout.Height(buttonSizeH))) // ボタンのサイズを半分に設定
            {
                deletingNode = node;
            }
            DrawLinkButtons(node);
            if (GUILayout.Button("+", GUILayout.Width(buttonSizeW), GUILayout.Height(buttonSizeH))) // ボタンのサイズを半分に設定
            {
                creatingNode = node;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();//-----------------------------------------------------------------GUILayout.EndArea
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            int buttonSizeH = 20; // ボタンのサイズを変数で指定
            int buttonSizeW = 40;
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link", GUILayout.Width(buttonSizeW), GUILayout.Height(buttonSizeH))) // ボタンのサイズを半分に設定
                {
                    linkingParentNode = node;
                }
            }
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("cancel", GUILayout.Width(buttonSizeW), GUILayout.Height(buttonSizeH))) // ボタンのサイズを半分に設定
                {
                    linkingParentNode = null;
                }
            }
            else if (linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("unlink", GUILayout.Width(buttonSizeW), GUILayout.Height(buttonSizeH)))
                { // ボタンのサイズを半分に設定
                    linkingParentNode.RemoveChild(node.name);
                    linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("child", GUILayout.Width(buttonSizeW), GUILayout.Height(buttonSizeH))) // ボタンのサイズを半分に設定
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Link");
                    linkingParentNode.AddChild(node.name);
                    linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }



    }
}