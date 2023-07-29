using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonfireView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem spark;
        [SerializeField] private ParticleSystem smoke;
        [SerializeField] private ParticleSystem fire;
        [SerializeField] private ParticleSystem fireSecond;
        [SerializeField] private Light light;
        
        private IBonfireController _bonfireController;

        private float _startSparkStartSize;
        private float _startSparkStartSpeed;
        
        private float _startSmokeStartSize;        
        private float _startSmokeStartSpeed;
        
        private float _startFireStartSize;        
        private float _startFireStartSpeed;
        
        private float _startFireSecondStartSize;        
        private float _startFireSecondStartSpeed;
        
        private float _startLightRange;        
        private float _startLightIntensity;
        
        public void Initialize(IBonfireController bonfireDefinition)
        {
            _bonfireController = bonfireDefinition;
            
            _startSparkStartSize = spark.startSize;
            _startSparkStartSpeed = spark.startSpeed;
            
            _startSmokeStartSize = smoke.startSize;        
            _startSmokeStartSpeed = smoke.startSpeed;
            
            _startFireStartSize = fire.startSize;        
            _startFireStartSpeed = fire.startSpeed;
            
            _startFireSecondStartSize = fireSecond.startSize;        
            _startFireSecondStartSpeed = fireSecond.startSpeed;
            
            _startLightRange = light.range;        
            _startLightIntensity = light.intensity;
        }
        
        public void Start()
        {
            BonfirePower(1);

            spark.Play();
            smoke.Play();
            fire.Play();
            fireSecond.Play();
            light.gameObject.SetActive(true);
        }

        public void BonfirePower(float value)
        {
            spark.startSize = _startSparkStartSize * value;
            spark.startSpeed = _startSparkStartSpeed * value;

            smoke.startSize = _startSmokeStartSize * value;
            smoke.startSpeed = _startSmokeStartSpeed * value;

            fire.startSize = _startFireStartSize * value;
            fire.startSpeed = _startFireStartSpeed * value;
			
            fireSecond.startSize = _startFireSecondStartSize * value;
            fireSecond.startSpeed = _startFireSecondStartSpeed * value;

            light.range = _startLightRange * value;
            light.intensity = _startLightIntensity * value;
        }

        public void Stop()
        {
            spark.Stop();
            smoke.Stop();
            fire.Stop();
            light.gameObject.SetActive(false);
        }
    }
}