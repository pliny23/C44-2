using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using RPG.Control;//PlayerController で定義　

//会話が発生対象にアタッチ、会話を発生させるコード

namespace RPG.Dialogue
{
    //PlayerControllerとIRaycastable の引継ぎが必要
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] GameObject player;
        public void Test2()//スプライトにボタン属性をつける
        {
            player.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
        }
    }
}