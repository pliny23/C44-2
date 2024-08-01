using System;


[Serializable]// キャラの状態を保持、内部的な処理を担当するクラス
public class Chara
{
    //     public CharaData charaData;
    //     public int likeScore;


    //     public void UPCharaScore(int value)// 好感度上昇
    //     {
    //         likeScore += value;
    //     }
    //     public int GetLikeScore()// 好感度を返す
    //     {
    //         return likeScore;
    //     }

    // ーーーーーーーーーーーーーーーーーーー↑　　引数を利用できる場合　　　↓利用できない場合

    public int shinkan_likeScore;//神官の好感度
    public int boukensya_likeScore;//冒険者の好感度


    // 神官の好感度を1上げる関数
    public void UPShinkanScore()
    {
        shinkan_likeScore += 1;
    }

    // 神官の好感度を返す関数
    public int GetShinkanScore()
    {
        return shinkan_likeScore;
    }

    // 冒険者の好感度を1上げる関数
    public void UPBoukensyaScore()
    {
        boukensya_likeScore += 1;
    }

    // 冒険者の好感度を返す関数
    public int GetBoukensyaScore()
    {
        return boukensya_likeScore;
    }











}
