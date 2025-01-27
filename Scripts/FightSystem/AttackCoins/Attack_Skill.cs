using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Events;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
/// <summary>
/// Attack : ���� ,
/// Evade : ȸ�� ,
/// Counter �ݰ� ,
/// Deffend : ��� ,
/// Counter_p : ��ȭ�ݰ�(�հ��ɹݰ�) ,
/// Deffend_p : ��ȭ���(�հ��� ���) 
/// </summary>
public enum SkillType {Attack, Evade, Counter,Deffend,Counter_p,Deffend_p}
public enum ComparisonResult { Win, Same, Lose }
[Tooltip("Normal : �Ϲ����� \n UnBreakable : �ı��Ұ����� \n Rev_Atk : �ݰ�����(��ųX)")]
public enum CoinType { Normal, UnBreakable, Rev_Atk/*,Rev_Eva*/ }
public enum CoinTosType { Clash, Attack }
/// < summary >
/// Wrath : �г� ,
///Lust : ���� ,
/// Sloth : ���� ,
/// Gluttony : Ž�� ,
/// Gloom : ��� ,
/// Pride : ���� ,
/// Envy  :����,
/// </summary>
public enum SinType {N,Envy,Gloom,Gluttony,Lust,Pride,Sloth,Wrath } //Wrath : �г� , Lust : ���� , Sloth : ���� , Gluttony : Ž�� ,Gloom : ��� , Pride : ���� , Envy  :����
/// <summary>
/// Slash : ���� ,
/// Pierce ���� ,
/// Blunt Ÿ��
/// </summary>
public enum AttackType {N,Slash,Pierce,Blunt } // Slash : ���� , Pierce ���� , Blunt Ÿ��

//��ų ���鋚 �ش� �ڵ� ��ӽ�ų��
public class Attack_Skill :EffectTriggerSystem
{
    public string skill_Name;
    public Character_Main Owner;
    public Attack_Values attack_OriValues;
    [FoldoutGroup("Skill"),Tooltip("Attack : ����\n Evade : ȸ��\n Counter �ݰ�\n Deffend : ���\n Counter_p : ��ȭ�ݰ�(�հ��ɹݰ�)\n Deffend_p : ��ȭ���(�հ��� ���) ")]
    public SkillType skill_Type;
    [FoldoutGroup("Skill"),Tooltip("Wrath : �г� \n Lust : ���� \n Sloth : ���� \n Gluttony : Ž�� \n Gloom : ��� \n Pride : ���� \n Envy  :����")]
    public SinType skill_SinType;
    [FoldoutGroup("Skill"), Tooltip("Slash : ���� \n Pierce ���� \n Blunt Ÿ��")]
    public AttackType skill_AttackType;
    [FoldoutGroup("Skill"), Tooltip("��ų �⺻�� (���� �淾 ������ �⺻���ӿ��� ����)")]
    public int skill_Value;
    [FoldoutGroup("Skill"), Tooltip("���ݽ�ų�ϰ�� ���ݷ���\n����ų�ϰ�� ����\n(���� ������)")]
    public int skill_Level;
    [FoldoutGroup("Skill")]
    public int skill_Count;// ��ų ����
    public int Skill_CurLevel()
    {
        int curSkillLevel = (Owner == null? 0: Owner.Identity_Level) + skill_Level;
        if(skill_AttackType == AttackType.N)
        {
            //�淾
            Call_EffectTrigger("GetDeffendLevel", ref curSkillLevel);
        }
        else
        {
            //����
            Call_EffectTrigger("GetAttackLevel", ref curSkillLevel);
        }
        return curSkillLevel;
    } //���� ����/�淾

    
    [Tooltip("�ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)")]
    public int attack_Speed; // �ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)
    [Tooltip("���� ����ġ")]
    public int attack_Weight; //���� ����ġ
    
