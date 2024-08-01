using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGauge : MonoBehaviour
{
    [SerializeField] GameObject wave;//変数に応じて動かしたい画像
    public int Gauge = 30;//現在の位置（０～１００）
    public float GaugeMaxPosition; // 画像の位置の最大値を表す変数
    public float GaugeMinPosition; // 画像の位置の最小値を表す変数


    //ボタンを押すとインスペクタで記載した変数に更新する
    public void ClikcButton()
    {
        // インスペクタで設定された値を使用してGaugeを更新
        Gauge = Mathf.Clamp(Gauge, 0, 100); // Gaugeの値を0から100の範囲に制限

        // 波の位置を更新
        UpdateWavePosition();
    }


    //Gauge変数に応じてwaveのY座標を変更する
    //Gauge=0の時GaugeMinPosition、gauge=100の時GaugeMaxPosition
    void UpdateWavePosition()
    {
        float normalizedGauge = Gauge / 100f;
        float targetYPosition = Mathf.Lerp(GaugeMinPosition, GaugeMaxPosition, normalizedGauge);
        StartCoroutine(MoveWaveSmooth(targetYPosition));
    }

    IEnumerator MoveWaveSmooth(float targetYPosition)
    {
        float elapsedTime = 0f;
        float duration = 1f;
        Vector3 startPosition = wave.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetYPosition, startPosition.z);

        while (elapsedTime < duration)
        {
            wave.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        wave.transform.position = targetPosition;
    }


}
