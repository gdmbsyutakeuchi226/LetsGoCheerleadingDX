/* ============================================================
 * Script Name : TeamButtonUI.cs
 * チームボタンのスクリプト
 * Update : 2025/12/10
 * Ver 0.00a : 作成
 * ============================================================ */
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

// IPointerClickHandlerを実装し、2段階クリックを処理
public class TeamButtonUI : MonoBehaviour, IPointerClickHandler {
    public TeamDataSO teamData;

    [Header("UI参照")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI difficultyText;
    public Image teamImage;
    public GameObject cursorIndicator; // カーソル表示用

    private MakingSceneController controller;
    private bool isSelected = false;

    // シーンロード時にUIを更新（エディタでの視認性向上）
    void Awake()
    {
        UpdateUIFromData();
    }

    // MakingSceneControllerから呼ばれる初期化処理
    public void Setup(MakingSceneController makingController)
    {
        controller = makingController;
        SetSelected(false);
    }

    // SOの内容をTextMeshProに書き込むロジック
    private void UpdateUIFromData()
    {
        if (teamData == null) return;

        nameText.text = teamData.teamName;
        descriptionText.text = teamData.description;

        // 難易度を星マークに変換
        string stars = "";
        for (int i = 0; i < 5; i++)
        {
            stars += (i < teamData.difficultyStars) ? "★" : "☆";
        }
        difficultyText.text = stars;

        teamImage.sprite = teamData.teamImage;
    }

    // カーソル表示・非表示を切り替えるメソッド
    public void SetSelected(bool selected)
    {
        Debug.Log($"[{teamData.teamName}] 選択状態を {selected} に設定。");
        isSelected = selected;

        if (cursorIndicator != null)
        {
            cursorIndicator.SetActive(selected);
        }
    }

    // クリックイベントの処理
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected)
        {
            // 2回目クリック: 決定処理を実行
            controller.SelectTeam(teamData);
        }
        else
        {
            // 1回目クリック: 選択状態に移行 (カーソル表示と中央詳細更新)
            controller.SetTeamSelection(this);
        }
    }
}