# ForgeCraft

평화로운 왁타버스성, 부활한 마왕에 의해 몬스터 웨이브가 오고 있다.<br>
성주 비즈니스킴은 몬스터에 의한 피해를 막기 위해 대장장이 집안이던 우왁굳에게 무기 제작을 부탁한다.<br>
우왁굳은 할아버지의 고물 대장간을 수리하며 마왕을 물리칠 무기들을 만들기 시작하는데..<br>

## 🖥️ 프로젝트 소개
#### 스파르타코딩클럽 내일배움캠프 - 최종 프로젝트<br> 
Unity를 활용한 2D 수집형 타이쿤 게임 <br>
탐험대와 함께 던전을 탐험하며 무기를 제작하고 제작 의뢰를 수행하며 대장간을 경영하는 게임
<br>

## 🕰️ 개발 기간 
* 23.10.23 (월) - 23.12.15 (금)

## 🧑‍🤝‍🧑 팀원 구성
* 김예준 (팀장) - UI, 전투 / https://github.com/kyj0701
* 박준규 - 전투 / https://github.com/Park-Junkyu
* 김명식 - UI, 디자인 / https://github.com/d0ryeon
* 이형빈 - 대장간(제작) / https://github.com/lightlight98
* 김도현 - 대장간(의뢰) / https://github.com/dohyeon9483

### ⚙️ Development Environment
- **Language** : C#
- **Engine** : Unity 2022.3.2f1
- **IDE** : Visual Studio 2022
- **Framework** : .NET 6.0

### 📜 Assets References
- [The DARK Series](https://penusbmic.itch.io/)
- [MODERN PIXEL ART FX PACK](https://krispixel.itch.io/modern-fx-pack)
- [Pixel Monsters](https://rvros.itch.io/)
- [Pixel Skeletons](https://evgeniy-luch.itch.io/animated-pixel-skeletons)
- [Pixel Elves](https://dreamir.itch.io/elves-pack)
- [Pixel Wolf](https://sanctumpixel.itch.io/wolf-pixel-art-character)
- [Pixel Weapons](https://dantepixels.itch.io/weapons-asset-16x16)
- [Pixel Items](https://beast-pixels.itch.io/crafting-materials)
- [GUI Pro](https://assetstore.unity.com/packages/2d/gui/gui-pro-casual-game-176695)
- [Skill Icon](https://assetstore.unity.com/packages/2d/gui/icons/2d-skills-icon-set-handpainted-210622)
- [Gotcha Box](https://assetstore.unity.com/packages/2d/environments/pixel-chests-pack-animated-263923)

## 📌 주요 기능

### 대장간
![Casting](https://github.com/UnityFourge/ForgeCraft_Scripts/assets/31722243/919fcc99-691f-49a7-a602-998300974455)
![Forging](https://github.com/UnityFourge/ForgeCraft_Scripts/assets/31722243/35077f5a-1447-4899-987e-5b518eb7105f)


### 전투
![Battle](https://github.com/UnityFourge/ForgeCraft_Scripts/assets/31722243/a8c30cb9-64e1-417f-93a2-54d79bf173f2)


## 📌 주요 기술

### UI 자동화
* 많은 팝업과 여러 씬을 사용할 예정이기 때문에 UI를 효율적으로 관리할 수 있는 시스템이 필요.
* 씬 단위, 팝업 단위로 스크립트를 만들고 Prefab으로 관리하고 적재적소에 Prefab을 활용.
* 팝업 Prefab이 생성되는 것을 스택으로 관리하여 팝업의 Sort Order가 섞이지 않도록 구현.

### Singleton
* 여러가지 필드와 자료들을 중복해서 생성하지 않고 여러 스크립트에서 공유하기위해 정적 객체를 활용.
* 씬이 전환될 때에도 유지되어야 할 정보를 가진 정적 객체는 DontDestroyOnLoad를 활용하여 객체가 파괴되는 것을 방지.

### Scriptable Object
* 캐릭터, 적, 아이템 등 다양한 요소의 정보를 관리하기 위해 활용.
* 기본이 되는 정보들을 중복해서 생성하지 않기 위해 ScriptableObject로 관리.
* 중복해서 생성하지 않기 때문에 메모리 상의 이점.

### Json Utility
* 저장하기/불러오기에 사용될 데이터를 Json으로 관리.
* 클래스와 배열 등, 직렬화가 가능한 여러 데이터를 관리.

### Coroutine Helper
* 코루틴 활용 시, 서브 루틴의 지연을 주기 위해 new WaitForSeconds()를 통해 WaitForSeconds를 생성.
* 생성된 WaitForSeconds는 결국 가비지를 발생시키기 때문에 서브 루틴이 실행될 때마다 가비지가 발생.
* 가지비 컬렉터가 호출되어 성능 저하를 유발.
* 시간(float)을 key, WaitForSeconds를 value로 갖는 딕셔너리에 저장하고 불러오면서 사용함으로써 가비지의 발생을 줄여 성능 저하를 방지.

### FSM
* 캐릭터와 적의 동작을 FSM을 활용하여 구현.
* 단순히 bool 변수로 동작의 변화를 확인하기보다는 상태가 변화하는 것에 따라 동작을 만들어주는 것이 다양한 상태와 동작에 대응하기 효율적이라 판단.
* 상태가 변화함에 따라 애니메이터의 트리거를 변화시켜 동작에 맞는 애니메이션을 재생.

### Delegate
* 팝업을 외부에서 제어할 수 있게 하여, 팝업의 특정 동작을 분리하여 팝업을 유연하게 사용할 수 있도록 제작.
* 코드의 가독성을 향상시키고 유지보수가 편리하도록 구현.

### Bézier curve
* 투사체를 구현하는 데에 베지에 곡선을 활용.

### 가중치
* 캐릭터 뽑기를 구현할 때, 캐릭터의 등급이 나뉘어져 있어 등급별로 뽑히는 확률이 달라야하는 상황.
* 등급마다 가중치를 부여하여 높은 등급일수록 적은 확률로 뽑히도록 구현.
* 몬스터가 여러 개의 스킬을 사용할 때, 가중치를 적용해서 다양한 패턴으로 스킬을 사용하도록 구현.

### Niji-journey (AI)
* AI를 활용해서 게임 분위기에 맞는 다양한 이미지 소스 추출.
* 게임에 활용할 수 있도록 리터칭.

### SPUM
* 다양한 픽셀 캐릭터가 필요.
* 기본 이미지만 있으면 애니메이션까지 구현해주는 SPUM 애셋 활용.


## 📌 링크 

### 팀 노션 페이지
https://teamsparta.notion.site/Fourge-da16c06db3ba48e5bd5d2065af1730d1

### 팀 피그마 페이지
https://www.figma.com/file/o8HRYDYF4F5t6k1sW918dE/ForgeCraft?type=whiteboard&node-id=0-1&t=ODYpyiAG9hgrzYck-0
