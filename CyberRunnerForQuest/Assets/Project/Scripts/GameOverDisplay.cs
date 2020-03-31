using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] char countInsert = '*';
    [SerializeField] TextMeshProUGUI diffusedText;

    void Start()
    {
        string[] textLines = diffusedText.text.Split(countInsert);

        string fullText = "";

        for (int i = 0; i < textLines.Length-1; i++)
        {
            fullText += textLines[i] + GameManager.Instance.diffusedBombs.ToString();
        }

        fullText += textLines[textLines.Length - 1];

        diffusedText.text = fullText;

        StartCoroutine(ReloadScene(5,2));
    }

    IEnumerator ReloadScene(float startDelay,float fadeTime)
    {
        yield return new WaitForSeconds(startDelay);

        OVRScreenFade screenFade = FindObjectOfType<OVRScreenFade>();
        screenFade.fadeTime = fadeTime;
        screenFade.FadeOut();

        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(1);
    }
}
