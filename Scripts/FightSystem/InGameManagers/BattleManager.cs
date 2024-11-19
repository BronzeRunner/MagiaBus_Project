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
    public float Attack_Speed; // 해당공격 속도(일반적으로는 캐릭터속도 그대로)
    public float Attack_Weight; //공격 가중치
    public float clash_minimum; //합 최소값
    [FoldoutGroup("ChangeValue")] // 변경값 모음
    public float clash_ChangeValue;//합 최소값변동치
    [BoxGroup("Coin_ori")]
    public float coin_minimum; // 코인 기본값
    [BoxGroup("Coin_ori")]
    public int coin_Count; //코인개수
    [FoldoutGroup("ChangeValue")]
    public float coin_changeValue; // 코인위력 증감
    [BoxGroup("Coin_ori")]
    public float coin_OriValue; // 기본코인위력
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


    List<List<Character_Connector>> Character_Group_Select; //아군 적 3세력 선택순서 나열
    List<List<Character_Connector>> Character_Group_Speed; //아군 적 3세력 그룹 속도순 나열 (턴시작시마다 속도 정하고 정렬)

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
            //가중치에따른 공격대상 지정 (Target_ 에 추가)
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
                    case 0: // 합
                        {
                            break;
                        }
                    case 1: // A 합승리
                        {
                            Skill_B.Coin_Break();
                            break;
                        }
                    case 2:// B 합승리
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
        both, one, Cancle// 합,일방공격,
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

        //Attacker Deffender 번갈아가면서 코인 뒤집기 *ui표시는 두 코인 한번에
        //코인 다 뒤집혔을시 값 비교,높은쪽이 승리 패배한쪽은 코인하나 파괴 동일할경우 합+1
        //만들어야할 액션 코인파괴 , 코인토스 , 효과 받기()



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
    */
}
