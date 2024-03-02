using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要


// 戦いを管理する
// PlayerとEnemyを戦わせる
public class BattleManager : MonoBehaviour
{
    // Playerを取得する
    public UnitManager player;
    // Enemyを取得する
    public UnitManager enemy;
    void Start()
    {
    }

    // PlayerがEnemyに攻撃する
    public void OnAttackButton()
    {
        player.Attack(enemy);
        if (enemy.hp > 0)
        {
            EnemyTurn();
        }
        else
        {
            BattleEnd();
        }
    }

    // EnemyがPlayerに攻撃する
    void EnemyTurn()
    {
        enemy.Attack(player);
        if (player.hp == 0)
        {
            BattleEnd();
        }
    }

    void BattleEnd()
    {
        Debug.Log("対戦終了");
        string currentScene = SceneManager.GetActiveScene().name; // 現在のシーン名を取得
        SceneManager.LoadScene(currentScene); // 現在のシーンを再読み込み
    }
}

