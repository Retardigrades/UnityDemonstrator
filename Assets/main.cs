using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    public GameObject LedStringPrefab;
    // Use this for initialization
    bool oneshot ;
    public List<GameObject> Strings;
	void Start () {
        Strings = new List<GameObject>();
		if (LedStringPrefab != null)
        {
            for(int ii =0; ii <25; ii++)
            {
                
                var ledstring = Instantiate(LedStringPrefab);
                Strings.Add(ledstring);
                
            }
        }
        oneshot = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(oneshot)
        {
            int ii = 0;
            foreach (GameObject ledstring in Strings)
            {

                ledstring.transform.Rotate(0f, 14.4f * ii, 0f);
                ii++;
            }
            oneshot = false;
        }
	}
}
