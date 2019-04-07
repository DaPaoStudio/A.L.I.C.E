Shader "Realistic Water Caustics/Alpha Blending" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Tiled ("Tiled", Range(0.1, 32)) = 6.283
		_Density ("Density", Range(0.002, 0.02)) = 0.005
		_Intensity ("Intensity", Range(0, 4)) = 1.4
		_Lerp ("Lerp", Range(0, 1)) = 0.5
		_FadeHeight ("Fade Height", Range(0, 6)) = 2
		_FadeFalloff ("Fade Falloff", Range(0.1, 2)) = 1
	}	
	SubShader {
		Pass {
			Tags { "RenderType" = "Transparent - 1" }
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Offset -1, -1
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#define ITER 5
			#include "RealisticWaterCaustics.cginc"
			ENDCG
		}
	} 
	FallBack Off
}
