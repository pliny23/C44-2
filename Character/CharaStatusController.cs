using UnityEngine;

public class CharaStatusController : MonoBehaviour
{
    [SerializeField] private Chara chara; // Charaクラスのインスタンスを保持


    // 神官の好感度を1上げるメソッドを呼び出す
    public void ShinkanLikeScore()
    {
        if (chara != null)
        {
            chara.UPShinkanScore();
            Debug.Log("神官の現在の好感度: " + chara.GetShinkanScore());
        }
        else
        {
            Debug.LogError("Chara is not assigned.");
        }
    }

    // 冒険者の好感度を1上げるメソッドを呼び出す
    public void BoukensyaLikeScore()
    {
        if (chara != null)
        {
            chara.UPBoukensyaScore();
            Debug.Log("冒険者の現在の好感度: " + chara.GetBoukensyaScore());
        }
        else
        {
            Debug.LogError("Chara is not assigned.");
        }
    }
}
