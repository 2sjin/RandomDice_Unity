using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {    
    public int sp;
    public int spCost;
    public int life;
    
    public GameObject spText;
    public GameObject spCostText;
    public GameObject [] heart = new GameObject[3];

    private void Start() {
        this.sp = 100;
        this.spCost = 10;
        this.life = 3;
    }

    private void Update() {
        // Sp, SpCost 텍스트 갱신
        spText.GetComponent<TextMesh>().text = sp.ToString();
        spCostText.GetComponent<TextMesh>().text = spCost.ToString();

        // 하트 이미지 갱신
        switch (life) {
            case 0:
                heart[0].SetActive(false);
                heart[1].SetActive(false);
                heart[2].SetActive(false);
                break;
            case 1:
                heart[0].SetActive(true);
                heart[1].SetActive(false);
                heart[2].SetActive(false);
                break;
            case 2:
                heart[0].SetActive(true);
                heart[1].SetActive(true);
                heart[2].SetActive(false);
                break;
            default:
                heart[0].SetActive(true);
                heart[1].SetActive(true);
                heart[2].SetActive(true);
                break;
        }

        // 게임 오버 체크
        if (life <= 0)
            SceneManager.LoadScene("LobbyScene");
    }
}