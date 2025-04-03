using System.Collections.Generic;
using UnityEngine;
// public interface IState_Enter{ public void I_State_Enter();}
// public interface IState_Exit{ public void I_State_Exit();}
// public interface IState_Excute{ public void I_State_Excute();}
// public interface IState_Check{public bool I_State_Check(float f);}
// public interface IState_Main:IState_Enter,IState_Excute,IState_Exit
// {

// }
// public interface IState_AddObserve { public void I_Observe_Add();}
// public interface IState_Observe : IState_Main,IState_AddObserve{}
//public enum State_layer{PublicLayer,SemiPrivateLayer,PrivateLayer}
// public enum State_Layer //전체턴 , (합,일방공격)
// {}

public class State_MultiState : MonoBehaviour
{
    //public State_Changer[] multistate_Layer = new State_Changer[]{};
    /*#region a
    Dictionary<State_StateLayer,State_Changer> multistate_Layer = new Dictionary<State_StateLayer, State_Changer>();
    public void Multistate_AddEvent(State_StateLayer stateLayer,State_Enum state_Enum,string addEvent)
    {
        State_Changer cur_StateChanger = multistate_Layer[stateLayer];

        if(!cur_StateChanger.StateCheck(state_Enum))
        cur_StateChanger.StateAdd(state_Enum,new State_StateObserver(state_Enum));
        
        
        
    }
    #endregion

    */
    
    
    //아니면 state속에 state..? 링크드 리스트 느낌으로
    //하위 state는 자주 삭제 및 재생성 되니 나중에 오브젝트폴링 느낌으로
   // public State_MultiState 

    //State_Layer state_CurLayer;
    public State_Enum state_Enum;
    public State_Changer state_CurLayer_State;
    State_MultiState state_HighLayer;
    State_MultiState state_LowLayer;

    public State_MultiState GetLast()
    {
        if(state_LowLayer == null)
        {
            return this;
        }
        else
        {
            return state_LowLayer.GetLast();
        }
    }

    public void AddNext(State_MultiState state_MultiState)
    {
        if(state_LowLayer != null)
        {
            state_LowLayer.AddNext(state_MultiState);
            return;
        }
        else
        {
            state_LowLayer = state_MultiState;
        }
    }
    public void RemoveNext()
    {
        
    }


    public void State_CurState_Execute()
    {
        state_CurLayer_State.StateExecute();
    }

    public void State_CurState_Exit()
    {
        //하위 상태들 부터 Exit
        if(state_LowLayer != null)
        {
            state_LowLayer.State_CurState_Exit();
            state_LowLayer = null;
        }
        state_CurLayer_State.StateExit();
        
    }
    

}
