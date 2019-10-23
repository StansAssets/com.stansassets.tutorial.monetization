using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SkinButton : MonoBehaviour
    {
        [SerializeField] private Text m_Title = null;
        [SerializeField] private RawImage m_Icon = null;
        [SerializeField] private Button m_Button = null;
        [SerializeField] private Image m_LockIcon = null;

        public void SetUp(Texture2D icon, string title, bool isLocked)
        {
            m_Title.text = title;
            m_Icon.texture = icon;
            m_LockIcon.gameObject.SetActive(isLocked);
        }
        
        public Button.ButtonClickedEvent onClick
        {
            get { return m_Button.onClick; }
            set { m_Button.onClick = value; }
        }
    }
}


