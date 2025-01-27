using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Stats", menuName = "Scriptable Objects/Character_Stats")]
public class Character_Stats : SerializedScriptableObject
{
    //추후 제작편의성을위해 기본설정 무조건!!!!!!!!!

    //등급별 스킬변화는 스킬에서 구현
    //레벨 제외 인게임고정스탯
    [Range(0, 3), MinValue(0), MaxValue(3),Tooltip("등급 미존재시 0")]
    public int Identity_Grade = 0; // 인격 등급?({Identity_Star}성)


    public int[] Struggle_Line;
    [FoldoutGroup("Speed")]
    [HorizontalGroup("Speed/Horizontal")]
    [BoxGroup("Speed/Horizontal/Min"), HideLabel]
    public int SpeedMin = 1; // 속도 최소값
    [FoldoutGroup("Speed")]
    [BoxGroup("Speed/Horizontal/Max"), HideLabel]
    public int SpeedMax = 3; //속도 최대값

    [FoldoutGroup("Hp")] //
    [HorizontalGroup("Hp/Horizontal")]
    [BoxGroup("Hp/Horizontal/Min"), HideLabel]
    public float Hp_Min = 0;
    [BoxGroup("Hp/Horizontal/Max"), HideLabel]
    public float Hp_Max = 100;

    [FoldoutGroup("Hp")]
    [BoxGroup("Hp/Horizontal/PerLv"), HideLabel]
    public float Hp_PerLevel = 1;
    [FoldoutGroup("Hp")]
    [BoxGroup("Hp/Horizontal/Percent"), HideLabel]
    public float Hp_StartPer = 100;

    [FoldoutGroup("Resistance"),Tooltip("죄악속성 저항이 1이 아닌경우만 변경혹은추가")]
    public Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // 죄악속성 저항이 1이 아닌경우만 변경혹은추가
    [FoldoutGroup("Resistance")]
    public Dictionary<AttackType, float> Resistance_AttackType = new Dictionary<AttackType, float>(3) 
    {
        {AttackType.Slash, 1},
        {AttackType.Pierce, 1},
        {AttackType.Blunt, 1}
    };

    [FoldoutGroup("Identity_Change")]
    public Dictionary<int, int> Speed_Min = new Dictionary<int, int>();
    [FoldoutGroup("Identity_Change")]
    public Dictionary<int, int> Speed_Max = new Dictionary<int, int>();

}
