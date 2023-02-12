using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    int times = 0; 

    public void AddLifeToMaui()
    {
        if(times <= 0)
        {
            StartCoroutine(GameManager.Instance.OpenMiniGamePanel());
            StartCoroutine(AddingHearts());
        }
    }

    IEnumerator AddingHearts()
    {
        GameManager.Instance.AddMauiLife(10);
        times = 1;
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject, 0.25f);
    }
}
