using UnityEngine;

namespace RPG.Control
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController playerController;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            // プレイヤーの入力を処理する
            HandleInput();
        }

        private void HandleInput()
        {
            // ここでプレイヤーの入力を処理する
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("起動");
                // レイキャストを実行して対象を検出し、必要に応じて PlayerController を呼び出す
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    IRaycastable raycastable = hit.collider.GetComponent<IRaycastable>();
                    if (raycastable != null)
                    {
                        raycastable.HandleRaycast(playerController);
                    }
                }
            }
        }
    }
}
