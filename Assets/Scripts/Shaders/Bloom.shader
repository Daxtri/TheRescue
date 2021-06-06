Shader "My Bloom Shader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}

	CGINCLUDE
		#include "UnityCG.cginc"

		struct appData {
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		v2f vert (appData v) {
			v2f i;
			i.pos = UnityObjectToClipPos(v.vertex);
			i.uv = v.uv;
			return i;
		}

		sampler2D _MainTex, _SourceTex;
		float4 _MainTex_TexelSize;
		half4 _Filter;
		half _Intensity;

		//Box DownSampling
		half3 Sample (float2 uv) {
			return tex2D(_MainTex, uv).rgb;
		}

		half3 SampleBox (float2 uv, float delta) {
			float4 o = _MainTex_TexelSize.xyxy * float2(-delta, delta).xxyy;
			half3 s =
				Sample(uv + o.xy) + Sample(uv + o.zy) +
				Sample(uv + o.xw) + Sample(uv + o.zw);
			return s * 0.25f;
		}

		// threshold
		half3 Prefilter (half3 c) {
			half brightness = max(c.r, max(c.g, c.b));
			half soft = brightness - _Filter.y;
			soft = clamp(soft, 0, _Filter.z);
			soft = soft * soft * _Filter.w;
			half contribution = max(soft, brightness - _Filter.x);
			contribution /= max(brightness, 0.00001);
			return c * contribution;
		}

	ENDCG

	SubShader {
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass  //0 - PreFilter
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				half4 frag(v2f i) : SV_Target {
					return half4(Prefilter(SampleBox(i.uv, 1)), 1);
				}
			ENDCG
		}

		Pass //1 - downsampling
		{ 
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				half4 frag(v2f i) : SV_Target {
					return half4(SampleBox(i.uv, 1), 1);
				}
			ENDCG
		}

		Pass //2 - upsampling
		{ 
			Blend One One
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				half4 frag(v2f i) : SV_Target {
					return half4(SampleBox(i.uv, 0.5), 1);
				}
			ENDCG
		}

		Pass //3 - apply bloom
		{ 
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				half4 frag(v2f i) : SV_Target {
					half4 c = tex2D(_SourceTex, i.uv);
					c.rgb += _Intensity * SampleBox(i.uv, 0.5);
					return c;
				}
			ENDCG
		}

		Pass  //4 - Debug
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				half4 frag(v2f i) : SV_Target {
					return half4(_Intensity * SampleBox(i.uv, 0.5), 1);
				}
			ENDCG
		}
	}
}