using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PinchButton : OVRGrabbable
{
    [SerializeField] UnityEvent Action;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);

        Action.Invoke();

        GrabEnd(Vector3.zero, Vector3.zero);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(FadeOutAndLoadScene(0.5f, 1));
    }

    IEnumerator FadeOutAndLoadScene(float fadeTime, int sceneIndex)
    {
        OVRScreenFade screenFade = FindObjectOfType<OVRScreenFade>();
        screenFade.fadeTime = fadeTime;
        screenFade.FadeOut();

        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
