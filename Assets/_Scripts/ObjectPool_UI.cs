using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class ObjectPool_UI : MonoBehaviour
{

    public SphereBehavior[] sp_actors;

    public int objectPool;
    const string display = " Object Pool: {0}";
    private Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        sp_actors = FindObjectsOfType<SphereBehavior>();
        objectPool = sp_actors.Length;
        m_Text.text = string.Format(display, objectPool);
    }
}
