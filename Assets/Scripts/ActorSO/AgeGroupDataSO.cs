using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "世代データ", menuName = "Scriptable Objects/AgeGroupDataSO")]
public class AgeGroupDataSO : ScriptableObject {
    public string ageGroupName; // 高校生、小学生
    public int minHeight; // 高校生なら130
    public int maxHeight; // 高校生なら179
    public List<TeamDataSO> selectableTeams; // 属するチームリスト（レッドスター、ブルーなど）
}