# RandomDice_Unity
Unity로 RandomDice의 Clone 게임 개발하기

## 원작
[랜덤 다이스(Random Dice) : 디펜스](https://play.google.com/store/apps/details?id=com.percent.royaldice)

## 실행 화면
|![image](https://user-images.githubusercontent.com/91407433/183568111-7dc4aa68-effa-439f-b782-8c7827c0c270.png)|
|:-:|

## 데이터베이스 설계
### 요구사항 명세
- 주사위 및 몬스터 등 랜덤다이스 오브젝트를 관리하는 데이터베이스를 구축하고자 한다.
- `주사위`는 고유 ID, 이름, 등급, 이미지번호, 능력치(공격력, 공격속도, 타겟, 특수(3개)) 정보를 갖는다.능력치 관련 모든 속성의 데이터는 1눈금을 기준으로 한다.
- `색상`은 고유 색상명, 색상코드(RGB) 정보를 갖는다. 색상코드의 각 RGB 값은 0~255의 사이의 정수로 제한한다.
- 주사위는 하나의 색상으로 `색칠`된다. 색상은 여러 종류의 주사위를 색칠할 수 있다.
- `몬스터`는 고유 ID, 이름, 보스판정, 이동속도 정보를 갖는다.

### 개체 및 속성 추출
|개체|속성|
|:-:|:-:|
|주사위|ID, 이름, 등급, 이미지번호, 공격력, 공격속도, 타겟, 특수0, 특수1, 특수2|
|색상|색상명, Red, Green, Blue|
|몬스터|ID, 이름, 보스판정, 이동속도|

### 관계 추출
|*관계*|참여 개체|관계 유형|속성|
|:-:|:-:|:-:|:-:|
|색칠|주사위, 색상|1:N|-|

### E-R 다이어그램
![image](https://user-images.githubusercontent.com/91407433/183568922-fe36a4f2-1cef-412c-9fd6-7806c9d357fd.png)

### 릴레이션 스키마
( 범주: **기본키** *외래키* )

- 주사위 릴레이션(**ID**, 이름, 등급, 이미지번호, 공격력, 공격속도, 타겟, 특수0, 특수1, 특수2, *색상명*)
- 색상 릴레이션(**색상명**, Red, Green, Blue)
- 몬스터 릴레이션(**ID**, 이름, 보스판정, 이동속도)

### 테이블
|테이블명|테이블|비고|
|:-:|:-:|:-:|
|dice|![image](https://user-images.githubusercontent.com/91407433/183569834-a7791381-39c7-4379-9bcd-798bb36e0251.png)|주사위 개체|
|color|![image](https://user-images.githubusercontent.com/91407433/183569894-342caa9e-869f-4db6-b47a-6a33a798062b.png)|색상 개체|
|monster|![image](https://user-images.githubusercontent.com/91407433/183569974-9d6ed179-5a5d-414e-b083-bf6e9c0dc7b9.png)|몬스터 개체|

### SQL(PHP 스크립트)
#### select_dice.php
특정한 ID를 가진 주사위의 모든 필드를 조회한다.
```
<?php

// POST로 전송받을 데이터
$id_field = $_POST['dice_id_field'];

// DBMS 연결을 위한 정보
$hostname = "127.0.0.1";        // 호스트 이름
$username = "admin";            // 사용자 이름
$password = "1111";             // 비밀번호
$database = "random_dice";             // 사용할 데이터베이스

// DBMS 연결
$conn = mysqli_connect($hostname, $username, $password, $database);

// SQL
$sql = "SELECT * FROM dice JOIN color ON dice.color = color.color_name WHERE di>

// SQL 처리
$result = mysqli_query($conn, $sql);
```

#### select_monster.php
특정한 ID를 가진 몬스터의 모든 필드를 조회한다.
```
<?php

// POST로 전송받을 데이터
$id_field = $_POST['monster_id_field'];

// DBMS 연결을 위한 정보
$hostname = "127.0.0.1";        // 호스트 이름
$username = "admin";            // 사용자 이름
$password = "1111";             // 비밀번호
$database = "random_dice";             // 사용할 데이터베이스

// DBMS 연결
$conn = mysqli_connect($hostname, $username, $password, $database);

// SQL
$sql = "SELECT * FROM monster WHERE monster_id=".$id_field.";";

// SQL 처리
$result = mysqli_query($conn, $sql);
```
