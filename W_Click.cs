using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Click : MonoBehaviour
{
    [SerializeField] GameObject w_Click;

    public void ShowAndHideW_Click()
    {
        Debug.Log("Wクリック表示");
        w_Click.SetActive(true);
        StartCoroutine(HideAfterDelay());
        w_Click.transform.parent.GetComponent<Collider2D>().enabled = false; // クリックイベントを受け付けないようにする
    }

    public IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(2);
        w_Click.SetActive(false);
        w_Click.transform.parent.GetComponent<Collider2D>().enabled = true; // クリックイベントを受け付けないようにする

    }



}
