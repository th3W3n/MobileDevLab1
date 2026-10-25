using UnityEngine;
using UnityEngine.SceneManagement;

public class InsButtonBehaviour : MonoBehaviour
{
    public void OnInsButtonClick()
    {
        SoundManager.Instance.PlaySfx(AudioName.BUTTON);
        SceneManager.LoadScene("Instruction");
    }
}
