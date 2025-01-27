using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Stats", menuName = "Scriptable Objects/Character_Stats")]
public class Character_Stats : SerializedScriptableObject
{
    //���� �������Ǽ������� �⺻���� ������!!!!!!!!!

    //��޺� ��ų��ȭ�� ��ų���� ����
    //���� ���� �ΰ��Ӱ�������
    [Range(0, 3), MinValue(0), MaxValue(3),Tooltip("��� ������� 0")]
    public int Identity_Grade = 0; // �ΰ� ���?({Identity_Star}��)


    public int[] Struggle_Line;
    [FoldoutGroup("Speed")]
    [HorizontalGroup("Speed/Horizontal")]
    [BoxGroup("Speed/Horizontal/Min"), HideLabel]
    public int SpeedMin = 1; // �ӵ� �ּҰ�
    [FoldoutGroup("Speed")]
    [BoxGroup("Speed/Horizontal/Max"), HideLabel]
    public int SpeedMax = 3; //�ӵ� �ִ밪

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

    [FoldoutGroup("Resistance"),Tooltip("�˾ǼӼ� ������ 1�� �ƴѰ�츸 ����Ȥ���߰�")]
    public Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // �˾ǼӼ� ������ 1�� �ƴѰ�츸 ����Ȥ���߰�
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
