using UnityEngine;

[CreateAssetMenu(fileName = "プレイヤーデータ", menuName = "Scriptable Objects/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject {
    [Header("基本情報")]
    public string lastName; // 試合での実況やフルネーム表示に使用。
    public string firstName; // ここが重要。チームメイトからのセリフ等で、この変数に「ちゃん」を結合して表示する。

    // UI表示例:
    // $"{firstName}ちゃん、練習行こう！" -> "あかりちゃん、練習行こう！"

    public int birthdayMonth;
    public int birthdayDay; // intで保持し、Turn計算は別ロジックで。

    public int height;

    [Header("性格・特性")]
    // PersonalityIDを基に、マスターデータからCharacterPersonalitySOを引く
    public int personalityID;

    [Header("見た目 (ちびキャラ設定)")]
    public int hairStyleID;
    public int hairColorID;

    public int eyesStyleID; // ツリ目、たれ目、ジト目など管理
    public int eyesColorID; // 目の色
}