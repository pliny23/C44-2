using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] Image hpbar;
    [SerializeField] int charaHp = 10;
    private const int CharaMaxHp = 100;

    private void Start()
    {
        //初回はアニメーションさせない
        float hpRatio = (float)charaHp / CharaMaxHp;
        hpbar.fillAmount = hpRatio;
    }

    void UpdateHPBar()
    {
        float targetHpRatio = (float)charaHp / CharaMaxHp;
        StartCoroutine(SmoothUpdateHPBar(targetHpRatio));
    }

    private System.Collections.IEnumerator SmoothUpdateHPBar(float targetFillAmount)
    {
        float elapsedTime = 0f;//経過時間を追跡するための変数
        float startFillAmount = hpbar.fillAmount;//アニメーションの開始時の fillAmount の値
        float duration = 1f; //アニメーションの全体の時間

        while (elapsedTime < duration)//経過時間が duration より短い間、実行
        {
            elapsedTime += Time.deltaTime;//経過時間をフレームごとに加算 Time.deltaTime は前のフレームからの経過時間
            float t = elapsedTime / duration;//現在の経過時間に対する、全体の進捗割合
            hpbar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);//startFillAmount から targetFillAmount までを t の割合で線形補間
            yield return null;//次のフレームまでコルーチンの実行を一時停止
        }

        hpbar.fillAmount = targetFillAmount; //ループが終了した後、fillAmount を targetFillAmount に設定 アニメーションの最終値が確実に設定される
    }
}
