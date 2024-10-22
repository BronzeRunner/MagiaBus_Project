using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effect_Bleed : BattleEffect
{
    /*
     Bleed
    When tossing an attack Coin, take fixed damage by the effect¡¯s Potency. 
    Then, reduce its Count by 1.
     */
    public Effect_Bleed(int Value,int Count)
    {
        ValueChange(Value);
        CountChange(Count);
    }
    
    void Set_Event()
    {
        
    }

    public UnityAction BleedActive ;

    public override void Effect_Active()
    {
        if(BleedActive == null)
        {
            BleedActive +=  Bleed_Active; 
            BleedActive
        }
    }

    public void Bleed_Active()
    {

    }


}
