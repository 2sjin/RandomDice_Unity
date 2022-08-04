using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour {

    private void OnMouseUp() {
        SceneManager.LoadScene("LobbyScene");
    }
}