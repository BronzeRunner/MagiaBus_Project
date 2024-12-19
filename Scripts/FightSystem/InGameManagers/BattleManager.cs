using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;
using UnityEditor.Experimental.GraphView;
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






public class BattleManager :EffectManager
{
    #region speed

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
    #endregion

    //속도시스템 , 캐릭터 메인 재확인
    /*
    public class Fight_Normal
    {

        public Fight_Normal(Character_Connector A, Attack_Skill SkillA, Character_Connector B, Attack_Skill SkillB)
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
        Attack_Skill Skill_A;
        [FoldoutGroup("B")]
        Attack_Skill Skill_B;
        [FoldoutGroup("C")]
        public int ClashCount_C;

        [FoldoutGroup("A")]
        int ClashValue_A;
        [FoldoutGroup("B")]
        int ClashValue_B;
        
        /* public string Clash_Check_A()
        {
           if(Skill_B == null)
            {


            }
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
        }*/
    List<BattleSystem> battleSystems = new List<BattleSystem>();

    public int Clash_Check_B()
        {
            int Result = 0;


            return Result;
        }
/*
        IEnumerator CoinCheck()
        {
            
            while (true)
            {
                while(true)
                {
                    //합진행중 코인추가 고려
                    for(int i = 0;i < (Skill_A.coin_Count >= Skill_B.coin_Count ? Skill_A.coin_Count:Skill_B.coin_Count); i++)
                    {
                        if(i < Skill_A.coin_Count)
                        if(UnityEngine.Random.Range(0,100) >= Excutioner_A.Character.GetCoinPercentage())
                        {
                            //앞면
                            ClashValue_A += (int)Skill_A.GetCoinValue(true);

                        }
                        else
                        {
                            //뒷면
                            ClashValue_A += (int)Skill_A.GetCoinValue(false);

                        }
                    }
                    yield return null;
                    if(true)
                    {
                        break;
                    }
                    ClashCount_C += 1;
                    if(ClashCount_C >= 99)
                    {

                    }
                    
                }

                yield return null;
            }
            
        }
        */


    
    /*

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
public class BattleSystem
{
    public int clash_Count;
    public Attack_Skill clash_SkillA = null, clash_SkillB = null;
    public int clash_ValueA, clash_ValueB;
    public void BattleSystem_Reset()
    {
        clash_SkillA = null;
        clash_SkillB = null;

        clash_Count = 0;

        clash_ValueA = 0;
        clash_ValueB = 0;
    }
    public bool Clash_EndCheck()
    {
        if (clash_Count >= 100 || clash_SkillA.Coin_CurCount <= 0 && clash_SkillB.Coin_CurCount <= 0)
        {
            if (clash_SkillA.Coin_CurCount <= 0 && clash_SkillB.Coin_CurCount <= 0)
            {

            }
            return true;
        }
        else if (clash_SkillB.Coin_CurCount <= 0) //B 합 패배
        {
            
            clash_SkillA.Skill_Attack(ComparisonResult.Win, clash_SkillB.Owner);

            clash_SkillB.Skill_Attack(ComparisonResult.Lose, clash_SkillA.Owner);
            return true;
        }
        else if (clash_SkillA.Coin_CurCount <= 0) //A 합 패배
        {
            
            clash_SkillB.Skill_Attack(ComparisonResult.Win, clash_SkillB.Owner);

            clash_SkillA.Skill_Attack(ComparisonResult.Lose, clash_SkillA.Owner);
            return true;
        }

        return false;
    }
    public void Skill_Start(Attack_Skill clash_SkillA, Attack_Skill clash_SkillB)
    {
        this.clash_SkillA = clash_SkillA; this.clash_SkillB = clash_SkillB;
        bool ClashEnd = false;
        while (!ClashEnd)
        {
            //합준비(위치이동 + 합 사용전 모습으로 변경)
            for (int i = 0; i < (clash_SkillA.Coin_CurCount >= clash_SkillB.Coin_CurCount ? clash_SkillA.Coin_CurCount : clash_SkillB.Coin_CurCount); i++)
            {
                if (i < clash_SkillA.Coin_CurCount)
                {
                    clash_ValueA += clash_SkillA.CoinToss_Clash(i);
                }

                if (i < clash_SkillB.Coin_CurCount)
                {
                    clash_ValueB += clash_SkillB.CoinToss_Clash(i);
                }
            }

            if (clash_ValueA > clash_ValueB) // A 합승리
            {
                clash_SkillB.Coin_Lose(1);
                ClashEnd = Clash_EndCheck();
                // A 합승리 에니메이션
            }
            else if (clash_ValueA == clash_ValueB) // 무승부
            {
                clash_Count += 1;
            }
            else // B 합승리
            {
                clash_SkillA.Coin_Lose(1);
                ClashEnd = Clash_EndCheck();
                // B 합승리 에니메이션
            }

            
        }


    }



}
