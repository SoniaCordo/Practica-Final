using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text Time;

    public float InitialTime;

    public delegate void TimeIsOVer();

    public TimeIsOVer OnTimeIsOver;

    private void Start()
    {
        //OnTimeIsOver =
        StartCoroutine(CuentaAtras());
    }

    public IEnumerator CuentaAtras()
    {
        while (InitialTime >= 0)
        {
            yield return null;
            InitialTime -= UnityEngine.Time.deltaTime;
            Time.text = InitialTime.ToString("00.0");
        }
        if (OnTimeIsOver != null)
        {
            OnTimeIsOver();
        }
    }
}