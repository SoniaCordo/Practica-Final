using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text Time;

    public float InicialTime;

    public delegate void TimeIsOVer();

    public TimeIsOVer OnTimeIsOver;

    private void Start()
    {
        StartCoroutine(CuentaAtras());
    }

    public IEnumerator CuentaAtras()
    {
        while (InicialTime >= 0)
        {
            yield return null;
            InicialTime -= UnityEngine.Time.deltaTime;
            Time.text = InicialTime.ToString("00.0");
        }
        if (OnTimeIsOver != null)
        {
            OnTimeIsOver();
        }
    }
}