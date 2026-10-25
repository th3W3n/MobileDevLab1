using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButtonBehaviour : MonoBehaviour
{
    public void OnResetButtonClick()
    {
        SoundManager.Instance.PlaySfx(AudioName.BUTTON);
        SceneManager.LoadScene("Play");
    }
}