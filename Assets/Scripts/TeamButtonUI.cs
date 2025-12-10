/* ============================================================
 * Script Name : TeamButtonUI.cs
 * チームボタンのスクリプト
 * Update : 2025/12/10
 * Ver 0.00a : 作成
 * ============================================================ */
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使うために必要
using UnityEngine.EventSystems; // クリックイベントのために追加

public class TeamButtonUI : MonoBehaviour, IPointerClickHandler {
    // InspectorでSOをアサイン
    public TeamDataSO teamData;

    [Header("UI参照")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI difficultyText;
    public Image teamImage;
    // カーソル表示用のImageまたはPanel
    public GameObject cursorIndicator;
    public Button selectButton; // 決定用のボタンは非表示にして、決定処理をIPointerClickHandlerで代替しても良い

    private MakingSceneController controller; // メインコントローラーへの参照
    private bool isSelected = false; // ★このフラグで選択状態を管理

    public void Setup(MakingSceneController makingController){
        controller = makingController;

        // データをUIに反映（省略）
        // ...

        // 初期状態ではカーソルを非表示
        SetSelected(false);

        // Note: ButtonコンポーネントのAddListenerは不要になるか、
        // IPointerClickHandlerと併用する場合は注意が必要です。
        // 今回はIPointerClickHandler（UIのどこをタップしても反応）で統一します。

        // selectButton.onClick.AddListener(OnTeamSelected);
    }

    // カーソル表示・非表示を切り替える外部公開メソッド
    public void SetSelected(bool selected){
        isSelected = selected;
        // UIのカーソルインジケーターの表示・非表示を切り替える
        if (cursorIndicator != null){
            cursorIndicator.SetActive(selected);
        }

        // ★カーソルが当たった時にパネルの縁の色を変えるなど、視覚的なフィードバックも可能
    }

    // ★ IPointerClickHandler インターフェースを実装してクリックを処理
    public void OnPointerClick(PointerEventData eventData){
        if (isSelected){
            // 2回目クリック: 既に選択状態だった場合、決定処理を実行
            controller.SelectTeam(teamData);
        }else{
            // 1回目クリック: 選択状態に移行
            controller.SetTeamSelection(this);
        }
    }
}