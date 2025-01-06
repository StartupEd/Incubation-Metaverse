using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Aud_Class
{
    public class Aud_TypeWriter : MonoBehaviour
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
            fullText[0] = "Hey, Nice to meet you";
            fullText[1] = "If you want pitch an idea.Then search pitch idea room in building in city. And you can also collect rewards coins in whole city";
            fullText[2] = "Thank you, bye";
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
}
