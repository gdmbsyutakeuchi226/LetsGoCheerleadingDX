using UnityEngine;

[CreateAssetMenu(fileName = "チームデータ", menuName = "Scriptable Objects/TeamDataSO")]
public class TeamDataSO : ScriptableObject {
    [Header("表示情報")]
    public string teamName;          // 例: レッドスターチーム
    [TextArea(3, 5)]
    public string description;       // 例: 全国大会を目指すストイックなチーム
    public int difficultyStars;      // 難易度 (1～5)
    public Sprite teamImage;         // チームのイメージ画像（UI表示用）

    [Header("ゲーム内データ")]
    public float initialStatBonus;   // 初期パラメータへの補正値
    public string ageGroupName = "高校生編"; // 所属する世代（将来の拡張用）
    // ...このチーム専用の練習効率定数など
}