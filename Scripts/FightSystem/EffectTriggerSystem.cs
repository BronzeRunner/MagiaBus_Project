using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class EffectTriggerSystem : SerializedMonoBehaviour
{
    //T MainCode;
    [SerializeField,FoldoutGroup("EffectTrigger")]
    public Dictionary<string, UnityAction> EffectTrigger_None;
    [SerializeField, FoldoutGroup("EffectTrigger")]
    public Dictionary<string, UnityAction<int>> EffectTrigger_int;

    public void Add_EffectTrigger(string tag,UnityAction Effect)
    {
        if(!EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None.Add(tag, null);    
        }
        EffectTrigger_None[tag]+= Effect;

    }
    public void Add_EffectTrigger(string tag, UnityAction<int> Effect)
    {
        if (!EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int.Add(tag, null);
        }
        EffectTrigger_int[tag] += Effect;
    }
    public void Remove_EffectTrigger(string tag,UnityAction Effect)
    {
        if (EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None[tag] -= Effect;
        }
    }
    public void Remove_EffectTrigger(string tag, UnityAction<int> Effect)
    {
        if (EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int[tag] -= Effect;
        }
    }
    /// <summary>
    /// (tag) ��Ȳ�� �ߵ��ϴ� ������ �䱸�����ʴ� ����Ʈ�� �ߵ���Ŵ�ϴ�.
    /// </summary>
    /// <param name="tag"></param>
    public void Call_EffectTrigger(string tag)
    {
        if (EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None[tag]?.Invoke();
        }
    }
    /// <summary>
    /// (tag) ��Ȳ�� �ߵ��ϴ� int��(value)�� �䱸�� ����Ʈ�� �ߵ���Ŵ�ϴ�.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="Value"></param>
    public void Call_EffectTrigger(string tag , int Value)
    {
        if (EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int[tag]?.Invoke(Value);
        }
    }
    //public void Find_EffectTrigger(string tag)


}
