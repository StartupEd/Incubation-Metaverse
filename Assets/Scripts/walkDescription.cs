using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class walkDescription : MonoBehaviour
{
    public TMP_Text textComponent;

    [TextArea(3, 10)]
    public string[] fullText;
    public float typingSpeed = 0.07f;
    public bool started;

    public void Update()
    {
        if (gameObject.activeInHierarchy && !started)
        {
            startInfo();
        }
    }

    private void OnEnable()
    {
        setText();
    }

    private void OnDisable()
    {
        started = false;
    }

    public void startInfo()
    {
        StartCoroutine(WriteInfo());
    }

    public void setText()
    {
        fullText[0] = "Hey, Man";
        fullText[1] = "If you are looking for pitch idea room";
        fullText[2] = "Click on map, you will find the exact location of pitch room";
    }

    IEnumerator WriteInfo()
    {
        started = true;
        if (fullText.Length > 1)
        {
            yield return StartCoroutine(TypeText());
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(TypeText1());
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(TypeText2());
            yield return new WaitForSeconds(1.5f);
            //yield return StartCoroutine(TypeText3());
            yield return StartCoroutine(End());
        }
        else
        {
            yield return StartCoroutine(Loadingtxt());
        }
        started = false;
    }
    IEnumerator Loadingtxt()
    {
        while (true)
        {
            textComponent.text = "";
            foreach (char c in fullText[0].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    IEnumerator TypeText()
    {
        textComponent.text = "";
        foreach (char c in fullText[0].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator TypeText1()
    {
        textComponent.text = "";
        foreach (char c in fullText[1].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    IEnumerator TypeText2()
    {
        textComponent.text = "";
        foreach (char c in fullText[2].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator TypeText3()
    {
        textComponent.text = "";
        foreach (char c in fullText[3].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator End()
    {
        textComponent.text = "";
        yield return new WaitForSeconds(2f);
    }
}
