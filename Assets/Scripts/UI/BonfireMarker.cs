using GameLogic;
using GameView;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameUI
{
    public class BonfireMarker : MonoBehaviour
    {
        [SerializeField] private Image arrow;
        [SerializeField] private int hideDistance = 10;

        private IPlayer player;
        private IBonfire bonfire;
        
        private int size;

        [Inject]
        private void Construct(IPlayer player, IBonfire bonfire)
        {
            this.player = player;
            this.bonfire = bonfire;
            
            size = (int)(GetComponent<RectTransform>().sizeDelta.x * 0.5f);
        }

        void Update()
        {
            var bonfirePosition = bonfire.GetStartPosition();
            var playerPosition = player.GetTransform().position;
            if (Vector3.Distance(bonfirePosition, playerPosition) > hideDistance)
            {
                arrow.gameObject.SetActive(true);
                Vector3 targetDir = bonfirePosition - playerPosition;

                var arrowRotation = Vector3.Angle(Vector3.forward, targetDir);
                if (playerPosition.x > bonfirePosition.x)
                    arrowRotation = 360 - arrowRotation;
                    
                arrow.rectTransform.rotation = Quaternion.Euler(0, 0, -arrowRotation);

                var arrowPosition = new Vector2(Mathf.Sin(arrowRotation * Mathf.Deg2Rad),
                    Mathf.Cos(arrowRotation * Mathf.Deg2Rad)) * size;
                arrow.rectTransform.localPosition = arrowPosition;
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
        }
    }
}
