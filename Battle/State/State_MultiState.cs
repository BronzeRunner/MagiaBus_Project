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
public enum State_StateLayer //전체턴 , (합,일방공격)
{}

public class State_MultiState : MonoBehaviour
{
    //public State_Changer[] multistate_Layer = new State_Changer[]{};
    Dictionary<State_StateLayer,State_Changer> multistate_Layer = new Dictionary<State_StateLayer, State_Changer>();
    public void Multistate_AddEvent(State_StateLayer stateLayer,State_Enum state_Enum,string addEvent)
    {
        State_Changer cur_StateChanger = multistate_Layer[stateLayer];

        if(!cur_StateChanger.StateCheck(state_Enum))
        cur_StateChanger.StateAdd(state_Enum,new State_StateObserver(state_Enum));
        
        
        
    }
}
