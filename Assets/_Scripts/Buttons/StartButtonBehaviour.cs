using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonBehaviour : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SoundManager.Instance.PlaySfx(AudioName.BUTTON);
        SceneManager.LoadScene("Play");
    }
}