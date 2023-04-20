using UnityEngine.Events;
using Zenject;

namespace UnityEngine.UI
{
    public class AudioMixerVolumeView : MonoBehaviour
    {
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _sfx;
        private AudioMixerVolume _audio;
        private UnityAction<float> _onMusicChange;
        private UnityAction<float> _onSFXChange;
        
        [Inject]
        public void SetSettings (AudioMixerVolume audio)
        {
            _audio = audio;
        }

        public void OnEnable ()
        {
            _music.value = Mathf.Lerp(_music.minValue, _music.maxValue, _audio.Volume.music);
            _sfx.value = Mathf.Lerp(_sfx.minValue, _sfx.maxValue, _audio.Volume.sfx);
            
            _onMusicChange += InMusicChange;
            _onSFXChange += InSFXChange;

            _music.onValueChanged.AddListener(_onMusicChange);
            _sfx.onValueChanged.AddListener(_onSFXChange);
        }

        public void OnDisable ()
        {
            _music.onValueChanged.RemoveListener(_onMusicChange);
            _sfx.onValueChanged.RemoveListener(_onSFXChange);

            _onMusicChange -= InMusicChange;
            _onSFXChange -= InSFXChange;
        }

        private void InMusicChange (float value)
        {
            AudioVolume values = _audio.Volume;
            values.music = Mathf.InverseLerp(_music.minValue, _music.maxValue, value);
            _audio.Volume = values;
        }

        private void InSFXChange (float value)
        {
            AudioVolume values = _audio.Volume;
            values.sfx = Mathf.InverseLerp(_sfx.minValue, _sfx.maxValue, value);
            _audio.Volume = values;
        }
    }
}
