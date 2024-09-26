using System.Collections.Generic;
using UnityEngine;
using System;

public class Character_Connector : MonoBehaviour 
{
    Character_Main Character;
    public List<BattleEffect> ActiveEffects= new List<BattleEffect>();
    public List<BattleEffect> DeadEffect = new List<BattleEffect>(); // 횟수값 0 등등 발동,영향을 아예 못주는 이펙트
    bool IsDead = false;
    Dictionary<string,Action> Effecft_Active;
    
    public void Effect_Active(string Type)
    {
        if(Effecft_Active.TryGetValue(Type,out Action Value))
        {
            if(Value != null)
            {
                Value.Invoke();
                Effect_Active($"{Type}_Trigger");
            }
        }
        
    }

    public void Effect_Alive(BattleEffect Effect)
    {
        DeadEffect.Remove(Effect);
        //재활성화 효과 있을시 발동
        ActiveEffects.Add(Effect);
    }
}
//BattleManager Attacker Deffender 캐릭터 커넥터로 변경
public class EffectManager : MonoBehaviour
{
    protected BattleManager BManager;
    public Dictionary<int, Character_Connector[]> Character_Teams;



}
