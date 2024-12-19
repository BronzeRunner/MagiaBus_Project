using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Stats", menuName = "Scriptable Objects/Character_Stats")]
public class Character_Stats : SerializedScriptableObject
{
    //���� ���� �ΰ��Ӱ�������
    //[Range(1, 3), MinValue(1), MaxValue(3)]
    //public int Identity_Star; // �ΰ� ���?({Identity_Star}��)

    public int[] Struggle_Line;
    [FoldoutGroup("Speed")]
    [HorizontalGroup("Speed/Horizontal")]
    [BoxGroup("Speed/Horizontal/Min"), HideLabel]
    public int SpeedMin ; // �ӵ� �ּҰ�
    [FoldoutGroup("Speed")]
    [BoxGroup("Speed/Horizontal/Max"), HideLabel]
    public int SpeedMax ; //�ӵ� �ִ밪

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
    public Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // �˾ǼӼ� ������ 1�� �ƴѰ�츸 ����Ȥ���߰�
    [FoldoutGroup("Resistance")]
    public Dictionary<AttackType, float> Resistance_AttackType = new Dictionary<AttackType, float>();
}
