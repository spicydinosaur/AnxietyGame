// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Squax/Sprite/HSVColor/Normal" {
	Properties {
		[PerRendererData] _MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
        Tags
        {
                "IgnoreProjector" = "True"
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
        }
        
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGB
		AlphaTest Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma target 2.0
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct vertexInput {
				float4 vertex : POSITION;
				float2 texcoord0 : TEXCOORD0;
				float4 color : COLOR;
			};

			struct fragmentInput{
				float4 position : SV_POSITION;
				float2 texcoord0 : TEXCOORD0;
				float4 color : COLOR;
			};
			
			float3 rgb2hsv(float3 c)
			{
			    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			    float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
			    float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

			    float d = q.x - min(q.w, q.y);
			    float e = 1.0e-10;
			    
			    return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}

			float3 hsv2rgb(float3 c)
			{
			    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			    float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			    
			    return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
			}
			
			fragmentInput vert(vertexInput i)
			{
		        fragmentInput o;
		        
		        o.position = UnityPixelSnap(UnityObjectToClipPos(i.vertex));
		        
		        o.texcoord0 = i.texcoord0;
		        o.color = i.color;
		        
		        return o;
			}
            
			float rand(float2 coord) {
				coord = fmod(coord, float2(2.0,1.0)*round(32));
				return frac(sin(dot(coord.xy ,float2(12.9898,78.233))) * 15.5453 * 2);
			}
            
			float noise(float2 coord){
				float2 i = floor(coord);
				float2 f = frac(coord);
				
				float a = rand(i);
				float b = rand(i + float2(1.0, 0.0));
				float c = rand(i + float2(0.0, 1.0));
				float d = rand(i + float2(1.0, 1.0));

				float2 cubic = f * f * (3.0 - 2.0 * f);

				return lerp(a, b, cubic.x) + (c - a) * cubic.y * (1.0 - cubic.x) + (d - b) * cubic.x * cubic.y;
			}

			float4 frag(fragmentInput i) : COLOR
			{
				float4 c = tex2D (_MainTex, i.texcoord0);
				
	    		float3 fragHSV = rgb2hsv(c.xyz).xyz;
	    		fragHSV.x = fragHSV.x + i.color.x;
	    		fragHSV.yz = fragHSV.yz * (i.color.yz * 2.0);
	    		
				return float4(hsv2rgb(fragHSV).xyz, c.a * i.color.a);
			}
			ENDCG
		}
	} 
}
