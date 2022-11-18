using GameLogic;
using UnityEngine;

namespace Managers
{
    public abstract class BaseTakingObjects : MonoBehaviour
    {
        [SerializeField] private readonly int logCreatePositionY = 50;
        [SerializeField] private readonly int borderIndentCreatePosition = 3;
        
        [SerializeField] protected int objectLayerMask = 256;
        [SerializeField] protected float minDistanceFromBonfire = 10;
        [SerializeField] protected float minDistanceFromSameObject = 3;
        [SerializeField] protected Transform pullContainer;
        [SerializeField] protected Transform gameContainer;
        [SerializeField] protected Terrain terrain;
        
        protected Vector2 terrainSize;

        protected ITimeService timeService;
        protected IBonfire bonfire;
        
        protected Vector3 FindPosition()
        {
            bool haveValue = false;
            int maxTryAmount = 5;
            while (!haveValue  && maxTryAmount > 0)
            {
                var createPosition = new Vector3(
                    Random.Range(borderIndentCreatePosition, terrainSize.x - borderIndentCreatePosition),
                    logCreatePositionY,
                    Random.Range(borderIndentCreatePosition, terrainSize.y - borderIndentCreatePosition));

                RaycastHit hit;
                if (Physics.Raycast(createPosition, Vector3.down, out hit, 100))
                {
                    var hitPosition = hit.point + (Vector3.up * 0.5f);
                    if (Vector3.Distance(hitPosition, bonfire.GetStartPosition()) > minDistanceFromBonfire)
                    {
                        var nearestObjects = Physics.OverlapSphere(hitPosition, minDistanceFromSameObject, objectLayerMask);
                        if (nearestObjects.Length == 0)
                        {
                            return hitPosition;
                        } 
                    }
                }
                maxTryAmount--;

                if (maxTryAmount == 0)
                {
                    Debug.LogWarning("Can't find log position");
                }
            }

            return Vector3.zero;
        }
    }
}