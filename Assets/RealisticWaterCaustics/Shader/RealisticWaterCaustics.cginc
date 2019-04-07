// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#ifndef REALISTIC_WATER_CAUSTICS_INCLUDED
#define REALISTIC_WATER_CAUSTICS_INCLUDED

#include "UnityCG.cginc"

uniform float3 _Color;
uniform float _Tiled;
uniform float _Density;
uniform float _Intensity;
uniform float _Lerp;
uniform float _FadeHeight;
uniform float _FadeFalloff;

struct VSOutput
{
	float4 pos : SV_POSITION;
	float2 tex : TEXCOORD0;
	float3 wldpos : TEXCOORD1;
	float3 wldnor : TEXCOORD2;
};
VSOutput vert (appdata_base v)
{
	VSOutput o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.tex = v.texcoord;
	o.wldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
	o.wldnor = mul(unity_ObjectToWorld, SCALED_NORMAL);
	return o;
}
float4 frag (VSOutput input) : SV_TARGET
{
	// height fade
	float fade = (input.wldpos.y - _FadeHeight) / _FadeFalloff;
	fade = 1 - fade;

	// below side fade
	float3 UP = float3(0, 1, 0);
	float3 N = normalize(input.wldnor);
	fade *= min(1, max(0, dot(N, UP) + 0.5));
	
	// caustics
	float2 uv = input.tex;
	float2 p = fmod(uv * _Tiled, _Tiled) - 250.0;
	float2 i = p;
	float c = 1.0;
				
	for (int n = 0; n < ITER; n++)
	{
		float t = _Time.y * (1.0 - (3.5 / float(n + 1)));
		i = p + float2(cos(t - i.x) + sin(t + i.y), sin(t - i.y) + cos(t + i.x));
		c += 1.0 / length(float2(p.x / (sin(i.x + t) / _Density), p.y / (cos(i.y + t) / _Density)));
	}
	c /= float(ITER);
	c = 1.17 - pow(c, _Intensity);
	float tmp = pow(abs(c), 8.0);
	float3 cc = float3(tmp, tmp, tmp);

	return float4(cc * _Color * fade, _Lerp);
}

#endif