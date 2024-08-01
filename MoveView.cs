using UnityEngine;

public class MoveView : MonoBehaviour
{
    private Vector3 dragOrigin; // ドラッグを開始したときのマウスの初期位置
    private bool isDragging = false;  // 現在マウスをドラッグしているかどうか
    public float dragSpeed = 20.0f;
    private bool isTouchingMap = false; // MAPタグを持つオブジェクトに接触しているかどうか

    void Start()
    {
        // 初期化コードがあればここに追加
        UnityEngine.Debug.Log("Start method called");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリックした時
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
            UnityEngine.Debug.Log("Mouse button down at position: " + dragOrigin);
        }

        if (Input.GetMouseButtonUp(0)) // 左クリックを離した時
        {
            isDragging = false;
            UnityEngine.Debug.Log("Mouse button up");
        }

        if (isDragging && isTouchingMap) // ドラッグ中かつMAPに接触中
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(difference.x * dragSpeed, 0, 0);

            transform.position += move;
            UnityEngine.Debug.Log("Camera moved to: " + transform.position);

            dragOrigin = Input.mousePosition;
        }
    }

    // MAPタグを持つオブジェクトに接触したときの処理
    void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("OnTriggerEnter called with object: " + other.name);
        if (other.CompareTag("MAP"))
        {
            isTouchingMap = true;
            UnityEngine.Debug.Log("Entered MAP area");
        }
    }

    // MAPタグを持つオブジェクトから離れたときの処理
    void OnTriggerExit(Collider other)
    {
        UnityEngine.Debug.Log("OnTriggerExit called with object: " + other.name);
        if (other.CompareTag("MAP"))
        {
            isTouchingMap = false;
            UnityEngine.Debug.Log("Exited MAP area");
        }
    }
}
