
using UnityEngine;

namespace AuidoHelper
{
	class AudioHelper : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;

		public void PlaySound(AudioClip clip)
		{
			_audioSource.clip = clip;
			_audioSource.Play();
		}
	}
}
