using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Attack_Values", menuName = "ScriptableObjects/Attack_ScriptableObject", order = 1)]
public class Attack_Values : ScriptableObject
{
    [FoldoutGroup("Coin_ori"), Tooltip("���� �⺻��")]
    public float coin_Original; // ���� �⺻��
    [FoldoutGroup("Coin_ori"), Tooltip("�ּ��������� \n(�޸� Ȥ�� ������ �����ϳ� �����佺������� �����Ұ��(�ĺ���)���� ����)")]
    public float coin_MinimamValue; // �ּ���������
}
