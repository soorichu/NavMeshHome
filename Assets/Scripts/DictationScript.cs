using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


public class DictationScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] stuffs;

//    [SerializeField]
//    private Text m_Hypotheses;
    [SerializeField]
    private Text m_Recognitions;

    DictationRecognizer m_DictationRecognizer;

    //   GameObject target;
    GameObject older;

    void Start()
    {
        stuffs = FindObjectsOfType<GameObject>();
        m_DictationRecognizer = new DictationRecognizer();
        older = GameObject.FindWithTag("older");

        // Result text. it maybe word or sentence.
        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text = text;
            Action(text);
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
//            m_Hypotheses.text += text;
        };

 //       m_DictationRecognizer.DictationComplete += (completionCause) =>
 //       {
 //           if (completionCause != DictationCompletionCause.Complete)
 //           {
 //               Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
 //           }
 //       };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}. HResult = {1}", error, hresult);

        };

        m_DictationRecognizer.Start();
    }

    void Action(string text)
    {
        if (text.Contains("agent"))
        {
            LetsMove.instance.seeking = false;
        }

        if (LetsMove.instance.seeking) return;

        // Find GameObject(target) by Text
        foreach(GameObject stuff in stuffs)
        {
            string stuffName = stuff.name.ToLower();
            if (text.Contains(stuffName))
            {
                TargetManager.targetInstance.target = stuff;
                Debug.Log("Find Target: " + TargetManager.targetInstance.target.name);

                // Bring Stuff to Older
                if (text.Contains("bring"))
                {
                    LetsMove.instance.FindTarget(TargetManager.targetInstance.target);
                    StartCoroutine(BackOlder(4));

                }

                // Take Older to Spot
                else if (text.Contains("take"))
                {
                    LetsMove.instance.TakeOlderToTarget(TargetManager.targetInstance.target);
                }

                // Just Find Stuff
                else
                {
                    LetsMove.instance.FindTarget(TargetManager.targetInstance.target);
                }
            }
        }

    }

    IEnumerator BackOlder(float sec)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        TargetManager.targetInstance.target.SetActive(false);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(sec);
        TargetManager.targetInstance.target = older;
        LetsMove.instance.FindTarget(TargetManager.targetInstance.target);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
