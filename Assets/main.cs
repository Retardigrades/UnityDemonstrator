using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    public GameObject LedStringPrefab;
    public Camera Main, Top, Front;
    public UnityEngine.UI.Text errortext;
    public UnityEngine.UI.InputField portfield;
    bool oneshot ;
    public List<GameObject> Strings;
    public List<GameObject> LEDS;
    public List<Color> colors;
    public socketinput socketin;

    // Use this for initialization
    void Start () {
        Main.gameObject.SetActive(false);
        Top.gameObject.SetActive(false);
        Front.gameObject.SetActive(true);
        //socketin = Instantiate(new socketinput());
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

        if(Input.GetKeyDown(KeyCode.F1))
        {
            Main.gameObject.SetActive(false);
            Top.gameObject.SetActive(false);
            Front.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Main.gameObject.SetActive(false);
            Top.gameObject.SetActive(true);
            Front.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Main.gameObject.SetActive(true);
            Top.gameObject.SetActive(false);
            Front.gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        errortext.text = socketin.debug;
        colors = socketin.DataOut;
        if(colors.Count > 0)
        {
            SetColors(LEDS);
        }

    }

    public void Setport()
    {
        socketin.Port = System.Convert.ToInt32(portfield.text);
        socketin.ListenToPort();
    }

    void SetColors(List<GameObject> myLeds)
    {
        if (myLeds.Count != colors.Count)
        {
            errortext.text = "Less than 500 colors defined. wont Set!";
            return;
        }
        else errortext.text = "";
        for (int ii = 0; ii < colors.Count; ii++)
        {
            myLeds[ii].GetComponent<Material>().color = colors[ii];
        }

    }
   public  void GetColors(List<GameObject> myLeds)
    {
        foreach(GameObject led in myLeds)
        {
            var col = led.GetComponent<Material>().color;
            colors.Add(col);
        }
    }
}
