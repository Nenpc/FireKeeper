using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using GameView;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class BonfireMarker : MonoBehaviour
    {
        [SerializeField] private Image arrow;
        [SerializeField] private int hideDistance = 10;
        
        [Space]
        [SerializeField] private Player player;
        [SerializeField] private BonfireView bonefire;
        
        private int size;

        private void Awake()
        {
            size = (int)(GetComponent<RectTransform>().sizeDelta.x * 0.5f);
        }

        void Update()
        {
            if (Vector3.Distance(bonefire.transform.position, player.transform.position) > hideDistance)
            {
                arrow.gameObject.SetActive(true);
                Vector3 targetDir = bonefire.transform.position - player.transform.position;

                var arrowRotation = Vector3.Angle(Vector3.forward, targetDir);
                if (player.transform.position.x > bonefire.transform.position.x)
                    arrowRotation = 360 - arrowRotation;
                    
                arrow.rectTransform.rotation = Quaternion.Euler(0, 0, -arrowRotation);
                Debug.Log(arrowRotation);

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
