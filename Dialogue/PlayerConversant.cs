using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;

        public string GetText()
        {
            if (currentDialogue == null)
            {
                return "";
            }

            return currentDialogue.GetRootNode().GetText();
        }
        public Sprite GetImage()
        {
            if (currentDialogue == null)
            {
                return null;
            }

            return currentDialogue.GetRootNode().GetImage();
        }
    }
}