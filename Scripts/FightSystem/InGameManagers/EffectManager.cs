using System.Collections.Generic;
using UnityEngine;
using System;

public class Character_Connector : MonoBehaviour 
{
    Character_Main Character;
    public List<BattleEffect> ActiveEffects= new List<BattleEffect>();
    public List<BattleEffect> DeadEffect = new List<BattleEffect>(); // Ƚ���� 0 ��� �ߵ�,������ �ƿ� ���ִ� ����Ʈ
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
        //��Ȱ��ȭ ȿ�� ������ �ߵ�
        ActiveEffects.Add(Effect);
    }
}
//BattleManager Attacker Deffender ĳ���� Ŀ���ͷ� ����
public class EffectManager : MonoBehaviour
{
    protected BattleManager BManager;
    public Dictionary<int, Character_Connector[]> Character_Teams;



}
