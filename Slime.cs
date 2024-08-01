using UnityEngine;
using UnityEngine.EventSystems;

public class Slime : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    private Animator animator;
    private float dragDistance;//なでなで量
    [SerializeField]
    public Gauge gauge;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)//クリック
    {
        Happy();
    }

    public void OnPointerMove(PointerEventData eventData)//ドラッグ
    {
        dragDistance += eventData.delta.magnitude;
        if (dragDistance >= 1300)
        {
            Happy();
        }
    }

    public void Happy()//反応------------------------------------------
    {
        gauge.TakeDamage(20f / 100);//HPバーに反映  //好感度を上げる

        SetBoolParameter("Happy", true);
        dragDistance = 0;
    }
    public void Angry()
    {
        SetBoolParameter("Angry", true);
    }
    public void VeryHappy()
    {
    }

    //アニメーションのパラメータを戻す-----------------------------------
    private void SetBoolParameter(string parameterName, bool value)
    {
        animator.SetBool(parameterName, value);
        StartCoroutine(ResetBoolAfterDelay(parameterName, 0.8f));
    }
    //アニメーションパラメータを戻す為に遅らせる
    private System.Collections.IEnumerator ResetBoolAfterDelay(string parameterName, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(parameterName, false);
    }
}