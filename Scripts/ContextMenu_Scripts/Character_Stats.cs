using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Stats", menuName = "Scriptable Objects/Character_Stats")]
public class Character_Stats : SerializedScriptableObject
{
    //레벨 제외 인게임고정스탯
    //[Range(1, 3), MinValue(1), MaxValue(3)]
    //public int Identity_Star; // 인격 등급?({Identity_Star}성)

    public int[] Struggle_Line;
    [FoldoutGroup("Speed")]
    [HorizontalGroup("Speed/Horizontal")]
    [BoxGroup("Speed/Horizontal/Min"), HideLabel]
    public int SpeedMin ; // 속도 최소값
    [FoldoutGroup("Speed")]
    [BoxGroup("Speed/Horizontal/Max"), HideLabel]
    public int SpeedMax ; //속도 최대값

    [FoldoutGroup("Hp")] //
    [HorizontalGroup("Hp/Horizontal")]
    [BoxGroup("Hp/Horizontal/Min"), HideLabel]
    public float Hp_Min ;
    [BoxGroup("Hp/Horizontal/Max"),HideLabel]
    public float Hp_Max ;
    
    [FoldoutGroup("Hp")]
    [BoxGroup("Hp/Horizontal/PerLv"), HideLabel]
    public float Hp_PerLevel;
    [FoldoutGroup("Hp")]
    [BoxGroup("Hp/Horizontal/Percent"), HideLabel]
    public float Hp_StartPer = 100;

    [FoldoutGroup("Resistance")]
    public Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // 죄악속성 저항이 1이 아닌경우만 변경혹은추가
    [FoldoutGroup("Resistance")]
    public Dictionary<AttackType, float> Resistance_AttackType = new Dictionary<AttackType, float>();
}
