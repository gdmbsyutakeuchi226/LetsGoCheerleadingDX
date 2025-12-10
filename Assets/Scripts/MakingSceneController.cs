/* ============================================================
 * Script Name : MakingSceneController.cs
 * シーン全体の処理と画面遷移を管理するメインスクリプト
 * Update : 2025/12/10
 * Ver 0.00a : 作成
 * ============================================================ */
using UnityEngine;
using System.Collections.Generic; // Listを使うために必要

public class MakingSceneController : MonoBehaviour {
    [Header("フェーズパネル")]
    public GameObject teamSelectPanel; // フェーズ1 (チーム選択)
    public GameObject charInfoPanel;   // フェーズ2 (名前・身長など)

    [Header("チーム選択関連")]
    public List<TeamButtonUI> teamButtons;

    // ★ 現在選択されているボタンを追跡する変数
    private TeamButtonUI currentSelectedButton = null;

    // 現在のプレイヤーデータ (全フェーズを通じて値を保持)
    private PlayerDataSO playerCurrentData;

    void Start(){
        // プレイヤーデータSOをロード (まだ初期化していない状態)
        // プレイヤーデータを初期化するロジックをここに書くか、
        // 予め空のSOアセットをInspectorで参照させます。

        // チーム選択画面を表示し、他の画面を非表示にする
        teamSelectPanel.SetActive(true);
        charInfoPanel.SetActive(false);

        // 各チームボタンの初期設定
        foreach (var button in teamButtons){
            button.Setup(this);
        }
    }

    // ★ TeamButtonUIから1回目のクリックで呼び出される
    public void SetTeamSelection(TeamButtonUI selectedButton){
        // 既に何か選択されている場合は、そのカーソルを解除
        if (currentSelectedButton != null && currentSelectedButton != selectedButton){
            currentSelectedButton.SetSelected(false);
        }

        // 新しいボタンを選択状態にする
        selectedButton.SetSelected(true);
        currentSelectedButton = selectedButton;

        // ★ チームの説明パネルがボタンの近くに表示されるように制御することも可能
    }
    // TeamButtonUIから2回目のクリック（決定）で呼び出される
    public void SelectTeam(TeamDataSO selectedTeam){
        Debug.Log("チームが最終決定されました: " + selectedTeam.teamName);

        // 1. プレイヤーデータに選択したチーム情報を書き込む（TODO）
        // 2. フェーズ1を非表示にし、フェーズ2に遷移
        teamSelectPanel.SetActive(false);
        charInfoPanel.SetActive(true);

        // ...
    }


}