    [FoldoutGroup("Skill/Coin"), Tooltip("���� ��")]
    public int coin_Value; // ���� �⺻��
    [FoldoutGroup("Skill/Coin"),Tooltip("�⺻ ���ΰ���")]
    public int coin_Count; //���ΰ���(�⺻)
    [FoldoutGroup("Skill/Coin")]
    public List<CoinType> Coin_CoinTypes;

    [FoldoutGroup("Skill/Dynamic"), Tooltip("�� ���°�(�����淾 ���)")]
    public int skill_CurValue; //�� �ּҰ�(�⺻��)
    [FoldoutGroup("Skill/Dynamic"), Tooltip("�������� ���ΰ���")]
    public int coin_CurCount; // ���ΰ���(��������)
    [FoldoutGroup("Skill/Dynamic"), Tooltip("�������� ���� ����ġ")]
    public int attack_CurWeight; //���� ����ġ(��������)


    public Attack_Skill() //������
    {
        //this.Owner = Owner;
        Skill_Setting();
    }
    [FoldoutGroup("Skill"),Button("Skill_Setting")]
    public virtual void Skill_Setting()
    {
        skill_SinType = attack_OriValues.Skill_SinType;
        skill_AttackType = attack_OriValues.Skill_AttackType;
        skill_Count = attack_OriValues.Skill_Count;
        attack_Weight = attack_OriValues.Attack_Weight;
        // ĳ���� ��ų ����� ����ȭ�� �߰�
    }

    public void Skill_Reset()
    {
        //scriptable object �ϼ��� ���濹��
        coin_CurCount = coin_Count;
        skill_CurValue = skill_Value + Skill_CurLevel() / 3;
    }
    
    public int GetCoinValue(bool coin)
    {
        if (coin)
        {
            return coin_Value;
        }
        else
        {
            return 0;
        }
        
    }

    public void Clash_Lose()
    {
        Call_EffectTrigger("Clash_Lose");
        Owner.Call_EffectTrigger("Clash_Lose");
        Coin_Lose(1);
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
        coin_CurCount--;
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
        Call_EffectTrigger("Coin_Toss");
        Call_EffectTrigger($"Coin_Toss_{coin_Count}");
        int result = 0;
        if (Owner.CoinCalculate())
        {
            Call_EffectTrigger($"Coin_Front_{coin_Count}",ref result);
            Call_EffectTrigger("Coin_Front",ref result);
            result += coin_Value;
            //���� �ո�

        }
        else
        {
            Call_EffectTrigger($"Coin_Back_{coin_Count}",ref result);
            Call_EffectTrigger("Coin_Back",ref result);
            //���� �޸�
        }
        return result;
    }
    // Start is called before the first frame update

    public virtual void Skill_Attack(ComparisonResult clash_r, Character_Main Target)
    {
        //�������ϴ� �Լ� ���鶧 ref�����ΰ������� ����
        skill_CurValue = skill_Value;
        for (int i = 0; i < (coin_Count > coin_CurCount ? coin_Count : coin_CurCount); i++)
        {
            switch (Coin_CoinTypes[i])
            {
                case CoinType.Normal:
                    {
                        if (coin_CurCount < i)
                        {
                            skill_CurValue += CoinToss_Attack(i);
                        }

                        break;
                    }
                case CoinType.UnBreakable:
                    {
                        skill_CurValue += CoinToss_Attack(i);
                        Target.Hp_GetDamage(skill_CurValue,skill_SinType,skill_AttackType,skill_Name);
                        break;
                    }
                case CoinType.Rev_Atk:
                    {
                        if (clash_r == ComparisonResult.Lose)
                        {
                            skill_CurValue += CoinToss_Attack(i);
                        }
                        break;
                    }
            }

            skill_CurValue += CoinToss_Attack(i);
            Target.Hp_GetDamage(skill_CurValue, skill_SinType, skill_AttackType, skill_Name);
        }


    }



}




