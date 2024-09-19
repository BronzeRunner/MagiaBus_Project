using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;
/*
 
 */
//獄覗 巨獄覗 反引 降疑繕闇



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

    public Dictionary<string, UnityEvent> TimeEvent; // 働舛授娃 降持馬澗 戚坤闘級税 乞製 (杯獣拙獣, 渡獣拙獣, 渡曽戟獣 去去)


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

    //背雁坪昔聖 紫遂馬澗 蝶遣斗(沙昔) 税 巨獄覗 葵聖 凧繕拝呪赤惟馬澗 敗呪

    public coin_type Type;


    /*public void GetValues()
    {

    }*/
    
    public float clash_minimum; //杯 置社葵
    [FoldoutGroup("ChangeValue")] // 痕井葵 乞製
    public float clash_ChangeValue;//杯 置社葵痕疑帖
    [BoxGroup("Coin_ori")]
    public float coin_minimum; // 坪昔 奄沙痕井葵
    [FoldoutGroup("ChangeValue")]
    public float coin_minimumChange;// 坪昔 奄沙痕井葵 痕井葵 せせせせせせ
    [BoxGroup("Coin_ori")]
    public int coin_Count; //坪昔鯵呪
    [FoldoutGroup("ChangeValue")]
    public float coin_changeValue; // 坪昔是径 装姶
    [BoxGroup("Coin_ori")]
    public float coin_OriValue; // 奄沙坪昔是径
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
        both, one, one_AB , Cancle// 杯,析号因維,杯戚板因維
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
                        
                        
                            switch (ClashCheck()) // (Attacker)杯渋軒,杯,杯鳶壕 :0,1,2
                            {
                                case 0:
                                    {

                                        TriggerEffectInvoke(ref Attacker_Main_p.CharacterTriggers, TriggerType.Clash_Win);
                                        TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, TriggerType.Clash_Lose);
                                        //坪昔督雨
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
                                        //坪昔督雨
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
                //坪昔[i]毒舛 戚坤闘
            }

            if (Deffender_Coins_p.coin_Count <= i)
            {
                DeffenderValue += CoinCheck_Clash(Deffender_Main_p, ref Deffender_Coins_p,i);
                TriggerEffectInvoke(ref Deffender_Main_p.CharacterTriggers, Ttype);
                //坪昔[i]毒舛 戚坤闘
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
    //杯遭楳獣 坪昔溌昔
    public float CoinCheck_Clash( Character_Main main ,ref AttackCoins coins , int count)
    {
        CoinCheck_Who = main == Attacker_Main_p ? true : false;
        CoinCheck_Result = 0;
        CoinCheck_ChangeValue = 0;
        CoinCheck_Bool = UnityEngine.Random.Range(1, 101) <= main.GetCoinPercentage() ? true : false;
        TriggerType Ttype; TriggerType.TryParse($"ClashCoin{count}_{CoinCheck_Bool}",out Ttype);

        CoinCheck_TriggerWho(CoinCheck_Who, Ttype);
        // 坪昔[count] 蒋,急 檎 戚坤闘
        CoinCheck_ChangeValue += coins.GetCoinValue(CoinCheck_Bool);
        CoinCheck_TriggerWho(CoinCheck_Who, TriggerType.ClashCoin_CheckEnd);
        // 坪昔 毒舛戚坤闘
        CoinCheck_Result += CoinCheck_ChangeValue + coins.coin_minimum;
        
        return CoinCheck_Result;
    }
    //析号因維獣 坪昔溌昔
    public float CoinCheck_Attack(Character_Main main,ref AttackCoins coins,int count)
    {
        CoinCheck_Who = main == Attacker_Main_p ? true : false;
        CoinCheck_Result = 0;
        CoinCheck_ChangeValue = 0;
        CoinCheck_Bool = UnityEngine.Random.Range(1, 101) <= main.GetCoinPercentage() ? true : false;
        TriggerType Ttype; TriggerType.TryParse($"AttackCoin{count}_{CoinCheck_Bool}", out Ttype);

        CoinCheck_TriggerWho(CoinCheck_Who, Ttype);
        // 坪昔[count] 蒋,急 檎 戚坤闘
        CoinCheck_ChangeValue += coins.GetCoinValue(CoinCheck_Bool);
        CoinCheck_TriggerWho(CoinCheck_Who, TriggerType.ClashCoin_CheckEnd);
        // 坪昔 毒舛戚坤闘
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
        //RevAttack 精 TriggerAction楕生稽?
        if (Deffender.Type == coin_type.Deffend)
        {
            for (int i = 0; i < Deffender.coin_Count; i++)
            {
                Deffender_Value+= CoinCheck_Attack(Deffender.Owner, ref Deffender, i);
            }
            //号嬢厳 条澗 敗呪叔楳

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
                    //噺杷失因
                    continue;
                }
                else
                {
                    //噺杷叔鳶
                }
                //杷背 脊備澗敗呪 叔楳

            }
            
        }
        


        
    }
}
