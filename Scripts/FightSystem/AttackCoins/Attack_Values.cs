using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Attack_Values", menuName = "ScriptableObjects/Attack_ScriptableObject", order = 1),System.Serializable]
public class Attack_Values : ScriptableObject
{
    [FoldoutGroup("Skill_ori/Type")]
    public AttackType Skill_AttackType;
    [FoldoutGroup("Skill_ori/Type")]
    public SinType Skill_SinType;
    [FoldoutGroup("Skill_ori")]
    public int Skill_Count;
    [FoldoutGroup("Skill_ori"),Tooltip("공격스킬일경우 공격레벨\n수비스킬일경우 방어레벨\n(레벨 미포함)")]
    public int Skill_Level;
    [FoldoutGroup("Skill_ori"),Tooltip("스킬 기본위력(공렙,방렙미계산)")]
    public int Skill_Value;

    [FoldoutGroup("Coin_ori"), Tooltip("코인갯수")]
    public float coin_Count;
    [FoldoutGroup("Coin_ori"), Tooltip("코인 기본값")]
    public int coin_Value; // 코인 기본값
    [FoldoutGroup("Coin_ori"), Tooltip("코인")]
    public CoinType[] coin_CoinTypes;

    [FoldoutGroup("Skill_ori")]
    public int Attack_Weight;
    //[FoldoutGroup("Coin_ori"), Tooltip("최소코인위력 \n(뒷면 혹은 코인이 존재하나 코인토스미적용시 존재할경우(파불코)에도 적용)")]
    //public int coin_MinimamValue; // 최소코인위력

}
