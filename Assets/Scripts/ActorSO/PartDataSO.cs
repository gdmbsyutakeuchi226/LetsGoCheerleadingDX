using UnityEngine;

[CreateAssetMenu(fileName = "PartDataSO", menuName = "チアDX/キャラメイク/パーツデータ")]
public class PartDataSO : ScriptableObject{
    public int partID;
    public string partName;
    public string category; // 例: "HairStyle", "HairColor", "EyeShape"

    [Header("反映データ")]
    // 髪型の場合: 実際に切り替える3Dメッシュ
    public GameObject prefab;
    // 髪色や目の色の場合: 使用するテクスチャまたはマテリアル
    public Material material;

    [Header("UI表示")]
    public Sprite thumbnail; // UI上のボタンに表示するサムネイル画像
}
