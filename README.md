# Learn_Unity_03

## 24.04.09
### 유니티 3 학습목표
 지난 학습 때 Player, Level, SceneManager, SoundManager의 스크리브를 각각 생성해서 직접 기능을 만들어보았다. 
 이번 학습에는 공통적인 기능을 가진 클래스를 생성해서 상속해보는 방법을 사용해본다.

#### 포트폴리오 준비
1. 장르를 정한다
2. 플랫폼을 정한다(어떤 해상도로 개발)
3. 게임의 기능을 개발 : 방해요인, 목표, UI

#### ScriptableObject
게임의 데이터를 보관해서 사용하기 위해 유니티가 제공해주는 클래스
- 대량의 데이터를 사용하기 위해서
    - 몬스터를 프리팹으로 하여 100마리 인스턴스화 했을때 Monster - HP, Attack, range... 가 반복된다.
    이 때 프로젝트에서 데이터를 저장하고 이 데이터를 참조해서 사용한다.

## 24.04.10
* NavMesh : AI가 길을 찾기 위한 길을 메쉬로 표현한것(bake)
* Nav Mesh Agent : 경로를 탐색하고, 장애물을 회피하고 다른 Agent와 충돌앟는 드으이 길찾기 AI객체
* Nav Mesh Obstacle : Agent가 이동하지 못하게 장애물을 설치한다.
* Nav Mesh link : NavMesh 끊어진 길이 있을때 둘 사이를 연결시켜주는 컴포넌트

## 24.04.11
Finitie State Machine(상태머신)
