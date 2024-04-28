using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCNameTag : MonoBehaviour
{
    private string nameTag;
    private TMP_Text nameTagText;
    // Start is called before the first frame update
    void Start()
    {
        nameTag = this.name;
        nameTagText = GetComponentInChildren<TMP_Text>();
        if(nameTagText != null)
        {
            nameTagText.text = nameTag;
        }
    }
}
