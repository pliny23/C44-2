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
        [SerializeField] GameObject AIImageL;
        [SerializeField] GameObject AIImageR;
        [SerializeField] GameObject AIFullImage;
        [SerializeField] Image AIFull;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;//選択肢ボタンを入れる矩形
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;
        [SerializeField] GameObject WindowL;//ウィンドウ
        [SerializeField] GameObject WindowR;
        [SerializeField] GameObject WindowN;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerConversant>();
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
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());//選択中ならchoiceRootをアクティブに
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();//選択肢のリスト構築
            }
            else//選択中でない場合は
            {
                AIText.text = playerConversant.GetText();
                AIFullImage.SetActive(playerConversant.IsFull());//一枚絵ノードがtrueならUIをアクティブに

                WindowL.gameObject.SetActive(playerConversant.GetWinL());//ウィンドウ表示
                WindowR.gameObject.SetActive(playerConversant.GetWinR());//ウィンドウ表示
                WindowN.gameObject.SetActive(!playerConversant.GetWinL() && !playerConversant.GetWinR());//ウィンドウ表示

                if (playerConversant.GetImageL() != null)//顔画像左
                {
                    //AIImageL.SetActive(!AIImageL.activeSelf);//非表示の場合は表示する
                    AIImageL.GetComponent<Image>().sprite = playerConversant.GetImageL(); //画像を更新する
                }
                if (playerConversant.GetImageR() != null)//顔画像右
                {
                    //AIImageR.SetActive(!AIImageR.activeSelf);
                    AIImageR.GetComponent<Image>().sprite = playerConversant.GetImageR();
                }
                if (playerConversant.GetFullImage() != null)//新規画像が入っているなら更新
                {
                    AIFull.sprite = playerConversant.GetFullImage();
                }
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()//選択肢のリスト構築
        {
            // choiceRootの全ての子要素を削除
            foreach (Transform child in choiceRoot)
            {
                Destroy(child.gameObject);
            }

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