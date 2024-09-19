using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;
/*
 
 */
//버프 디버프 효과 발동조건



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

    public Dictionary<string, UnityEvent> TimeEvent; // 특정순간 발생하는 이벤트들의 모음 (합시작시, 턴시작시, 턴종료시 등등)


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

    //해당코인을 사용하는 캐릭터(본인) 의 디버프 값을 참조할수있게하는 함수

    public coin_type Type;


    /*public void GetValues()
    {

    }*/
    
    public float clash_minimum; //합 최소값
    [FoldoutGroup("ChangeValue")] // 변경값 모음
    public float clash_ChangeValue;//합 최소값변동치
    [BoxGroup("Coin_ori")]
    public float coin_minimum; // 코인 기본변경값
    [FoldoutGroup("ChangeValue")]
    public float coin_minimumChange;// 코인 기본변경값 변경값 ㅋㅋㅋㅋㅋㅋ
    [BoxGroup("Coin_ori")]
    public int coin_Count; //코인개수
    [FoldoutGroup("ChangeValue")]
    public float coin_changeValue; // 코인위력 증감
    [BoxGroup("Coin_ori")]
    public float coin_OriValue; // 기본코인위력
    Dictionary<string, List<BattleEffect>> Effects_Dictionary;

    public float GetCoinValue(bool coin)
    {
        if(coin)
        {

            return coin_minimum + coin_minimumChange + coin_changeValue + coin_OriValue;
        }
        else
        {
            return coin_minimum;
        }
        
    }

    public int Atk_Multiplier;
    public float Atk_Level;
    public float Atk_LevelPlus;
    public Character_Main Owner;
}



public class test : NormalEffects
{
    public enum Fight_type 
    {
        both, one, one_AB , Cancle// 합,일방공격,합이후공격
    }

    [SerializeField]
    bool Debug_Log;

    [SerializeField, ShowIf("Debug_Log",true)]

    List<bool> Debug_WhichLog;

    List<string> Effect__List;
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

    public AttackCoins Attacker_Coins_p;
    public Character_Main Attacker_Main_p;
    public AttackCoins Deffender_Coins_p;
    public Character_Main Deffender_Main_p;
   
    
    public Dictionary<EffectScriptType, Action<Component>> AttackerSettingActions;
    public Dictionary<EffectScriptType, Action<Component>> DeffenderSettingActions;
    public float ClashCount;
    public Fight_type Cases;
    IEnumerator CoinCalculate(AttackCoins Attacker_Coins, AttackCoins Deffender_Coins)
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
                        
                        
                            switch (ClashCheck()) // (Attacker)합승리,합,합패배 :0,1,2
                            {
                                case 0:
                                    {

                                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.Clash_Win);
                                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.Clash_Lose);
                                        //코인파괴
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
                                        //코인파괴
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
                //코인[i]판정 이벤트
            }

            if (Deffender_Coins_p.coin_Count <= i)
            {
                DeffenderValue += CoinCheck_Clash(Deffender_Main_p, ref Deffender_Coins_p,i);
                TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, Ttype);
                //코인[i]판정 이벤트
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
    //합진행시 코인확인
    public float CoinCheck_Clash( Character_Main main ,ref AttackCoins coins , int count)
    {
        CoinCheck_Who = main == Attacker_Main_p ? true : false;
        CoinCheck_Result = 0;
        CoinCheck_ChangeValue = 0;
        CoinCheck_Bool = UnityEngine.Random.Range(1, 101) <= main.GetCoinPercentage() ? true : false;
        TriggerType Ttype; TriggerType.TryParse($"ClashCoin{count}_{CoinCheck_Bool}",out Ttype);

        CoinCheck_TriggerWho(CoinCheck_Who, Ttype);
        // 코인[count] 앞,뒷 면 이벤트
        CoinCheck_ChangeValue += coins.GetCoinValue(CoinCheck_Bool);
        CoinCheck_TriggerWho(CoinCheck_Who, TriggerType.ClashCoin_CheckEnd);
        // 코인 판정이벤트
        CoinCheck_Result += CoinCheck_ChangeValue + coins.coin_minimum;
        
        return CoinCheck_Result;
    }
    //일방공격시 코인확인
    public float CoinCheck_Attack(Character_Main main,ref AttackCoins coins,int count)
    {
        CoinCheck_Who = main == Attacker_Main_p ? true : false;
        CoinCheck_Result = 0;
        CoinCheck_ChangeValue = 0;
        CoinCheck_Bool = UnityEngine.Random.Range(1, 101) <= main.GetCoinPercentage() ? true : false;
        TriggerType Ttype; TriggerType.TryParse($"AttackCoin{count}_{CoinCheck_Bool}", out Ttype);

        CoinCheck_TriggerWho(CoinCheck_Who, Ttype);
        // 코인[count] 앞,뒷 면 이벤트
        CoinCheck_ChangeValue += coins.GetCoinValue(CoinCheck_Bool);
        CoinCheck_TriggerWho(CoinCheck_Who, TriggerType.ClashCoin_CheckEnd);
        // 코인 판정이벤트
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
        //RevAttack 은 TriggerAction쪽으로?
        if (Deffender.Type == coin_type.Deffend)
        {
            for (int i = 0; i < Deffender.coin_Count; i++)
            {
                Deffender_Value+= CoinCheck_Attack(Deffender.Owner, ref Deffender, i);
            }
            //방어막 얻는 함수실행

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
                    //회피성공
                    continue;
                }
                else
                {
                    //회피실패
                }
                //피해 입히는함수 실행

            }
            
        }
        


        
    }
}
