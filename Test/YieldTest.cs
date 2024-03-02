using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldTest : MonoBehaviour
{
    public IEnumerable<int> GetNumbers()
    {
        yield return 1;
        yield return 2;
        yield return 3;
    }

}
