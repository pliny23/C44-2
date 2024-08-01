using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//必要な名前空間を追加
using UnityEngine.Events;

public class EnemyAnim : MonoBehaviour
{
    //HPテキスト
    [SerializeField] Text playerHP_txt;
    [SerializeField] int playerHP = 100;
    [SerializeField] Text enemyHP_txt;
    [SerializeField] int enemyHP = 100;
    Animator animator;
    [SerializeField]
    //[SerializeField] private Healthbar m_healthbar;//HPバー
    public Healthbar healthbar;

    void Start()
    {
        // animatorコンポーネントを取得
        animator = GetComponent<Animator>();
        EnemyAttackStart();


    }

    public void EnemyAttackStart()//ダメージアニメーションの再生が終わったら
    {
        //1～３秒までのランダムな時間でOneRep関数を実行する
        float randomTime = Random.Range(1f, 3f);
        Invoke("OneRep", randomTime);//指定した時間後に指定したメソッドを呼び出す
    }


    public void OneRep()//アニメーションの再生が終わったら
    {
        animator.SetTrigger("EnemyAttack");
    }
    public void OnAnimationEnd()//敵の攻撃アニメーションの再生が終わったら
    {
        playerHP--;//HPを減らす
        playerHP_txt.text = playerHP + "/100";//テキストに反映
        EnemyAttackStart();
    }

    public void PlayerAttack()//プレイヤーの攻撃（ボタン登録）
    {
        enemyHP -= 20;//HPを減らす
        enemyHP_txt.text = enemyHP + "/100";//テキストに反映
        healthbar.TakeDamage(20f / 100);//HPバーに反映
        animator.SetTrigger("EnemyDamage");//敵のダメージアニメ

    }



}

