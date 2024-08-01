// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// //トリガーで引数を使用できるまでの仮のクラス　各キャラごとに関数を作成する
// public class Chara_LikeUP : MonoBehaviour
// {

//     //CharaクラスのcharaDataをシリアライズフィールドで指定
//     [SerializeField] private CharaData charaData;
//     //CharaクラスのlikeScoreをシリアライズフィールドで指定
//     [SerializeField] private int likeScore;

//     private Chara chara;

//     void Start()
//     {
//         // Charaクラスのインスタンスを作成し、charaDataを設定
//         chara = new Chara { charaData = charaData, likeScore = 0 };
//     }

//     public void LikeUP(int value)
//     {
//         // 好感度を増減
//         chara.UPCharaScore(value);
//         Debug.Log("Current Like Score: " + chara.GetLikeScore());
//     }
// }
