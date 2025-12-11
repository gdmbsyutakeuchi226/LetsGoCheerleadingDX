using UnityEngine;

[CreateAssetMenu(fileName = "性格データ", menuName = "Scriptable Objects/CharacterPersonalitySO")]
public class CharacterPersonalitySO : ScriptableObject {
    public int personalityID;
    public string personalityName; // 例：完璧主義者, 世話焼き
    [TextArea(2, 4)]
    public string description;
    public float initialMoodBonus;
    public float technicalStatGain; // 成長補正値
    // ... その他、性格に関連するパラメータ
}