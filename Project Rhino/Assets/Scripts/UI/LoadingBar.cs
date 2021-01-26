﻿using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] RectTransform[] ticks;
    [SerializeField] float tickInactiveHeight = 10f, tickActiveHeight = 30f;

    void InitBar()
    {
        ticks = BarTicks();
    }

    RectTransform[] BarTicks()
    {
        RectTransform[] children = new RectTransform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
        return children;
    }

    public void ResetBar()
    {
        for (int i = 0; i < ticks.Length; i++)
        {
            ticks[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tickInactiveHeight);
        }
    }

    public void UpdateBar(float _percentage)
    {
        int ticksTotal = ticks.Length;
        int activeTicks = Mathf.RoundToInt(_percentage * ticksTotal);
        Debug.Log(_percentage);
        for(int i = 0; i < ticksTotal; i++)
        {
            if(i + 1 > activeTicks)
            {

                ticks[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tickInactiveHeight);
            }
            else
            {
                ticks[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tickActiveHeight);
            }
        }
    }
}