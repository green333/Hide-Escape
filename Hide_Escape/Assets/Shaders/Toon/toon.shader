Shader "Unlit/toon"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		//	1パス目（アウトライン）
		Pass
		{
			Cull Front
			Blend SrcAlpha OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex VS_MAIN
			#pragma fragment PS_MAIN
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"


			struct VS_INPUT
			{
				float4 vertex	: POSITION;
				float2 uv		: TEXCOORD0;
				float4 normal	: NORMAL;
				float4 color	: COLOR;
			};

			typedef struct VS_OUT
			{
				float2 uv		: TEXCOORD0;
				float4 vertex	: SV_POSITION;
				float4 color	: COLOR;
				UNITY_FOG_COORDS(1)
			} PS_INPUT;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			VS_OUT VS_MAIN(VS_INPUT v)
			{
				VS_OUT o;
				
				v.vertex.xyz += v.normal * 0.25;
				
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			

			fixed4 PS_MAIN(PS_INPUT i) : SV_Target
			{
				fixed4 col = fixed4(1.0, 1.0, 0.0, 0.5);
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}


		//	2パス目（自分自身の影）
		Pass
		{
			CGPROGRAM
			#pragma vertex VS_MAIN
			#pragma fragment PS_MAIN
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct VS_INPUT
			{
				float4 vertex	: POSITION;
				float2 uv		: TEXCOORD0;
				float level		: TEXCOORD1;
				float4 normal	: NORMAL;
			};

			typedef struct VS_OUT
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float level : TEXCOORD1;
			
			} PS_INPUT;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			VS_OUT VS_MAIN(VS_INPUT v)
			{
				VS_OUT o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

				float3x3 mat = _Object2World;
				float3 w_normal = mul(mat, v.normal.xyz);
				o.level = dot(-w_normal, -float3(1.0, 1.0, 0.0));

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				UNITY_TRANSFER_FOG(o,o.vertex);

				return o;
			}
			
			fixed4 PS_MAIN(PS_INPUT i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				if(i.level < 0.02) { col.rgb *= 0.5; }

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
