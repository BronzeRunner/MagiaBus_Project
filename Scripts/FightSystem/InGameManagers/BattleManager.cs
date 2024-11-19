using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

public enum coin_type
{
    Null, Attack = 2, evasion = 0, Deffend = 0, RevAtk = 1
}
/*
public struct Charecter_State
{
    int MaxHp;
    int curHp;
    public Dictionary<string, List<float>> CurBuff;
    public Dictionary<string, List<float>> CurDeBuff;

    public Dictionary<string, UnityEvent> TimeEvent; // Ư������ �߻��ϴ� �̺�Ʈ���� ���� (�ս��۽�, �Ͻ��۽�, ������� ���)


}
public struct EffectS
{
    public Effect_type type;
    public int Count;
    public int Value;
    public UnityEvent Effect;
}
*/



public struct AttackCoins 
{

    //�ش������� ����ϴ� ĳ����(����) �� ����� ���� �����Ҽ��ְ��ϴ� �Լ�

    public coin_type Type;


    /*public void GetValues()
    {

    }*/
    public float Attack_Speed; // �ش���� �ӵ�(�Ϲ������δ� ĳ���ͼӵ� �״��)
    public float Attack_Weight; //���� ����ġ
    public float clash_minimum; //�� �ּҰ�
    [FoldoutGroup("ChangeValue")] // ���氪 ����
    public float clash_ChangeValue;//�� �ּҰ�����ġ
    [BoxGroup("Coin_ori")]
    public float coin_minimum; // ���� �⺻��
    [BoxGroup("Coin_ori")]
    public int coin_Count; //���ΰ���
    [FoldoutGroup("ChangeValue")]
    public float coin_changeValue; // �������� ����
    [BoxGroup("Coin_ori")]
    public float coin_OriValue; // �⺻��������
    EffectTriggerSystem Skill_Effects;

    public float GetCoinValue(bool coin)
    {
        if (coin)
        {

            return coin_minimum + coin_changeValue + coin_OriValue;
        }
        else
        {
            return coin_minimum;
        }

    }

    public void Coin_Break()
    {

    }
    public int Atk_Multiplier;
    public float Atk_Level;
    public float Atk_LevelPlus;
    public Character_Main Owner;
}


public class BattleManager :EffectManager
{


    List<List<Character_Connector>> Character_Group_Select; //�Ʊ� �� 3���� ���ü��� ����
    List<List<Character_Connector>> Character_Group_Speed; //�Ʊ� �� 3���� �׷� �ӵ��� ���� (�Ͻ��۽ø��� �ӵ� ���ϰ� ����)

    public void Character_Group_Create(Transform parent)
    {
        List<Character_Connector> character_Connectors = new List<Character_Connector>(parent.childCount);
        foreach(Character_Main main in parent.GetComponentsInChildren<Character_Main>())
        {
            character_Connectors.Add(new Character_Connector(main)) ;
        }
        Character_Group_Select.Add(character_Connectors);
    }


    public void Group_Speed_Set(int i)
    {
        List<Character_Connector> Connectors =new List<Character_Connector>();
        int count = 0;
        foreach(Character_Connector Connector in Character_Group_Select[i])
        {
            if(Connector.Character.CurState == Character_Main.State.Dead)
            {
                break;
            }
            Connector.Character.Speed_Set();
            //Connectors.Add(Connector);
            for(int i2 =count; i2 > 0; i2 --)
            {
                if (Connectors[i2].Character.Speed_Cur > Connectors[i2 -1].Character.Speed_Cur)
                {
                    Connectors[i2] = Connectors[i2 - 1];
                    Connectors[i2 - 1] = Connector;
                }
                else
                {
                    break;
                }
            }
        }
        Character_Group_Speed[i] = Connectors;


    }


    public class Fight
    {

        public Fight(Character_Connector A,AttackCoins SkillA,Character_Connector B,AttackCoins SkillB)
        {
            Excutioner_A = A;
            Skill_A = SkillA;
            //����ġ������ ���ݴ�� ���� (Target_ �� �߰�)
            Excutioner_B = B;
            Skill_B = SkillB;
        }
        [FoldoutGroup("A")]
        Character_Connector Excutioner_A;
        [FoldoutGroup("B")]
        Character_Connector Excutioner_B;
        [FoldoutGroup("A")]
        List<Character_Connector> Targets_A;
        [FoldoutGroup("B")]
        List<Character_Connector> Target_B;
        [FoldoutGroup("A")]
        AttackCoins Skill_A;
        [FoldoutGroup("B")]
        AttackCoins Skill_B;
        
        public string Clash_Check_A()
        {
            string result;
            while(Skill_A.coin_Count > 0 && Skill_B.coin_Count > 0)
            {
                switch (Clash_Check_B())
                {
                    case 0: // ��
                        {
                            break;
                        }
                    case 1: // A �ս¸�
                        {
                            Skill_B.Coin_Break();
                            break;
                        }
                    case 2:// B �ս¸�
                        {
                            Skill_A.Coin_Break();
                            break;
                        }
                }

            }
            if(Skill_A.coin_Count == 0 && Skill_B.coin_Count == 0)
            {
                result = "";
            }
            else if(Skill_A.coin_Count <= 0)
            {
                result = "";
            }
            else
            {
                result = "";
            }

            return result;
        }

        


    }


