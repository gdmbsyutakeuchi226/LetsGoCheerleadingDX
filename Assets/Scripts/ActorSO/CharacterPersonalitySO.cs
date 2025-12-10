using UnityEngine;

[CreateAssetMenu(fileName = "性格データ", menuName = "Scriptable Objects/CharacterPersonalitySO")]
public class CharacterPersonalitySO : ScriptableObject {
    public string personalityName; // 例：完璧主義者
    public float statGainMultiplier_Technical; // 技術力アップ倍率
    public float moodDecreaseRate; // やる気減少率
    // ...その他性格による行動特徴
}