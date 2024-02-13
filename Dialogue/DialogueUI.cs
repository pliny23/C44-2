using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Image AIImage;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;//選択肢ボタンを入れる矩形
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());
            UpdateUI();
        }

        void UpdateUI()//playerConversantが会話の更新を通知すると、UpdateUIメソッドが呼び出される
        {
            gameObject.SetActive(playerConversant.IsActive());//DialogueUIを表示
            if (!playerConversant.IsActive())
            {
                return;//playerConversantがアクティブでない場合は処理をスキップして終了
            }
            AIResponse.SetActive(!playerConversant.IsChoosing());//AI が発言中である場合は、AIResponse をアクティブに
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());//つまりプレイヤーが選択中である場合は、choiceRoot をアクティブに
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();//選択肢のリスト構築
            }
            else//選択中でない場合は
            {
                AIText.text = playerConversant.GetText();
                AIImage.sprite = playerConversant.GetImage(); //画像の更新
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()//選択肢のリスト構築
        {
            choiceRoot.DetachChildren();
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => { playerConversant.SelectChoice(choice); });
            }
        }

    }
}