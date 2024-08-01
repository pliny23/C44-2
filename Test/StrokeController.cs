using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//設定データ用クラス
[System.Serializable]
public class LineSettings
{
    public Material lineMaterial;
    public Gradient lineColor;
    [Range(0.1f, 0.5f)] public float lineWidth = 0.1f; //線幅 
    [Range(0f, 1f)] public float lineTolerance = 0.1f; //許容値
    [Range(0f, 180f)] public float cornerThreshold = 45f; //角と判定するしきい値
}

public class StrokeController : MonoBehaviour
{
    [SerializeField] LineSettings lineSettings; //描画するラインの設定

    LineController currentLine; //描画中のラインオブジェクト
    List<LineController> lines = new(); //描画したラインオブジェクトのリスト

    void Update()
    {
        //左クリックされた時
        if (Input.GetMouseButtonDown(0))
        {
            //描画開始
            StartStroke();
        }

        //左クリックされ続けている時
        if (Input.GetMouseButton(0))
        {
            //描画更新
            UpdateStroke();
        }

        //左クリックが終わった時
        if (Input.GetMouseButtonUp(0))
        {
            //描画終了
            EndStroke();
        }
    }

    //描画開始
    private void StartStroke()
    {
        CreateLineObject();
    }

    //描画更新
    private void UpdateStroke()
    {
        /*座標に関する処理*/
        //マウス座標取得
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        //ワールド座標へ変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        //描画中のラインにポイントを追加
        currentLine.AddPoint(worldPos);
    }

    //描画終了
    private void EndStroke()
    {
        //描画中のラインを確定させる
        currentLine.Build();

        //完成したラインの情報を確認
        Debug.Log("角の数: " + currentLine.CornerCount);
        Debug.Log("交差の数: " + currentLine.CrossCount);

        //どこか1箇所でも交差していればループしているとみなせる
        Debug.Log("ループ: " + (currentLine.CrossCount > 0));
    }

    //ライン描画用ゲームオブジェクト作成
    private void CreateLineObject()
    {
        //ゲームオブジェクトを作成しライン制御用コンポーネント追加
        //同時に追加したコンポーネントを現在描画中のラインとして変数に保持
        currentLine = new GameObject("Line").AddComponent<LineController>();
        currentLine.transform.SetParent(transform); //自身の子に追加
        currentLine.Initialize(lineSettings); //初期化

        //描画したラインのリストに追加
        //あとで古いラインの情報を取得したり消したりするときに使えるように
        lines.Add(currentLine);
    }
}