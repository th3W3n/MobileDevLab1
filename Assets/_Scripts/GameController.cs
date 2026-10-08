using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text livesLabel;
    [SerializeField] TMP_Text screenLabel;

    private float scoreLabelHalfH, scoreLabelHalfW;
    private float livesLabelHalfH, livesLabelHalfW;

    private void Start()
    {
        scoreLabelHalfH = scoreLabel.rectTransform.rect.height * 0.5f;
        scoreLabelHalfW = scoreLabel.rectTransform.rect.width * 0.5f;
        livesLabelHalfH = livesLabel.rectTransform.rect.height * 0.5f;
        livesLabelHalfW = livesLabel.rectTransform.rect.width * 0.5f;
    }
    private void Update()
    {
        scoreLabel.rectTransform.position = new Vector2
        (Screen.safeArea.xMax - scoreLabelHalfW, Screen.safeArea.yMax - scoreLabelHalfH);
        livesLabel.rectTransform.position = new Vector2
        (Screen.safeArea.xMin + livesLabelHalfW, Screen.safeArea.yMax - livesLabelHalfH);

        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
                screenLabel.text = "LandscapeLeft"; break;
            case ScreenOrientation.LandscapeRight:
                screenLabel.text = "LandscapeRight"; break;
            case ScreenOrientation.Portrait:
                screenLabel.text = "Portrait"; break;
            case ScreenOrientation.PortraitUpsideDown:
                screenLabel.text = "PortraitUpsideDown"; break;
            default:
                screenLabel.text = "Unknown"; break;
        }
    }
}