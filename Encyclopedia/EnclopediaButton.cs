 
using UnityEngine;
using UnityEngine.UI;

public class EnclopediaButton : MonoBehaviour
{
    [SerializeField] private Sprite questionMark;
    private string spriteName;
    
    private Button b;
    

    public void setSprite(Sprite s)
    {
        spriteName = s.name;
        b = GetComponent<Button>();
        
        if (Encyclopedia.Instance.HasBeenSeen(spriteName))
        {
            b.image.sprite = s;
        }
        else
        {
            b.image.sprite = questionMark;
        }
    }

    public void OnButtonclick()
    {
        Encyclopedia.Instance.OnEncyclopediaButtonClick(spriteName);

    }
}
