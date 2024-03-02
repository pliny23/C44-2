using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeController : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
    [SerializeField] Color lineColor;
    [Range(0.1f, 0.5f)]
    [SerializeField] float lineWidth;


    GameObject lineObj;//線となるゲームオブジェクト用変数
    LineRenderer lineRenderer;//lineObjのlineRenderer用変数
    List<Vector2> linePoints;//コライダーのための座標を保持するリスト型の変数

    void Start()
    {
        //Listの初期化*
        linePoints = new List<Vector2>();
    }


    void Update()
    {
        //左クリックされた時
        if (Input.GetMouseButtonDown(0))
        {
            //線となるゲームオブジェクト作成
            _addLineObject();
        }
        //左クリックされ続けている時
        if (Input.GetMouseButton(0))
        {
            //lineRendererの更新処理
            _addPositionDataToLineRenderer();
        }
    }

    //ゲームオブジェクト作成関数

    void _addLineObject()
    {
        //ゲームオブジェクト作成
        lineObj = new GameObject();
        //名前をLineにする
        lineObj.name = "Line";
        //lineObjにLineRendererコンポーネントを追加
        lineObj.AddComponent<LineRenderer>();
        //lineObjにEdgeCollider2Dコンポーネントを追加
        lineObj.AddComponent<EdgeCollider2D>();
        //lineObjを自身（Stroke）の子要素に設定
        lineObj.transform.SetParent(transform);
        _initRenderer();
    }

    //LineRenderer初期化関数

    private void _initRenderer()
    {
        //LineRendererを取得
        lineRenderer = lineObj.GetComponent<LineRenderer>();
        //ポジションカウントをリセット
        lineRenderer.positionCount = 0;
        //マテリアルを設定
        lineRenderer.material = lineMaterial;
        //マテリアルの色を設定
        lineRenderer.material.color = lineColor;
        //始点の太さを設定
        lineRenderer.startWidth = lineWidth;
        //終点の太さを設定
        lineRenderer.endWidth = lineWidth;
    }
    //lineRenderer更新処理

    private void _addPositionDataToLineRenderer()
    {
        /*座標に関する処理*/
        //マウス座標取得
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        //ワールド座標へ変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        /*LineRendererに関する処理*/
        //LineRendererのポイントを増やす
        lineRenderer.positionCount += 1;
        //LineRendererのポジションを設定
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);

        /*EdgeCollider2Dに関する処理*/
        //ワールド座標をリストに追加
        linePoints.Add(worldPos);
        //EdgeCollider2Dのポイントを設定
        lineObj.GetComponent<EdgeCollider2D>().SetPoints(linePoints);
    }
}