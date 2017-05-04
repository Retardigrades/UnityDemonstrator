using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    public GameObject LedStringPrefab;
   
    bool oneshot ;
    public List<GameObject> Strings;
    public List<GameObject> LEDS;
    public List<Color> colors;
    // Use this for initialization
    void Start () {
        LEDS = new List<GameObject>();
        Strings = new List<GameObject>();
        colors = new List<Color>();
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

        #region rotate LedStrings to right position
        if (oneshot)
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
        #endregion

    }

    void SetColors(List<GameObject> myLeds)
    {
        if (myLeds.Count != colors.Count) return;
        for(int ii = 0; ii < colors.Count; ii++)
        {
            myLeds[ii].GetComponent<Material>().color = colors[ii];
        }

    }
    void GetColors(List<GameObject> myLeds)
    {
        foreach(GameObject led in myLeds)
        {
            var col = led.GetComponent<Material>().color;
            colors.Add(col);
        }
    }
}
