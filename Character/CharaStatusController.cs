using UnityEngine;

public class CharaStatusController : MonoBehaviour
{
    [SerializeField] private Chara chara; // Chara�N���X�̃C���X�^���X��ێ�


    // �_���̍D���x��1�グ�郁�\�b�h���Ăяo��
    public void ShinkanLikeScore()
    {
        if (chara != null)
        {
            chara.UPShinkanScore();
            Debug.Log("�_���̌��݂̍D���x: " + chara.GetShinkanScore());
        }
        else
        {
            Debug.LogError("Chara is not assigned.");
        }
    }

    // �`���҂̍D���x��1�グ�郁�\�b�h���Ăяo��
    public void BoukensyaLikeScore()
    {
        if (chara != null)
        {
            chara.UPBoukensyaScore();
            Debug.Log("�`���҂̌��݂̍D���x: " + chara.GetBoukensyaScore());
        }
        else
        {
            Debug.LogError("Chara is not assigned.");
        }
    }
}
