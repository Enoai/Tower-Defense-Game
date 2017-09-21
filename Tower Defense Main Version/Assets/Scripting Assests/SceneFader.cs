using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//fades the scene from one to another
public class SceneFader : MonoBehaviour {

    public Image img;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn()); // begins to coruntine.
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene)); // method to which can be called to lolad a scene
    }

    //fades in
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            // controls the curve / fade in.
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // skip to the next frame
        }

        // 

    }

    // fades out of the current scene/options
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            // controls the curve / fade out.
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // skip to the next frame
        }

        // 
        SceneManager.LoadScene(scene);

    }
}
