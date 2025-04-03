using UnityEngine;

public class State_Static : MonoBehaviour

{
    //효과관련 배열은 여기서 전부관리 배열 이벤트 전부가져오면 비효율적일테니 State_StateObserver를 가져옴
    //dictionary key값은 StateEnum

    //발동등도 여기서

    public static State_Static Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }
    }

    State_MultiState state_MultiState_Head = null;

    public void State_StateEnter(State_Changer state_Changer)
    {
        State_MultiState state_MultiState = new State_MultiState();
        state_MultiState.state_CurLayer_State = state_Changer;
    }
}
