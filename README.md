# MagiaBus_Project
 ToBreakTheCycle

오늘할꺼 (이펙트 사용(배틀매니저에서),속도,속도에따른순서(가능하면 스킬배치까지)) 함수를통한 중계?

 림버스프로젝트
{
할일
{
	
	공격 방어 계산식 만들기 버프대상(본인 혹은 합진행상대)본인은 본인 상대는 ref값으로
	이펙트 조건 자신바탕(자기자신 코드) , 상대바탕(합진행하는상대저장 변수ref 값) , 합진행or공격진행 상황바탕(BattleManager) , 이펙트 바탕()

	공격(AttackCoins)현재 test에 묶여있는걸 풀어서 각각캐릭터에 curAttackCoins 로 모아둠 + EffectDictionary 가 아닌 코인 하나하나가 효과를 가지도록(고려사항)
	공격, 회피 방어 등등 을 통한 합,상쇠공격 분류 // 진행중
	이펙트 걸려있는 버프와 공격에 걸려있는 효과 분류(공격에 걸려있는경우 공격마다 다르므로 그때그때 바뀌어야함)
	(별도로 하나더?Invoke두번이라 성능문제 있을수도 하나로 묶을시 그거대로 관리 추가삭제 곤란)
	enum string 고민
	코인토스 자동(라오루 빠른진행) 합진행시 코인토스는 클릭한번에 전부 일방공격(합종료후 공격 혹은 방어 회피 무방비 상태 대상공격)
(시에는 클릭한번에 코인 전부(재사용은 확인 필요 아마도 자동으로나올듯))
전투진행중에도 온오프 가능
(bool값 코루틴에서 while문(while(값 == false){마우스클릭등 특정이벤트 발동시 break}))
	에니메이션(코인 하나하나당 나눠서 (코인토스 자동 옵션을 켰을경우에 해당 에니메이션이 연속적으로 나와서 하나의 에니메이션처럼 보임))
	이펙트 에니메이션 도입방법 고민 및 실험 + 셰이더 관련 강의영상 확인하기
	+디버프 구현 (침잠,출혈,파열등등)
	
}
효과(BattleEffect)아이디어
{
	1.코인파괴 대신 디버프 횟수감소
	(코인파괴or합패배(아마도 합패배)일시 발동 bool값혹은 기존 합패배 이벤트를 덮어쓰는 형식(if 횟	수 > 0  true = 디버프 횟수 감소 false = 주사위 파괴)스킬에 Action 통해서 주사위 파괴를 할것(해당 action값을 바꿔치기하는형식))
	
	2.추가적인 디버프 매턴 부여(공격위력감소 방어위력감소 등등  턴시작시에넣거나 (앞서이야기한경우) 각각 디버프에따라 적절히위치 배치)
}
https://arca.live/b/lobotomycoperation/71186923 // 버프디버프 효과정리
BattleEffect // 침잠,화상,합위력증가 등등 버프 디버프 효과
{
	AbstractClass와 Virtual method 를 사용해서 Override를 통해 변경해 적용 
	UnityEvent.EventTrigger 혹은 한곳에 모아서 실행
}

TriggerKey
{
	ClashCoin{int}_Check , ClashCoin{int}_{bool} , ClashCoin_Check , Clash_Same
}
주소지 코드를 받아서 값을수정(ref 를이용하면됨!!!!!)

조건확인및 효과부여

Attack코인 구조체를 받아서 조건을 충족하는지 확인
Attack코인 구조체를 받음,bool값을 반환 혹은 Unityevent를 받아서 실행 , 인스펙터창에서 조작가능

합시작 >Always 효과 적용(일방공격일시 바로 Step2로 합판정값 없음)>!#!사용시 효과 적용>!#!공격시작시 효과 적용> !#!합시작 효과 적용 (둘다 회피,방어,반격이 아닌경우)
 
Step1(코인0개시 바로 Step2,둘다 0 개시 둘다 합판정값 패배) > (속도가더빠른공격 A ,느린공격 B (속도 동일시 플레이어 우선)){A 코인남아있을시 앞뒷면계산
(!#!합코인효과),B코인남아있을시 앞뒷면계산(!#!합코인효과)}
 > !#!합판정? 효과적용(회피시 합위력증가 적용됨..?) >!#!합판정>결과에따라 코인파괴혹은 유지(둘다 회피,방어,반격이 아닌경우) >파괴된코인 !#!코인파괴 효과 적용 >코인갯수 확인 어느한쪽이 0개인가?= 다시Step1으로 : 합승리 패배 효과 적용(둘다합패배시 합패배효과만 받고 끝) 하고 Step2로
 
Step2  > 합승리 혹은 일방공격 하는 대상이 공격 > !#!각각의 코인효과 적용 > !#!공격종료시 효과 적용 

!#!(발동구간) UnityEvent를 통해 적용 name?.Invoke();

UnityEvent<InterfaceName> nameA 조건검사후

코인판정,버프디버프 효과
합승리시 합패배시 코인앞면시 코인뒷면시, 자신혹은적 특정버프디버프 일정량 이상일 경우


버프 디버프 Unity Event 혹은 구조체를 이용하여  합진행시 (매 코인마다) 적용되는 버프디버프 배열에 저장 및 적용
적용되는 시점에 따라 나눠서 저장
매코인마다, 매 합 시작시, 매 합 마지막 계산시 
struct
{
	UnityEvent
}


}
Add

