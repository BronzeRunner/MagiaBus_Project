using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;

public class Test_A_Identity : Character_Main
{
    public void Test_Mental(int i)
    {
        MentalValueChange(i);
    }

    public Test_A_Identity()
    {
        State_Setting();
    }
    
    

}

public class Test_A_Skill : Attack_Skill
{
    
    public Test_A_Skill() 
    {
        
    }
}
