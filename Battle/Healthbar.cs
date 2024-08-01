using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Image burnImage;

    public float duration = 0.5f;//何秒かけてhealthを減らすか
    public float strength = 20f;
    public int vibrate = 100;

    // public float debugDamageRate = 0.1f;//デバッグ用　ダメージ量

    private float currentRate = 1f;//現在のHP

    private void Start()
    {
        SetGauge(1f);//全快からスタート
    }

    public void SetGauge(float value)
    {
        // DoTweenを連結して動かす
        healthImage.DOFillAmount(value, duration)
            .OnComplete(() =>
            {
                burnImage
                    .DOFillAmount(value, duration / 2f)
                    .SetDelay(0.5f);
            });
        transform.DOShakePosition(
            duration / 2f,
            strength, vibrate);

        currentRate = value;
    }

    public void TakeDamage(float rate)//ダメージを受けた時　何割ダメージを受けたか
    {
        SetGauge(currentRate - rate);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))//スペースで呼び出す
    //     {
    //         TakeDamage(debugDamageRate);
    //     }

    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         transform.DOShakePosition(
    //             duration / 2f,
    //             strength, vibrate);
    //     }
    // }
}