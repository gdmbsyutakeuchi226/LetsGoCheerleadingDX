/* ============================================================
 * Script Name : MakingSceneController.cs
 * キャラクターメイキングのシーン全体の処理と画面遷移を管理するメインスクリプト
 * Update : 2025/12/10
 * Ver 0.00a : 作成
 * ============================================================ */
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

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

    [Header("フェーズ2 UI 参照")]
    public TMP_InputField lastNameInput;
    public TMP_InputField firstNameInput;
    public TMP_Dropdown birthMonthDropdown;
    public TMP_Dropdown birthDayDropdown;
    public TMP_InputField heightInput;
    public TMP_Dropdown personalityDropdown;
    public Button confirmCharInfoButton; // 決定ボタン

    // ★★★ 性格データ（CharacterPersonalitySOアセット）のリストを追加 ★★★
    [Header("フェーズ2 マスターデータ")]
    public List<CharacterPersonalitySO> personalityDataList;

    void Start(){
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
    public void SetTeamSelection(TeamButtonUI selectedButton){
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

        selectedTeamData = selectedTeam;
        teamSelectPanel.SetActive(false);
        charInfoPanel.SetActive(true);

        // ★ フェーズ2の初期化処理を呼び出す ★
        InitializeCharInfoPanel();
    }

    public int GetMaxDayOfMonth(int month, int year = 2025) {
        if (month == 4 || month == 6 || month == 9 || month == 11)
            return 30;

        if (month == 2){
            // 閏年判定 (高校生編の3年間では閏年を無視しても良いが、今回は29日まで許可)
            // 厳密な閏年計算: if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
            return 29; // 29日まで許可（ゲームのルールとして）
        }
        return 31;
    }

    private bool ValidateHeight(int height){
        // TeamDataSO (または AgeGroupDataSO) から最小/最大値を取得
        const int MIN_HEIGHT = 130;
        const int MAX_HEIGHT = 179;

        if (height < MIN_HEIGHT || height > MAX_HEIGHT){
            // 警告UIを表示（例: "身長は130cmから179cmの範囲で入力してください"）
            return false;
        }
        return true;
    }
    // フェーズ2のUIを初期化し、イベントを登録する
    private void InitializeCharInfoPanel(){
        // 1. Dropdownの初期化
        SetupBirthdayDropdowns();
        SetupPersonalityDropdown();

        // 2. 身長入力時の検証イベントを登録
        // TMP_InputFieldは文字列なので、値が確定した時（EndEdit）に検証するのが望ましい
        heightInput.onEndEdit.AddListener((string heightStr) => {
            if (int.TryParse(heightStr, out int height) && ValidateHeight(height)){
                // 検証OK
                CheckAllInputs();
            }else{
                // 検証NG: 身長エラーメッセージを表示
                confirmCharInfoButton.interactable = false;
            }
        });

        // 3. 決定ボタンに最終処理を登録
        confirmCharInfoButton.onClick.AddListener(OnCharInfoConfirmed);
        confirmCharInfoButton.interactable = false; // 初期は無効化
    }
    // 誕生日の月と日を連動させるための初期化
    private void SetupBirthdayDropdowns(){
        // 月のDropdownに1～12を設定（今回は手動設定を想定）
        // 月の変更時に日の選択肢を更新するリスナーを登録
        birthMonthDropdown.onValueChanged.AddListener(UpdateDayDropdownOptions);

        // 初回実行
        UpdateDayDropdownOptions(birthMonthDropdown.value);
    }
    // 性格ドロップダウンを初期化するメソッド
    private void SetupPersonalityDropdown(){
        // 既存の選択肢をクリア
        personalityDropdown.ClearOptions();

        List<string> options = new List<string>();

        // マスターデータリストから名前を取得してオプションに追加
        if (personalityDataList != null){
            foreach (var personality in personalityDataList){
                // ドロップダウンには「ID：性格名」を表示
                options.Add($"Type{personality.personalityID}: {personality.personalityName}");
            }
        }

        // オプションをドロップダウンに設定
        personalityDropdown.AddOptions(options);

        // 選択値が変更されたときに検証を行うリスナーを登録
        personalityDropdown.onValueChanged.AddListener((int index) => {
            // 性格が選択されたら、決定ボタンの有効性をチェック
            CheckAllInputs();
        });
    }

    // 月の変更に基づいて、日のDropdownの選択肢を更新する
    private void UpdateDayDropdownOptions(int monthIndex){
        // Dropdownのインデックスは0から始まるため、月は monthIndex + 1
        int selectedMonth = monthIndex + 1;
        int maxDays = GetMaxDayOfMonth(selectedMonth);

        birthDayDropdown.ClearOptions();
        List<string> days = new List<string>();
        for (int i = 1; i <= maxDays; i++){
            days.Add(i.ToString());
        }
        birthDayDropdown.AddOptions(days);

        CheckAllInputs();
    }

    // 全ての入力が完了し、検証を通過したかチェック
    private void CheckAllInputs(){
        // ここで名字、名前、身長、誕生日がすべて有効な値かチェックする
        bool isValid = !string.IsNullOrWhiteSpace(firstNameInput.text) &&
                       !string.IsNullOrWhiteSpace(lastNameInput.text) &&
                       ValidateHeight(int.Parse(heightInput.text)); // 他の検証も追加

        confirmCharInfoButton.interactable = isValid;
    }

    // 決定ボタンが押された時の最終処理
    private void OnCharInfoConfirmed(){
        // 最終データをPlayerDataSOに書き込み、フェーズ3(見た目)に遷移
        // ...
    }
}