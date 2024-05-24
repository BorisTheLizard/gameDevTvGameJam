using UnityEngine;
using TMPro;

public class WavyTextEffect : MonoBehaviour
{
	public float speed = 1f; // Adjust the speed of the wave
	public float intensity = 0.1f; // Adjust the intensity of the wave
	public bool isDoubleSize = true; // Flag to enable double size effect
	public float maxScale = 1.5f; // Maximum scale when isDoubleSize is true
	private TMP_Text textComponent;
	public string originalText;

	void Start()
	{
		textComponent = GetComponent<TMP_Text>();
		if (textComponent == null)
		{
			Debug.LogError("TextMeshPro component not found!");
			enabled = false; // Disable the script if TextMeshPro component is not found
			return;
		}

		// Store the original text to revert to later
		originalText = textComponent.text;
	}

    public void resetText(string newText)
	{
		originalText = newText;
	}

	void Update()
	{
		if (textComponent == null)
			return;

		AnimateText();
	}

	void AnimateText()
	{
		string animatedText = "";
		float scale = 1f;

		for (int i = 0; i < originalText.Length; i++)
		{
			char c = originalText[i];
			float offset = Mathf.Sin(Time.time * speed + i) * intensity;

			if (isDoubleSize)
			{
				// Calculate scale based on offset
				scale = Mathf.Lerp(1f, maxScale, Mathf.Abs(offset) / intensity);
			}

			animatedText += $"<voffset={offset}><size={scale * 100}%>{c}</size></voffset>";
		}

		textComponent.text = animatedText;
	}
}

