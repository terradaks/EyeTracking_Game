using UnityEngine;
using System.Collections;

public class SceneReadyManager : MonoBehaviour
{
    public static bool IsReady { get; private set; }

    void Awake()
    {
        IsReady = false;
    }

    IEnumerator Start()
    {
        yield return null;
        yield return new WaitForSeconds(0.5f);

        IsReady = true;
    }
}