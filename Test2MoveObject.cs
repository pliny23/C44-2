using UnityEngine;

public class Test2MoveObect : MonoBehaviour
{
    [SerializeField] // インスペクタでドラッグ＆ドロップによる設定を可能にするための属性
    private Transform spriteB; // スプライトBのTransformをインスペクターで設定

    private void OnMouseDown()
    {
        if (spriteB != null)
        {
            // Z軸は元のカメラの位置を保持
            float currentCameraZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(spriteB.position.x, spriteB.position.y, currentCameraZ);
        }
    }
}
