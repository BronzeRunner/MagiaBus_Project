using System;
using System.Collections.Generic;
using UnityEngine;
// 세부분류는 enum이 아닌 코드쪽에서 별도로 분류 예 합위력증가,최종위력증가,코인위력증가 등등 위력증가로 하나로 뭉치기
public enum Effect_Buff{}
public enum Effect_Debuff{Debuff_Fire,Debuff_Bleed,Debuff_Sink,Debuff_Termer}
public enum State_Enum 
{// 생각나는대로 넣어보고 가능한 간소화
State_OnHit_Hp,
State_Coin_Throw,
State_Debuff_Active,
State_Buff_Active,

}
public class State_Changer : MonoBehaviour//,IState_Check
{
    //상태 목록 발동될 효과가 있을경우 해당효과 세팅하며 생성
    Dictionary<State_Enum,IState_Main> state_StateList;
    public bool I_State_Check(State_Enum state)
    {
        return StateCheck(state);
    }
    [ContextMenu("Set")]
    public void Set()
    {
        state_StateList.Clear();
        //states.AddRange(transform.GetComponents<IState_Main>()) ;
    }
    IState_Main curState;

    
    void Start()
    {
        Set();
    }

    // Update is called once per frame
    public void StateChange(State_Enum state)
    {
        curState?.I_State_Exit();
        if(state_StateList.TryGetValue(state,out IState_Main main))
        {
            curState = main;
            curState.I_State_Enter();

        }
        else
        {
            curState = null;
        }
    }

    public void StateExit()
    {
        curState?.I_State_Exit();
        curState = null;
    }

    public void StateExecute()
    {
        curState?.I_State_Excute();
    }

    public bool StateCheck(State_Enum state)
    {
        return state_StateList.ContainsKey(state) ;
    }

    public void StateAdd(State_Enum state_Enum, IState_Main state_Main)
    {
        if(state_StateList.ContainsKey(state_Enum))
        {
            //이미 해당상태에서 발동되는것이 있을경우
            //해당 statemain IAddObserver
            return;
        }
        else
        {
            state_StateList.Add(state_Enum,state_Main);
        }
        
    }
}


