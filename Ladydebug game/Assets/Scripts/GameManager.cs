using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject YouDiedPanel;
    [SerializeField] private GameObject TutorialPanel;

    private void Start()
    {
        StartCoroutine(Tutorial(5));
    }
    public void YouDiedDisplay()
    {
        YouDiedPanel.SetActive(true);
    }

    IEnumerator Tutorial(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideTutorial();
    }

    public void HideTutorial()
    {
        TutorialPanel.SetActive(false);
    }
    private void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }
}
