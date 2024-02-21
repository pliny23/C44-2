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
        [SerializeField] Image AIImageL;
        [SerializeField] Image AIImageR;
        [SerializeField] GameObject AIFullImage;
        [SerializeField] Image AIFull;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;//選択肢ボタンを入れる矩形
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;
        [SerializeField] GameObject Mwindow;
        [SerializeField] Image WindowL;
        [SerializeField] Image WindowR;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;//ActionデリゲートにUpdateUIを登録
            nextButton.onClick.AddListener(() => playerConversant.Next());//nextButtonクリックでplayerConversant.Next()を呼び出す
            quitButton.onClick.AddListener(() => playerConversant.Quit());
            UpdateUI();
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());//DialogueUIを表示
            if (!playerConversant.IsActive())
            {
                return;//処理をスキップして終了
            }
            AIResponse.SetActive(!playerConversant.IsChoosing());//AIが発言中ならAIResponseをアクティブに
            Mwindow.SetActive(!playerConversant.IsChoosing());//吹き出し表示
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());//選択中ならchoiceRootをアクティブに
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();//選択肢のリスト構築
            }
            else//選択中でない場合は
            {
                AIText.text = playerConversant.GetText();
                AIFullImage.SetActive(playerConversant.IsFull());//一枚絵ノードがtrueならUIをアクティブに

                if (playerConversant.GetImageL() != null)//顔画像左
                {
                    AIImageL.gameObject.SetActive(!AIImageL.gameObject.activeSelf);//表示されてないなら表示する
                    AIImageL.sprite = playerConversant.GetImageL(); //画像の更新
                }
                if (playerConversant.GetImageR() != null)//顔画像右
                {
                    AIImageR.gameObject.SetActive(!AIImageR.gameObject.activeSelf);
                    AIImageR.sprite = playerConversant.GetImageR();
                }
                if (playerConversant.GetFullImage() != null)
                {
                    AIFull.sprite = playerConversant.GetFullImage();
                }
                nextButton.gameObject.SetActive(playerConversant.HasNext());

                //吹き出し右なら右
                //左なら左を表示
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