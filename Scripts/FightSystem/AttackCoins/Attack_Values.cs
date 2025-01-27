using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Attack_Values", menuName = "ScriptableObjects/Attack_ScriptableObject", order = 1),System.Serializable]
public class Attack_Values : ScriptableObject
{
    [FoldoutGroup("Skill_ori/Type")]
    public AttackType Skill_AttackType;
    [FoldoutGroup("Skill_ori/Type")]
    public SinType Skill_SinType;
    [FoldoutGroup("Skill_ori")]
    public int Skill_Count;
    [FoldoutGroup("Skill_ori"),Tooltip("���ݽ�ų�ϰ�� ���ݷ���\n����ų�ϰ�� ����\n(���� ������)")]
    public int Skill_Level;
    [FoldoutGroup("Skill_ori"),Tooltip("��ų �⺻����(����,�淾�̰��)")]
    public int Skill_Value;

    [FoldoutGroup("Coin_ori"), Tooltip("���ΰ���")]
    public float coin_Count;
    [FoldoutGroup("Coin_ori"), Tooltip("���� �⺻��")]
    public int coin_Value; // ���� �⺻��
    [FoldoutGroup("Coin_ori"), Tooltip("����")]
    public CoinType[] coin_CoinTypes;

    [FoldoutGroup("Skill_ori")]
    public int Attack_Weight;
    //[FoldoutGroup("Coin_ori"), Tooltip("�ּ��������� \n(�޸� Ȥ�� ������ �����ϳ� �����佺������� �����Ұ��(�ĺ���)���� ����)")]
    //public int coin_MinimamValue; // �ּ���������

}
