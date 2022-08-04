using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DiceDatabaseConnector : MonoBehaviour {
    private string selectURL = "http://152.70.94.65/random_dice/select_dice.php";   // PHP 스크립트

    // 데이터베이스에서 주사위 정보 가져오기(SELECT)
    public void getDiceInfoFromDatabase(int diceId, int deckIndex) {
        StartCoroutine(selectDiceInfo(diceId, deckIndex));
    }

    // 데이터베이스에서 주사위 정보 가져오기(SELECT)
    IEnumerator selectDiceInfo(int diceId, int deckIndex) {
        // 데이터 POST 전송
        WWWForm form = new WWWForm();
        form.AddField("id_field", diceId.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post(selectURL, form);
        yield return webRequest.SendWebRequest();

        // 메시지 출력
        if (webRequest.error != null)
            Debug.Log(webRequest.error);
        else {
            string dataText = webRequest.downloadHandler.text;
            DiceManager.diceDataText[deckIndex] = dataText;     // DiceManager로 텍스트 전달
        }
    }
}