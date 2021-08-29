using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    private Object[] props;
    private string[] propNames;

    private void Start() {
        //Load all the prefabs contained in Assets/Resources/Prefabs
        props = Resources.LoadAll("Prefabs", typeof(GameObject));
    }
}
