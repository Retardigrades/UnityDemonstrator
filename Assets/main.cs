using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    public GameObject LedStringPrefab;
    // Use this for initialization
    bool oneshot ;
    public List<GameObject> Strings;
    public List<GameObject> LEDS;
	void Start () {
        LEDS = new List<GameObject>();
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
            int kk = 0;
            foreach (GameObject ledstring in Strings)
            {

                ledstring.transform.Rotate(0f, 14.4f * kk, 0f);
                kk++;
            }


            for (int ii = 0; ii < Strings.Count; ii++)
            {
                Debug.Log(ii);
                Debug.Log("childs :" + Strings[ii].transform.childCount);

                if (Strings[ii].transform.childCount > 0)
                {
                    if (ii % 2 == 0)
                    {
                        for (int jj = 0; jj < Strings[ii].transform.childCount; jj++)
                        {
                            var parent = Strings[ii].transform;

                            LEDS.Add(parent.GetChild(jj).gameObject);
                        }
                    }
                    else
                    {
                        for (int jj = 0; jj < Strings[ii].transform.childCount; jj++)
                        {
                            var parent = Strings[ii].transform;

                            LEDS.Add(parent.GetChild((parent.childCount - 1) - jj).gameObject);
                        }
                    }
                }
            }

            oneshot = false;
        }
	}
}
