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
            //vom Jan :)
            for (int ii = 0; ii < Strings.Count; ii++)
            {
                var off = (ii / 5) % 2;
                for (int jj = 0; jj < Strings[ii].transform.childCount; jj++)
                {
                    var parent = Strings[ii].transform;
                    if ((ii + off) % 2 == 0)
                    {
                        LEDS.Add(parent.GetChild(jj).gameObject);
                    }
                    else
                    {
                        LEDS.Add(parent.GetChild((parent.childCount - 1) - jj).gameObject);
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
        if(colors.Count == 250)
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
        if (myLeds.Count != colors.Count/2)
        {
            errortext.text = "Less than 250 colors defined. wont Set!";
            return;
        }
        else errortext.text = "";
        for (int ii = 0; ii < colors.Count; ii++)
        {
            myLeds[ii].GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor",colors[ii]);
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
