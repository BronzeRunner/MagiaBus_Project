using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;


public class Attack_Skill :EffectTriggerSystem
{
    public coin_type Type;
    [SerializeField]
    public Attack_Values Attack_OriValues;
    [Tooltip("�ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)")]
    public float Attack_Speed; // �ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)
    [Tooltip("���� ����ġ")]
    public float Attack_Weight; //���� ����ġ
    [Tooltip("�� �ּҰ�(�⺻��)")]
    public float clash_minimum; //�� �ּҰ�
    [FoldoutGroup("Coin_ori"), Tooltip("���� �⺻��")]
    public float coin_Original; // ���� �⺻��
    [FoldoutGroup("Coin_ori"), Tooltip("���� �迭")]
    public List<Coin> coin_coins;
    [FoldoutGroup("Coin_ori"), Tooltip("�ּ��������� \n(�޸� Ȥ�� ������ �����ϳ� �����佺������� �����Ұ��(�ĺ���)���� ����)")]
    public float coin_MinimamValue; // �ּ���������
    public Dictionary<string, float> coin_ChangeValue = new Dictionary<string, float>(); //
    [SerializeField]
    public EffectTriggerSystem Skill_Effects = new EffectTriggerSystem();
    public Attack_Skill skill_test;

    public float GetCoinValue(bool coin)
    {
        if (coin)
        {

            return coin_MinimamValue + coin_Original;
        }
        else
        {
            return coin_MinimamValue;
        }

    }

    public void Coin_Break()
    {

    }
    public int Atk_Multiplier;
    public float Atk_Level;
    public float Atk_LevelPlus;
    public Character_Main Owner;
    // Start is called before the first frame update
    
}

public class Coin : MonoBehaviour
{

}
