using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeAndEventManagerSO", menuName = "Scriptable Objects/TimeAndEventManagerSO")]
public class TimeAndEventManagerSO : ScriptableObject {
    [System.Serializable]
    public class WeeklyEvent {
        public int year; // 1～3
        public int month; // 1～12
        public int week; // 1～4
        //public EventDataSO eventToTrigger; // 発生するイベント(ScriptableObject)
    }

    public List<WeeklyEvent> fixedEvents; // 固定スケジュールイベントリスト
}