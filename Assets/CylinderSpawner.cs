using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSpawner : MonoBehaviour
{
    public GameObject LEDPrefab;
    public float radiusTop = 0.03f;
    public float radiusBottom = 0.75f;

    public List<Vector3> positions;

    void Start()
    {
        positions = new List<Vector3>();
        for(int ii = 0; ii <10; ii ++)
        { var trns = transform.position;

            if (ii == 0)
            {
                var pos = new Vector3(trns.x + radiusTop, trns.y, trns.z);
                positions.Add(pos);
            }
            else if (ii < 2)
            {
                var pos = new Vector3(trns.x + radiusTop + (0.075f * (ii)), trns.y - (0.055f * (ii)), trns.z);
                positions.Add(pos);
            }
            else if (ii < 7)
            {
                var pos = new Vector3(trns.x + radiusTop + (0.075f * (ii)), trns.y - (0.065f * (ii)), trns.z);
                positions.Add(pos);
            }
            else
            {
                var pos = new Vector3(trns.x + radiusTop + (0.075f * (ii)), trns.y - (0.06f * (ii)), trns.z);
                positions.Add(pos);
            }

        }
        for (int ii = 0; ii < 10; ii++)
        {
            const float xadd = 0.075f * 9f + 0.0325f;
            const float ysub = 0.065f * 9.0f + 0.048f;
            var trns = transform.position;
            var pos = new Vector3(trns.x + radiusTop + xadd, trns.y-ysub-0.06f*ii, trns.z);
                positions.Add(pos);
            

        }

        int num = 0;
        foreach(Vector3 pos in positions)
        {
            if (LEDPrefab != null)
            {
                var led = Instantiate(LEDPrefab, pos, transform.rotation);
                led.name = num.ToString();
                led.transform.SetParent(transform);
                num++;
            }
        }
    }

}
