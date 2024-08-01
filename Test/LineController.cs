using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//必須コンポーネントの指定
//ゲームオブジェクトにアタッチされた時、指定コンポーネントが無ければ自動的に追加する
//指定したコンポーネントの削除を無効にする
[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class LineController : MonoBehaviour
{
    LineRenderer lineRenderer; //レンダラー
    EdgeCollider2D edgeCollider; //コライダー
    float lineTolerance; //許容値
    float cornerThreshold; //角と判定するしきい値
    List<Vector2> points; //座標情報

    public int CornerCount { get; private set; } //角の数
    public int CrossCount { get; private set; } //交差の数

    private void Awake()
    {
        //オブジェクト生成時にコンポーネントを取得
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    //初期化(StrokeControllerから設定を受け取る)
    public void Initialize(LineSettings settings)
    {
        //LineRendererの描画設定を反映
        lineRenderer.material = settings.lineMaterial;
        lineRenderer.colorGradient = settings.lineColor;
        lineRenderer.startWidth = settings.lineWidth;
        lineRenderer.endWidth = settings.lineWidth;

        //簡略化の許容値と角のしきい値を設定
        lineTolerance = settings.lineTolerance;
        cornerThreshold = settings.cornerThreshold;

        //座標情報を初期化
        points = new();
        lineRenderer.positionCount = 0;
        edgeCollider.SetPoints(new());
        Debug.Log("Line初期化");
    }

    //ポイント追加
    public void AddPoint(Vector3 worldPos)
    {
        //座標を追加
        points.Add(worldPos);

        //LineRenderer更新
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);

        //EdgeCollider2D更新
        edgeCollider.SetPoints(points);
    }

    //追加したポイントを元に最終的な線を確定させる
    public void Build()
    {
        //簡略化
        Simplify();

        //角と交差を求める
        CornerCount = GetCornerCount();
        CrossCount = GetCrossCount();

        //交差の数を角の数に加算(交差1箇所ごとに角とみなす)
        CornerCount += CrossCount;
    }

    //簡略化
    private void Simplify()
    {
        //許容値をもとに座標情報を削減
        LineUtility.Simplify(points, lineTolerance, points);

        //LineRendererのポジション数を削減後のポイント数に合わせる
        lineRenderer.positionCount = points.Count;
        //SelectでList<Vector2>からList<Vecotr3>に変換し、ToArrayでVector3[]にする
        lineRenderer.SetPositions(points.Select(p => (Vector3)p).ToArray());

        //EdgeCollider2D更新
        edgeCollider.SetPoints(points);
    }

    //角がいくつあるか求める(交差によって生じる角は含まれない)
    private int GetCornerCount()
    {
        int cornerCount = 0; //カウント用変数

        //各ポイントを取り出して調べる
        //先頭と最後のポイントは角をなさないので省略
        for (int i = 1; i < points.Count - 1; i++)
        {
            //あるポイントで線の進む向きが何度変わったか求める
            Vector2 from = points[i] - points[i - 1];
            Vector2 to = points[i + 1] - points[i];
            float angle = Vector2.Angle(from, to);

            //角度がしきい値以上ならカウントする
            if (angle >= cornerThreshold)
            {
                cornerCount++;
            }
        }

        return cornerCount;
    }

    //線が交差している数を求める
    private int GetCrossCount()
    {
        int crossCount = 0; //カウント用変数

        //各ポイント間のすべての線を取り出して交差しているか判定する
        //先頭の点から開始して後ろから3番目の点まで繰り返す(最後の線は判定しなくて良いため)
        for (int i = 0; i < points.Count - 2; i++)
        {
            //現在の点とその次の点を結ぶ線のデータ
            //(型 名前, 型 名前) 変数名 はタプルという書き方です
            //以下の例では line1 という変数が作られ、この変数は line1.start と line2.end という2つの値を持つことができます
            //特定の名前付きの値をもつデータを作りたいけれどクラスを宣言するまでもない時などに使います
            (Vector2 start, Vector2 end) line1 = (points[i], points[i + 1]);

            //現在の線より先の線をすべて交差しているか確認します(現在の線より前はチェック済み)
            //こちらは最後の線まで確認する必要があるので後ろから2番目の点まで繰り返します
            for (int j = i + 1; j < points.Count - 1; j++)
            {
                //交差判定を行う線データ
                (Vector2 start, Vector2 end) line2 = (points[j], points[j + 1]);
                
                //交差しているならカウントを増やす
                if (IsCrossing(line1, line2))
                {
                    crossCount++; 
                }
            }
        }

        return crossCount;
    }

    //2つの線が交差しているか判定する
    //参考: https://nekojara.city/unity-line-segment-cross
    private bool IsCrossing((Vector2 start, Vector2 end) line1, (Vector2 start, Vector2 end) line2)
    {
        Vector2 v1 = line1.end - line1.start;
        Vector2 v2 = line2.end - line2.start;
        return Cross(v1, line2.start - line1.start) * Cross(v1, line2.end - line1.start) < 0 &&
               Cross(v2, line1.start - line2.start) * Cross(v2, line1.end - line2.start) < 0;
    }

    //2つのベクトルの外積を求める
    private float Cross(Vector2 v1, Vector2 v2)
    {
        return v1.x * v2.y - v1.y * v2.x;
    }
}
