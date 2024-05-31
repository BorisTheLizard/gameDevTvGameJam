using UnityEngine;

public class MouthAnimation : MonoBehaviour
{
	// Reference to the mouth bones
	public Transform lipsTop;
	public Transform lipsLow;

	// Amplitude scale factor
	public float amplitudeScale = 1.0f;


	public float maxUpPosition = 0.5f;
	public float minUpPosition = 0.1f;


	public float maxLowPosition = -0.2f;
	public float minLowPosition = -0.1f;

	[SerializeField] AudioSource audioSource;


	void Start()
	{
		// Get the AudioSource component attached to the GameObject
		//audioSource = GetComponent<AudioSource>();

		// Check if AudioSource component exists
		if (audioSource == null)
		{
			Debug.LogError("AudioSource component not found!");
			return;
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        // If audio is playing, analyze amplitude
        if (audioSource.isPlaying)
        {
            // Get the amplitude of the currently playing clip
            float amplitude = GetAmplitude();

            // Clamp the amplitude value between 0 and 1
            amplitude = Mathf.Clamp01(amplitude * amplitudeScale);

            // Calculate new positions for the mouth bones based on amplitude
            float lipsTopY = Mathf.Lerp(0.0f, maxUpPosition, amplitude);
            float lipsLowY = Mathf.Lerp(0.0f, maxLowPosition, amplitude);

            // Clamp minimum values for the lips' positions
            lipsTopY = Mathf.Max(lipsTopY, minUpPosition);
            lipsLowY = Mathf.Max(lipsLowY, minLowPosition);

            // Update the position of the mouth bones
            lipsTop.localPosition = new Vector3(lipsTop.localPosition.x, lipsTopY, lipsTop.localPosition.z);
            lipsLow.localPosition = new Vector3(lipsLow.localPosition.x, lipsLowY, lipsLow.localPosition.z);
        }
    }
	// Function to calculate the amplitude of the currently playing clip
	private float GetAmplitude()
	{
		float[] samples = new float[1024];
		audioSource.GetOutputData(samples, 0); // Get audio data from the audio source

		float maxAmplitude = 0f;
		foreach (float sample in samples)
		{
			float absSample = Mathf.Abs(sample);
			if (absSample > maxAmplitude)
			{
				maxAmplitude = absSample;
			}
		}
		return maxAmplitude;
	}
}