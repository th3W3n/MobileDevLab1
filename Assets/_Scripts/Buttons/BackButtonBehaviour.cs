using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonBehaviour : MonoBehaviour
{
    public void OnBackButtonClick()
    {
        SoundManager.Instance.PlaySfx(AudioName.BUTTON);
        SceneManager.LoadScene("Start");
    }
}