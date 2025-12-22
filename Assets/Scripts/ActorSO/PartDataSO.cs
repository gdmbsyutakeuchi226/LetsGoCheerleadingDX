using UnityEngine;

[CreateAssetMenu(fileName = "PartDataSO", menuName = "チアDX/キャラメイク/パーツデータ")]
public class PartDataSO : ScriptableObject{
    public int partID;
    public string partName;
    public Sprite partSprite; // そのパーツ単体の画像
    [Header("UI表示")]
    public Sprite thumbnail; // UI上のボタンに表示するサムネイル画像

    // カテゴリー分け（FrontHair, BackHair, Eyes など）
    public string category;

    [Header("反映データ")]
    // 髪型の場合: 実際に切り替える3Dメッシュ
    public GameObject prefab;
    // 髪色や目の色の場合: 使用するテクスチャまたはマテリアル
    public Material material;
}
