/* ============================================================
 * Script Name : MakingSceneController.cs
 * シーン全体の処理と画面遷移を管理するメインスクリプト
 * Update : 2025/12/10
 * Ver 0.00a : 作成
 * ============================================================ */
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MakingSceneController : MonoBehaviour {
    [Header("フェーズパネル")]
    public GameObject teamSelectPanel;
    public GameObject charInfoPanel;

    [Header("チーム選択関連")]
    public List<TeamButtonUI> teamButtons;
    private TeamButtonUI currentSelectedButton = null;
    private TeamDataSO selectedTeamData;

    [Header("チーム詳細表示パネル")]
    public TextMeshProUGUI centralDetailName;
    public TextMeshProUGUI centralDetailDescription;
    public TextMeshProUGUI centralDetailDifficulty;

    void Start()
    {
        // 初期パネル設定
        teamSelectPanel.SetActive(true);
        charInfoPanel.SetActive(false);

        // 各チームボタンの初期設定
        foreach (var button in teamButtons){
            button.Setup(this);
        }

        // 中央パネルの初期表示（最初のデータがないため、ガイド表示）
        centralDetailName.text = "チームを選んでください";
        centralDetailDescription.text = "画面左右のチームをタップして詳細を確認し、再度タップすると決定します。";
        centralDetailDifficulty.text = "難易度：ー";
    }

    // 1回目クリック時：カーソル表示と中央詳細更新
    public void SetTeamSelection(TeamButtonUI selectedButton)
    {
        // 既に選択されていたボタンのカーソルを解除
        if (currentSelectedButton != null && currentSelectedButton != selectedButton){
            currentSelectedButton.SetSelected(false);
        }

        // 新しいボタンを選択状態にする
        selectedButton.SetSelected(true);
        currentSelectedButton = selectedButton;

        // 中央の詳細表示パネルを更新
        TeamDataSO teamData = selectedButton.teamData;

        if (centralDetailName != null){
            centralDetailName.text = "【" + teamData.teamName + "】";
        }
        if (centralDetailDescription != null){
            centralDetailDescription.text = teamData.description;
        }
        if (centralDetailDifficulty != null){
            string stars = "";
            for (int i = 0; i < 5; i++){
                stars += (i < teamData.difficultyStars) ? "★" : "☆";
            }
            centralDetailDifficulty.text = "難易度：" + stars;
        }
    }

    // 2回目クリック時：チーム決定とフェーズ遷移
    public void SelectTeam(TeamDataSO selectedTeam){
        Debug.Log("チームが最終決定されました: " + selectedTeam.teamName + " -> フェーズ2へ");

        // 1. 選択されたデータを保持
        selectedTeamData = selectedTeam;

        // 2. フェーズ1を非表示にし、フェーズ2に遷移
        teamSelectPanel.SetActive(false);
        charInfoPanel.SetActive(true);
    }
}