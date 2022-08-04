using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {    
    public int sp;
    public int spCost;
    public int heart;
    
    public GameObject spText;
    public GameObject spCostText;

    private void Start() {
        this.sp = 100;
        this.spCost = 10;
        this.heart = 3;
    }

    private void Update() {
        spText.GetComponent<TextMesh>().text = sp.ToString();
        spCostText.GetComponent<TextMesh>().text = spCost.ToString();
        
        // 게임 오버 체크
        if (heart <= 0)
            SceneManager.LoadScene("LobbyScene");
    }
}