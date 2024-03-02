using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class TestClick : MonoBehaviour
{

    [SerializeField] Image win;
    //bool winBool = true;


    public void Test3()
    {
        win.enabled = true;
        Debug.Log("クリック！");
    }
}
