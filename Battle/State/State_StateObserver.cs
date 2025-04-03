using System.Collections.Generic;
using UnityEngine;
public delegate void State_Effect_Delegate(int id);
public interface IState_Enter{ public void I_State_Enter();}
public interface IState_Exit{ public void I_State_Exit();}
public interface IState_Excute{ public void I_State_Excute();}
public interface IState_Check{public bool I_State_Check(float f);}
public interface IState_Main:IState_Enter,IState_Excute,IState_Exit
{

}
public interface IState_AddObserve{public void I_Observe_Add(State_Effect_Delegate state_Effect_Delegate);}
public interface IState_Observe : IState_Main,IState_AddObserve{};

public class Observe_class 
{
    int id;//
    State_Effect_Delegate state_Effect_Delegate;

    public void Observe_Active()
    {
        state_Effect_Delegate(id);
    }

    public void Observe_Add(State_Effect_Delegate state_Effect_Delegate)
    {
        this.state_Effect_Delegate += state_Effect_Delegate;
    }

    public void Observe_Remove(State_Effect_Delegate state_Effect_Delegate)
    {
        this.state_Effect_Delegate -= state_Effect_Delegate;
    }
}

public enum Observe_ActiveType{Enter,Excute,Exit};
//State_Changer 참고

// State_Changer 에서 관리 생태 최소단위
public class State_StateObserver :IState_Observe
{
    [SerializeField]
    //현재 상태
    State_Enum state_Name;

    public State_StateObserver(State_Enum state_Enum)
    {
        state_Name = state_Enum;
    }
    // int : id(누가 걸었는지지)
    Dictionary<Observe_ActiveType,Dictionary<int,Observe_class>> state_Observes;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void I_Observe_Add(State_Effect_Delegate state_Effect_Delegate)
    {
        
    }
    
    public void I_State_Enter()
    {
        
        Observe_Active(Observe_ActiveType.Enter);
        Debug.Log($"Enter_{state_Name}");
    }

    public void I_State_Excute()
    {
        Observe_Active(Observe_ActiveType.Excute);
        Debug.Log($"Excute_{state_Name}");
    }

    public void I_State_Exit()
    {
        Observe_Active(Observe_ActiveType.Exit);
        Debug.Log($"Exit_{state_Name}");
    }


    public void Observe_Active(Observe_ActiveType observe_ActiveType)
    {
        if(state_Observes.TryGetValue(observe_ActiveType,out Dictionary<int,Observe_class> list)) 
        {
            foreach(int observer in list.Keys)
            {
                list[observer].Observe_Active();
            }
        }
    }
}

public class ExampleClass 
{
    int hp;

    int Effect_Value , Effect_Count;

    Transform target;

    public void hit(int instanceId , int value)
    {
        hp += value;
        
    }

}

