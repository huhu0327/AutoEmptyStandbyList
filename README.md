<div align="center">
<img src="https://upload.wikimedia.org/wikipedia/commons/c/c2/GitHub_Invertocat_Logo.svg" alt="대표 이미지" width="340"  height="100" />
<br/ >
<br/ >

# AutoEmptyStandbyList
윈도우 메모리 대기모드 청소 툴입니다.   
EmptyStandbyList.exe를 작업 스케줄러에 등록하여 주기적으로 실행합니다.
</div>

## 사용 방법
1. 원하는 시간 주기 설정
> 최소: 1분

2. 적용

## 기능
- [x] 작업 스케쥴러 자동 등록
- [x] Hide Window Background 실행 (윈도우 터미널 깜빡이 방지)

## 스크린샷
![image](https://github.com/huhu0327/AutoEmptyStandbyList/assets/28612967/d7c4ad5b-8793-4029-bf89-d6fa17406b5e)

## 개발 환경
|       |       이름        | 버전        |
|:------|:---------------:|-----------|
| IDE   | JetBrains Rider | 2023.3    |
| 언어    |    C#(.NET)     | 12.0(8.0) |
| 프레임워크 |    Avalonia     | 11.0.7       |

## 라이브러리 (Client)

|  이름  | 버전   |
|:----:|------|
|  Avalonia.Desktop  | 11.0.7 |
|  Avalonia.Themes.Fluent  | 11.0.7 |
|  Avalonia.Fonts.Inter  | 11.0.7 |
|  CommunityToolkit.Mvvm  | 8.2.2 |
|  MessageBox.Avalonia  | 3.1.5.1 |
|  Microsoft.Extensions.DependencyInjection  | 8.0.0 |

## 라이브러리 (Shared)

|  이름  | 버전   |
|:----:|------|
|  Salaros.ConfigParser  | 0.3.8 |
|  TaskScheduler  | 2.10.1 |

## 개선
- [ ] Program Icon
