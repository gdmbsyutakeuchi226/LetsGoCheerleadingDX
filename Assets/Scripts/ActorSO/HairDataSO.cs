using UnityEngine;

[CreateAssetMenu(fileName = "HairDataSO", menuName = "チアDX/キャラメイク/髪型データ")]
public class HairDataSO : ScriptableObject {
    public int hairID;
    public string hairName;

    [Header("反映用スプライト")]
    public Sprite frontHairSprite; // 前髪
    public Sprite backHairSprite;  // 後ろ髪（ショートなら空でもOK）

    [Header("UI表示用")]
    public Sprite thumbnail;       // 選択ボタンに表示するアイコン
}