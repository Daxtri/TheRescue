using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BloomEffect : MonoBehaviour {

	//shader Passes
	const int BoxDownPrefilterPass = 0;
	const int BoxDownPass = 1;
	const int BoxUpPass = 2;
	const int ApplyBloomPass = 3;
	const int DebugBloomPass = 4;

	public Shader bloomShader;
	public bool debug;

	[Range(1, 16)] //bluring
	public int iterations = 4;
	[Range(0, 10)] //pixels oncluded in bloom
	public float threshold = 1.4f;
	[Range(0, 10)]
	public float intensity = 1;
	[Range(0, 1)]
	public float softThreshold = 0.5f;

	RenderTexture[] textures = new RenderTexture[16];

	[NonSerialized]
	Material bloom;

	void OnRenderImage (RenderTexture src, RenderTexture dest) {
		if (bloom == null) {
			bloom = new Material(bloomShader);
			bloom.hideFlags = HideFlags.HideAndDontSave;
		}

		//bloom.SetFloat("_Threshold", threshold);
		//bloom.SetFloat("_SoftThreshold", softThreshold);
		float knee = threshold * softThreshold;
		Vector4 filter;
		filter.x = threshold;
		filter.y = filter.x - knee;
		filter.z = 2f * knee;
		filter.w = 0.25f / (knee + 0.00001f);

		bloom.SetVector("_Filter", filter);
		bloom.SetFloat("_Intensity", Mathf.GammaToLinearSpace(intensity));

		//Downsampling (Blur) - increase denominator value for + bluriness
		int width = src.width / 2;
		int height = src.height / 2;
		RenderTextureFormat format = src.format;

		//temporary texture
		RenderTexture currentDest = textures[0] =
			RenderTexture.GetTemporary(width, height, 0, format);
		Graphics.Blit(src, currentDest, bloom, BoxDownPrefilterPass);
		RenderTexture currentSrc = currentDest;

		//downsampling iterations
		int i = 1;
		for (; i < iterations; i++) {
			width /= 2;
			height /= 2;
			if (height < 2) {
				break;
			}
			currentDest = textures[i] =
				RenderTexture.GetTemporary(width, height, 0, format);
			Graphics.Blit(currentSrc, currentDest, bloom, BoxDownPass);
			currentSrc = currentDest;
		}

		//upsampling iterations
		for (i -= 2; i >= 0; i--) {
			currentDest = textures[i];
			textures[i] = null;
			Graphics.Blit(currentSrc, currentDest, bloom, BoxUpPass);
			RenderTexture.ReleaseTemporary(currentSrc);
			currentSrc = currentDest;
		}

		if (debug) {
			Graphics.Blit(currentSrc, dest, bloom, DebugBloomPass);
		}
		else {
			bloom.SetTexture("_SourceTex", src);
			Graphics.Blit(currentSrc, dest, bloom, ApplyBloomPass);
		}
		RenderTexture.ReleaseTemporary(currentSrc);
	}
}