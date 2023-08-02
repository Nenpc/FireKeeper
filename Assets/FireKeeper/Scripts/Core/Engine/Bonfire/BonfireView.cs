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

            var sparkMain = spark.main;
            _startSparkStartSize = sparkMain.startSizeMultiplier;
            _startSparkStartSpeed = sparkMain.startSpeedMultiplier;
        
            var smokeMain = smoke.main;
            _startSmokeStartSize = smokeMain.startSizeMultiplier;        
            _startSmokeStartSpeed = smokeMain.startSpeedMultiplier;
            
            var fireMain = fire.main;
            _startFireStartSize = fireMain.startSizeMultiplier;        
            _startFireStartSpeed = fireMain.startSpeedMultiplier;
            
            var fireSecondMain = fireSecond.main;
            _startFireSecondStartSize = fireSecondMain.startSizeMultiplier;        
            _startFireSecondStartSpeed = fireSecondMain.startSpeedMultiplier;
            
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
            var sparkMain = spark.main;
            sparkMain.startSizeMultiplier = _startSparkStartSize * value;
            sparkMain.startSpeedMultiplier = _startSparkStartSpeed * value;

            var smokeMain = smoke.main;
            smokeMain.startSizeMultiplier = _startSmokeStartSize * value;
            smokeMain.startSpeedMultiplier = _startSmokeStartSpeed * value;

            
            var fireMain = fire.main;
            fireMain.startSizeMultiplier = _startFireStartSize * value;
            fireMain.startSpeedMultiplier = _startFireStartSpeed * value;
	
            var fireSecondMain = fireSecond.main;
            fireSecondMain.startSizeMultiplier = _startFireSecondStartSize * value;
            fireSecondMain.startSpeedMultiplier = _startFireSecondStartSpeed * value;

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