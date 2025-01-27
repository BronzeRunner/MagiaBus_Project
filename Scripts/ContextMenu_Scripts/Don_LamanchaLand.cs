using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using Sirenix.Serialization;


public class Don_LamanchaLand : Character_Main
{
    public string Identity_Name;
    [Range(1, 3), MinValue(1), MaxValue(3)]
    public int Identity_Grade = 3;
    //public int Identity_Level = 50;
    //public JsonDataReader ???
}

public class Skill_Don_LamanchLand_EnoughIsEnough : Attack_Skill
{

    /*
     �� ��ų���� ���� ��, ���� ü�� ���ط��� 30%��ŭ ü�� ȸ�� (�ִ� 10 ȸ��) (EffectTrigger�� ���� ���� ������ Ȯ��)
    [����] ������ �ִ� 50 �Ҹ��Ͽ�, ���� 5 ��, ���� 1 ���� 
            ������ �Ҹ����� ��������, ���� 5 ����
    [����] ����� ���� 6 ��, ���� ���� +1 (�ִ� 2)

    (����1)[���߽�] ���� 2 �ο�
    (����2)[���߽�] ���� 2 �ο�
     */
    [SerializeField]
    int Heal_Onhit_Cur;
    [SerializeField]
    int Heal_Onhit_Max = 30;
    
    /// <summary>
    /// ����ü�� ���ط�
    /// </summary>
    /// <param name="i">��ų�� ���� ���ط�</param>
    public void OnEnemyHit(ref int i)
    {
        if(i < 0)
        {

        }
    }
    public void OnUse(ref int i)
    {
        int Value = i / 5;
        if (Value >0) // ���� �Ҹ�� ���� Value ��ŭ ����
        {
            i -= Value * 5;
        }
        else // ���ܼҸ� ���н� ���� 5,0 ����
        {

        }

        Character_Main Enemy;//BattleManager ���� �������� �޾ƿ�
        //if
    }
}

