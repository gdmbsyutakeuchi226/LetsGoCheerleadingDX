using UnityEngine;

[CreateAssetMenu(fileName = "チームデータ", menuName = "Scriptable Objects/TeamDataSO")]
public class TeamDataSO : ScriptableObject {
    public string teamName;
    public Sprite teamLogo;
    public int difficultyStars;
    public string description; // ゲーム内の説明文
    // ...このチーム専用の練習効率定数など
}