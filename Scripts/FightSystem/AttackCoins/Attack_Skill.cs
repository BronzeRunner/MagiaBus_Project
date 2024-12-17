using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Events;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public enum ComparisonResult { Win, Same, Lose }
public enum CoinType { Normal, UnBreakable, Rev_Atk/*,Rev_Eva*/ }
public enum CoinTosType { Clash, Attack }
/// <summary>
/// Wrath : �г� ,
///Lust : ���� ,
/// Sloth : ���� ,
/// Gluttony : Ž�� ,
/// Gloom : ��� ,
/// Pride : ���� ,
/// Envy  :����,
/// </summary>
public enum SinType {Envy,Gloom,Gluttony,Lust,Pride,Sloth,Wrath } //Wrath : �г� , Lust : ���� , Sloth : ���� , Gluttony : Ž�� ,Gloom : ��� , Pride : ���� , Envy  :����
/// <summary>
/// Slash : ���� ,
/// Pierce ���� ,
/// Blunt Ÿ��
/// </summary>
public enum AttackType {Slash,Pierce,Blunt } // Slash : ���� , Pierce ���� , Blunt Ÿ��

public class Attack_Skill :EffectTriggerSystem
{
    public Character_Main Owner;
    SinType Skill_AttributeType;
    public int Attack_SkillCount;// ��ų ����
    
    [SerializeField]
    public Attack_Values Attack_OriValues;
    [Tooltip("�ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)")]
    public int Attack_Speed; // �ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)
    [Tooltip("���� ����ġ")]
    public int Attack_Weight; //���� ����ġ
    [Tooltip("�� �ּҰ�(�⺻��)")]
    public int clash_NormalValue; //�� �ּҰ�(�⺻��)
    public int clash_CurValue;
    public int Attack_CurValue;
    [FoldoutGroup("Coin"), Tooltip("���� �⺻��")]
    public int Coin_Value; // ���� �⺻��
    [FoldoutGroup("Coin"), Tooltip("�ּ��������� \n(�޸� Ȥ�� ������ �����ϳ� �����佺������� �����Ұ��(�ĺ���)���� ����)")]
    public int Coin_MinimamValue; // �ּ���������
    [FoldoutGroup("Coin"),Tooltip("�⺻ ���ΰ���")]
    public int Coin_Count; //���ΰ���(�⺻)
    [FoldoutGroup("Coin"), Tooltip("�������� ���ΰ���")]
    public int Coin_CurCount; // ���ΰ���(��������)
    public List<CoinType> Coin_CoinTypes;
    public int GetCoinValue(bool coin)
    {
        
        if (coin)
        {
            return Coin_MinimamValue + Coin_Value;
        }
        else
        {
            return Coin_MinimamValue;
        }
        
    }
    public void Coin_Lose(int i)
    {
        while(i > 0)
        {
            Coin_Break();
            i--;
            
        }
    }

    public void Coin_Break()
    {
        Coin_CurCount--;
    }
    /*
    public int Atk_Multiplier;
    public float Atk_Level;
    public float Atk_LevelPlus;
    */

    public int CoinToss_Clash(int coin_Count)
    {
        return CoinToss(coin_Count, CoinTosType.Clash);
    }

    public int CoinToss_Attack(int coin_Count)
    {
        return CoinToss(coin_Count, CoinTosType.Attack);
    }
    private int CoinToss(int coin_Count, CoinTosType TosType)
    {
        int result = 0;
        if (Owner.CoinCalculate())
        {
            result += Coin_Value;
            //���� �ո�
        }
        else
        {

            //���� �޸�
        }
        return result;
    }
    // Start is called before the first frame update

    public virtual void Skill_Attack(ComparisonResult clash_r, Character_Main Target)
    {
        //�������ϴ� �Լ� ���鶧 ref�����ΰ������� ����
        clash_CurValue = clash_NormalValue;
        for (int i = 0; i < (Coin_Count > Coin_CurCount ? Coin_Count : Coin_CurCount); i++)
        {
            switch (Coin_CoinTypes[i])
            {
                case CoinType.Normal:
                    {
                        if (Coin_CurCount < i)
                        {
                            Attack_CurValue += CoinToss_Attack(i);
                        }

                        break;
                    }
                case CoinType.UnBreakable:
                    {
                        Attack_CurValue += CoinToss_Attack(i);
                        Target.Damage_Get(Attack_CurValue);
                        break;
                    }
                case CoinType.Rev_Atk:
                    {
                        if (clash_r == ComparisonResult.Lose)
                        {
                            Attack_CurValue += CoinToss_Attack(i);
                        }
                        break;
                    }
            }

            Attack_CurValue += CoinToss_Attack(i);
            Target.Damage_Get(Attack_CurValue);
        }


    }



}




