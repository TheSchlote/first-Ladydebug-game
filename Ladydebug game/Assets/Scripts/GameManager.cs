using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject YouDiedPanel;
    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private AudioSource highFlyingMusic;
    [SerializeField] private AudioSource DeathSound;
    [SerializeField] private AudioSource CoffeeSound;
    [SerializeField] private AudioSource BugSound;

    private void Start()
    {
        highFlyingMusic.Play();
        StartCoroutine(Tutorial(5));
    }
    public void YouDiedDisplay()
    {
        DeathSound.Play();
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

    public void PlayCoffeeSound()
    {
        CoffeeSound.Play();
    }
    public void PlayBugSound()
    {
        BugSound.Play();
    }
}
