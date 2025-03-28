using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    public int gemCount;
    public TextMeshProUGUI gemText;

    private void Update()
    {
        gemText.text = gemCount.ToString();
    }
}
