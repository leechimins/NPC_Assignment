using System.IO; // 🌟 파일 읽고 쓰기(File 클래스)를 위해 맨 위에 반드시 추가!
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    //유니티 인스펙터 창에서 숫자를 조절해볼 수 있도록 public으로 선언합니다.
    public int playerLevel = 1;    
    // 위치를 저장하고 복구할 2D 플레이어 오브젝트
    public GameObject player;
    // 파일이 저장될 컴퓨터 메모리 경로를 담을 변수
    private string savePath;

    void Start()
    {
        // 유니티가 자동으로 제공하는 안전한 경로에 "savefile.json"이라는 파일명을 합칩니다.
        savePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadGame();
        }
    }

    
    public void SaveGame()
    {
        // [실습 1] PlayerPrefs 저장 함수

        // "CurrentLevel"이라는 이름의 사물함(Key)을 파서 playerLevel 값(Value)을 저장합니다.
        PlayerPrefs.SetInt("CurrentLevel", playerLevel);

        // 중요! 사물함에 넣은 데이터를 하드디스크에 완전히 잠금 장치(저장)를 합니다.
        PlayerPrefs.Save();

        Debug.Log($"[PlayerPrefs] 저장 완료! 현재 레벨: {playerLevel}");


        // [실습 2] JSON 세이브 함수

        // 1. 텅 빈 데이터 상자(SaveData)를 하나 새로 조립합니다.
        SaveData data = new SaveData();

        // 2. 상자 안에 저장할 게임 데이터들을 차곡차곡 채워 넣습니다.
        data.playerLevel = 25;
        data.playerName = "도트용사";
        data.x = player.transform.position.x; // 플레이어의 현재 X 좌표
        data.y = player.transform.position.y; // 플레이어의 현재 Y 좌표

        // 3. 상자(객체)를 하나의 긴 텍스트(JSON 문자열)로 변환합니다.
        // true를 넣으면 메모장으로 열었을 때 줄바꿈이 예쁘게 디자인됩니다.
        string jsonText = JsonUtility.ToJson(data, true);

        // 4. 변환된 텍스트를 설정해둔 파일 경로에 실제로 저장(저장)합니다.
        File.WriteAllText(savePath, jsonText);

        Debug.Log($"📂 [JSON] 저장 완료! 경로: {savePath}");
    }

    public void LoadGame()
    {
        // [실습 1] PlayerPrefs 불러오기 함수

        // "CurrentLevel"이라는 이름의 사물함이 진짜 존재하는지 먼저 확인합니다.
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            // 사물함에서 값을 꺼내와서 우리 게임 변수에 대입합니다.
            playerLevel = PlayerPrefs.GetInt("CurrentLevel");
            Debug.Log($"[PlayerPrefs] 불러오기 완료! 레벨: {playerLevel}");
        }
        else
        {
            Debug.LogWarning("❌ 저장된 데이터가 없습니다.");
        }


        // [실습 2] JSON 로드 함수

        // 1. 하드디스크에 세이브 파일이 진짜 존재하는지 먼저 체크합니다.
        if (File.Exists(savePath))
        {
            // 2. 파일 안에 적힌 텍스트를 통째로 읽어옵니다.
            string jsonText = File.ReadAllText(savePath);

            // 3. 읽어온 텍스트를 다시 데이터 상자(SaveData) 형태로 해체 및 역조립합니다.
            SaveData data = JsonUtility.FromJson<SaveData>(jsonText);

            // 4. 상자에서 꺼낸 데이터를 바탕으로 2D 플레이어의 실제 위치를 이동시킵니다.
            player.transform.position = new Vector3(data.x, data.y, 0f); // 2D이므로 Z는 0

            Debug.Log($"🎮 [JSON] 로드 완료! 이름: {data.playerName}, 위치 복구 성공!");
        }
        else
        {
            Debug.LogWarning("❌ 세이브 파일이 존재하지 않습니다.");
        }
    }
}

// 🌟 꼭 적어주어야 유니티가 이 상자를 텍스트(JSON)로 변환할 수 있습니다!
[System.Serializable]
public class SaveData
{
    public int playerLevel;
    public string playerName;
    // 2D 게임이므로 X와 Y 좌표만 저장해도 충분합니다!
    public float x;
    public float y;
}