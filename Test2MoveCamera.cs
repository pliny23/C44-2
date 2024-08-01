using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test2MoveCamera : MonoBehaviour
{
    private Camera cam; // メインカメラを使用

    [SerializeField]
    private SpriteRenderer mapRenderer; // インスペクタでマップを設定

    private float mapMinX, mapMaxX;
    private Vector3 dragOrigin;

    private void Awake()
    {
        cam = Camera.main; // メインカメラを自動的に取得
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
    }

    private void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = cam.transform.position + new Vector3(difference.x, 0, 0); // Y軸の移動を無視
            cam.transform.position = ClampCamera(newPosition);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);

        return new Vector3(newX, cam.transform.position.y, targetPosition.z); // Y座標は変更しない
    }
}