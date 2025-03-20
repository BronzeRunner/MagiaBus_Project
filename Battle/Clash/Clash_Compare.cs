using System.Collections.Generic;
using UnityEngine;
public enum Clash_EndState{Same,Win,Lose}
public class Clash_Compare : MonoBehaviour
{
    Skill_CoinCaculate clash_A ;
    Skill_CoinCaculate clash_B ;

    public void Clash_ClashStart()
    {
        while(true)
        {
            Clash_CoinsCheck();

            var clashResult = Clash_ValueComapare(clash_A.skill_Value_Cur,clash_B.skill_Value_Cur);
            Clash_ClashResult(clash_A,clashResult.A);
            Clash_ClashResult(clash_B,clashResult.B);


            //코인갯수 확인 
            //한쪽 0개일경우 해당공격 합패배 남아있는쪽 승리 , 둘다 0개일경우 무승부
            //이긴쪽이 먼저 공격 사용 그후 진쪽이 공격사용 무승부일경우 속도순 

            //승리 패배 무승부 알림

            //각각 공격 코인 남아있는거 실행
        }
        


    }

    public void Clash_CoinsCheck()
    {
        for(int i = 0 ; i <(clash_A.skill_Coin_Count_Cur > clash_B.skill_Coin_Count_Cur ? clash_A.skill_Coin_Count_Cur:clash_B.skill_Coin_Count_Cur);i++ )
            {
                if(i < clash_A.skill_Coin_Count_Cur)
                {
                    clash_A.Coin_Value_Calculate(i);
                }

                if(i < clash_B.skill_Coin_Count_Cur)
                {
                    clash_B.Coin_Value_Calculate(i);
                }
            }
    }

    public void Clash_ClashResult(Skill_CoinCaculate skill_Main, Clash_EndState clash_EndState)
    {
        switch(clash_EndState)
        {
            case Clash_EndState.Same:
            {
                break;
            }
            case Clash_EndState.Win:
            {
                break;
            }
            case Clash_EndState.Lose:
            {
                skill_Main.Coin_Break();
                break;
            }
        }
    }

    public (Clash_EndState A,Clash_EndState B) Clash_ValueComapare(int a,int b)
    {
        (Clash_EndState A,Clash_EndState B) result = (Clash_EndState.Same,Clash_EndState.Same);
        if(a>b)
        {
            result.A = Clash_EndState.Win;
            result.B = Clash_EndState.Lose;
        }
        else if(a<b)
        {
            result.A = Clash_EndState.Lose;
            result.B = Clash_EndState.Win;
        }
        
        return result;
    }
}
