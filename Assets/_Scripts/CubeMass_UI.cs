using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class CubeMass_UI : MonoBehaviour
{
    //public CubeBehaviour actors;
    public int mass;
    const string display = " Cube mass: {0}";
    private Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        mass = 12;

        m_Text.text = string.Format(display, mass);
    }
}
