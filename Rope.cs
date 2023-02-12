using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    int times = 0;

    public void AddRopeToMaui()
    {
        if (times <= 0)
        {
            StartCoroutine(GameManager.Instance.OpenMiniGamePanel());
            StartCoroutine(AddingRopes());
        }
    }

    IEnumerator AddingRopes()
    {
        GameManager.Instance.AddRopesMaui();
        times = 1;
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject, 0.25f);
    }
}
