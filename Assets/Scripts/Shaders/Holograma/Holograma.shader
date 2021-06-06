Shader "My Holograma Shader"
{
	Properties
	{
		_Color("Color", Color) = (0, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_AlphaTexture("Alpha Mask (R)", 2D) = "white" {}
		//Alpha Mask Properties
		_Scale("Alpha Tiling", Range(2, 6)) = 2
		_ScrollSpeedV("Alpha scroll Speed", Range(0, 5.0)) = 1.0
			// Glow
		_GlowIntensity("Glow Intensity", Range(0.01, 1.0)) = 0.5
			// Glitch
		_GlitchSpeed("Glitch Speed", Range(0, 50)) = 50.0
		_GlitchIntensity("Glitch Intensity", Range(0.0, 0.1)) = 0
	}

		SubShader
		{
			Tags{ "Queue" = "Transparent" }

			Pass
			{
				Lighting Off
				ZWrite On
				Blend SrcAlpha One
				Cull Back

				CGPROGRAM

					#pragma vertex vert
					#pragma fragment frag

					#include "UnityCG.cginc"

					struct appdata {
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
						float3 normal : NORMAL;
					};

					struct v2f {
						float4 position : SV_POSITION;
						float2 uv : TEXCOORD0;
						float3 worldPos : TEXCOORD1;
						float3 viewDir : TEXCOORD2;
						float3 worldNormal : NORMAL;
					};

					fixed4 _Color, _MainTex_ST;
					sampler2D _MainTex, _AlphaTexture;
					half _Scale, _ScrollSpeedV, _GlowIntensity, _GlitchSpeed, _GlitchIntensity;

					v2f vert(appdata v) {
						v2f o;

						//Criar o Glitch
						v.vertex.z += sin(_Time.y * _GlitchSpeed * 5 * v.vertex.y) * _GlitchIntensity;

						o.position = UnityObjectToClipPos(v.vertex);
						o.uv = TRANSFORM_TEX(v.uv, _MainTex);

						//Alpha mask coordinates
						o.worldPos = UnityObjectToViewPos(v.vertex);

						//Scroll Alpha mask uv
						o.worldPos.y += _Time * _ScrollSpeedV;

						o.worldNormal = UnityObjectToWorldNormal(v.normal);
						o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldPos.xyz));

						return o;
					}

					fixed4 frag(v2f v) : SV_Target{

						half dirVertex = (dot(v.worldPos, 1.0) + 1) / 2;

						fixed4 alphaColor = tex2D(_AlphaTexture,  v.worldPos.xy * _Scale);
						fixed4 pixelColor = tex2D(_MainTex, v.uv);
						pixelColor.w = alphaColor.w;

						// Rim Light
						half rim = 1.0 - saturate(dot(v.viewDir, v.worldNormal));

						return pixelColor * _Color * (rim + _GlowIntensity);
					}
				ENDCG
			}
		}
}