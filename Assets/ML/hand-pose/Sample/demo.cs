using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatSuite.ML;
public class demo : MonoBehaviour
{
    // Start is called before the first frame update
    public MLModelData modelData;
    void Start()
    {
        using var model = modelData.Deserialize();
        Debug.Log(model);
    }

    // Update is called once per frame
}