    public enum Fight_type
    {
        both, one, Cancle// ��,�Ϲ����,
    }

    [SerializeField]
    bool Debug_Log;

    [SerializeField, ShowIf("Debug_Log", true)]

    List<bool> Debug_WhichLog;

    List<string> Effect_List;
    [SerializeField]
    AttackCoins CurAttackCoinsA;
    [SerializeField]
    AttackCoins CurAttackCoinsB;
    public AttackCoins[] test_Attacks;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //CoinCalculate_Clash(null, null);
    }
    IEnumerator ClashCalCulater(Character_Main Allie, Character_Main Enemy)
    {
        if (Allie.Speed_Cur > Enemy.Speed_Cur)
        {
            Attacker_Main_p = Allie;
            Deffender_Main_p = Enemy;
        }
        else
        {
            Attacker_Main_p = Enemy;
            Deffender_Main_p = Allie;
        }

        //Attacker Deffender �����ư��鼭 ���� ������ *uiǥ�ô� �� ���� �ѹ���
        //���� �� ���������� �� ��,�������� �¸� �й������� �����ϳ� �ı� �����Ұ�� ��+1
        //�������� �׼� �����ı� , �����佺 , ȿ�� �ޱ�()



        yield return null;
    }

    public AttackCoins Attacker_Coins_p;
    public Character_Main Attacker_Main_p;
    public AttackCoins Deffender_Coins_p;
    public Character_Main Deffender_Main_p;


    public Dictionary<EffectScriptType, Action<Component>> AttackerSettingActions;
    public Dictionary<EffectScriptType, Action<Component>> DeffenderSettingActions;
    public float ClashCount;
    public Fight_type Cases;
    /*IEnumerator CoinCalculate(AttackCoins Attacker_Coins, AttackCoins Deffender_Coins)
    {
        
        Attacker_Coins_p = Attacker_Coins;
        Deffender_Coins_p = Deffender_Coins;
        Attacker_Main_p = Attacker_Coins_p.Owner;
        Deffender_Main_p = Deffender_Coins_p.Owner;
        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.Start);

        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.Start);

        if (Attacker_Coins_p.Type != coin_type.Attack || Deffender_Coins_p.Type != coin_type.Attack)
        {
            if(Attacker_Coins_p.Type != coin_type.Attack && Deffender_Coins_p.Type != coin_type.Attack)
            {
                Cases = Fight_type.Cancle;
            }
            else if(Attacker_Coins_p.Type > 0 && Deffender_Coins_p.Type > 0 && (int)Attacker_Coins_p.Type + (int)Deffender_Coins_p.Type > 2)
            {
                Cases = Fight_type.both;
            }
            else
            {
                Cases = Fight_type.one;
            }
        }
        else
        {
            Cases = Fight_type.both;
        }

        
        ClashCount = 0;
        switch (Cases)
        {
            case Fight_type.both:
                {
                    while (Attacker_Coins_p.coin_Count > 0 && Deffender_Coins_p.coin_Count > 0)
                    {
                        
                        
                            switch (ClashCheck()) // (Attacker)�ս¸�,��,���й� :0,1,2
                            {
                                case 0:
                                    {

                                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.Clash_Win);
                                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.Clash_Lose);
                                        //�����ı�
                                        break;
                                    }
                                case 1:
                                    {
                                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.Clash_Same);
                                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.Clash_Same);
                                        ClashCount++;
                                        break;
                                    }
                                case 2:
                                    {
                                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.Clash_Lose);
                                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.Clash_Win);
                                        //�����ı�
                                        break;
                                    }

                            }
                        

                    }

                    if(Attacker_Coins_p.coin_Count == Deffender_Coins_p.coin_Count)
                    {
                        
                    }
                    else if(Attacker_Coins_p.coin_Count == 0)
                    {
                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.ClashEnd_Win);
                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.ClashEnd_Lose);
                    }
                    else
                    {
                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.ClashEnd_Win);
                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.ClashEnd_Lose);
                    }


                    AttackEnd();
                    break;
                }
            case Fight_type.Cancle:
                {
                    break;
                }
            case Fight_type.one_AB:
                {
                    
                    break;
                }
        }


        


        yield return null;

    }
    public void AttackEnd()
    {
        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.End);
        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.End);
    }



    public int ClashCheck()
    {

        float count = Attacker_Coins_p.coin_Count >= Deffender_Coins_p.coin_Count ? Attacker_Coins_p.coin_Count : Deffender_Coins_p.coin_Count;
        float AttackerValue = Attacker_Coins_p.clash_minimum;
        float DeffenderValue = Deffender_Coins_p.clash_minimum;
        TriggerType Ttype;
        
        for (int i = 1; i <= count; i++)
        {
            TriggerType.TryParse($"ClashCoin{i}_Check", out Ttype);
            if (Attacker_Coins_p.coin_Count <= i)
            {
                AttackerValue += CoinCheck_Clash(Attacker_Main_p, ref Attacker_Coins_p, i);
                TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, Ttype);
                //����[i]���� �̺�Ʈ
            }

            if (Deffender_Coins_p.coin_Count <= i)
            {
                DeffenderValue += CoinCheck_Clash(Deffender_Main_p, ref Deffender_Coins_p,i);
                TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, Ttype);
                //����[i]���� �̺�Ʈ
            }
            count = Attacker_Coins_p.coin_Count >= Deffender_Coins_p.coin_Count ? Attacker_Coins_p.coin_Count : Deffender_Coins_p.coin_Count;
        }
        if (AttackerValue > DeffenderValue)
        {
            return 0;
        }
        else if (DeffenderValue > AttackerValue)
        {
            return 2;
        }

        return 1;
        
    }

    public bool CoinCheck_Who;
    public float CoinCheck_Result;
    public float CoinCheck_ChangeValue;
    public bool CoinCheck_Bool;

    public void CoinCheck_TriggerWho(bool who, TriggerType theKey)
    {
        if (who)
        {
            TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, theKey);
        }
        else
        {
            TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, theKey);
        }
    }
    //������� ����Ȯ��
    public float CoinCheck_Clash( Character_Main main ,ref AttackCoins coins , int count)
    {
        CoinCheck_Who = main == Attacker_Main_p ? true : false;
        CoinCheck_Result = 0;
        CoinCheck_ChangeValue = 0;
        CoinCheck_Bool = UnityEngine.Random.Range(1, 101) <= main.GetCoinPercentage() ? true : false;
        TriggerType Ttype; TriggerType.TryParse($"ClashCoin{count}_{CoinCheck_Bool}",out Ttype);

        CoinCheck_TriggerWho(CoinCheck_Who, Ttype);
        // ����[count] ��,�� �� �̺�Ʈ
        CoinCheck_ChangeValue += coins.GetCoinValue(CoinCheck_Bool);
        CoinCheck_TriggerWho(CoinCheck_Who, TriggerType.ClashCoin_CheckEnd);
        // ���� �����̺�Ʈ
        CoinCheck_Result += CoinCheck_ChangeValue + coins.coin_minimum;
        
        return CoinCheck_Result;
    }
    //�Ϲ���ݽ� ����Ȯ��
    public float CoinCheck_Attack(Character_Main main,ref AttackCoins coins,int count)
    {
        CoinCheck_Who = main == Attacker_Main_p ? true : false;
        CoinCheck_Result = 0;
        CoinCheck_ChangeValue = 0;
        CoinCheck_Bool = UnityEngine.Random.Range(1, 101) <= main.GetCoinPercentage() ? true : false;
        TriggerType Ttype; TriggerType.TryParse($"AttackCoin{count}_{CoinCheck_Bool}", out Ttype);

        CoinCheck_TriggerWho(CoinCheck_Who, Ttype);
        // ����[count] ��,�� �� �̺�Ʈ
        CoinCheck_ChangeValue += coins.GetCoinValue(CoinCheck_Bool);
        CoinCheck_TriggerWho(CoinCheck_Who, TriggerType.ClashCoin_CheckEnd);
        // ���� �����̺�Ʈ
        CoinCheck_Result += CoinCheck_ChangeValue + coins.coin_minimum;

        return CoinCheck_Result;
    }

    public void TriggerEffectInvoke(ref Dictionary<TriggerType, Action> lists, TriggerType theKey)
    {
        if(theKey == TriggerType.Fail)
        {
            Debug.LogError("Error From StringToTriggerType");
        }
        if (lists.ContainsKey(theKey))
        {
            lists[theKey]?.Invoke();
        }
    }
    
    public float Attack_Value;
    public float Deffender_Value;
    public void Attack(ref AttackCoins Winner,ref AttackCoins Deffender)
    {
        Attack_Value = 0;
        Deffender_Value = 0;
        Attack_Value += Winner.clash_minimum;
        //RevAttack �� TriggerAction������?
        if (Deffender.Type == coin_type.Deffend)
        {
            for (int i = 0; i < Deffender.coin_Count; i++)
            {
                Deffender_Value+= CoinCheck_Attack(Deffender.Owner, ref Deffender, i);
            }
            //�� ��� �Լ�����

        }
        for (int i = 0; i < Winner.coin_Count; i ++)
        {
            Attack_Value += CoinCheck_Attack(Winner.Owner, ref Winner, i);
            if (Deffender.Type == coin_type.evasion)
            {
                for (int i1 = 0; i1 < Deffender.coin_Count; i1++)
                {
                    Deffender_Value += CoinCheck_Attack(Deffender.Owner, ref Deffender, i1);
                }

                if(Deffender_Value > Attack_Value)
                {
                    //ȸ�Ǽ���
                    continue;
                }
                else
                {
                    //ȸ�ǽ���
                }
                //���� �������Լ� ����

            }

            
        }
        

    
        
    }
    */
}
