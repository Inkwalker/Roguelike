Shader "Unlit/Vertex Color" 
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
     
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 color : COLOR0;
			};
			
			v2f vert (appdata_full v)
			{
				 v2f o;
				 o.pos = UnityObjectToClipPos (v.vertex);
				 o.color = v.color;
				 return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
				return half4 (i.color, 1);
			}

			ENDCG
		}
	}
}