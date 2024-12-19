using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Attack_Values", menuName = "ScriptableObjects/Attack_ScriptableObject", order = 1),System.Serializable]
public class Attack_Values : ScriptableObject
{
    [FoldoutGroup("Skill_ori"),Tooltip("���ݽ�ų�ϰ�� ���ݷ��� ����ų�ϰ�� ����")]
    public int Skill_Level;
    [FoldoutGroup("Skill_ori"),Tooltip("��ų �⺻����(����,�淾�̰��)")]
    public int Skill_Value;
    [FoldoutGroup("Coin_ori"), Tooltip("���� �⺻��")]
    public int coin_Value; // ���� �⺻��
    //[FoldoutGroup("Coin_ori"), Tooltip("�ּ��������� \n(�޸� Ȥ�� ������ �����ϳ� �����佺������� �����Ұ��(�ĺ���)���� ����)")]
    //public int coin_MinimamValue; // �ּ���������

}
