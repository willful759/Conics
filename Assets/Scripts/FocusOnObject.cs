using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnObject : MonoBehaviour
{
    public GameObject defaultFocus;
    GameObject focusedObject;
    // Start is called before the first frame update
    void Start()
    {
       if (defaultFocus == null) {
          focusedObject = new GameObject();
       } else {
          focusedObject = defaultFocus;
          focusedObject.SetActive(true);
       }
    }

    public void SetFocus(GameObject focus) {
        focusedObject.SetActive(false);
        focusedObject = focus;
        focusedObject.SetActive(true);
    }
}
