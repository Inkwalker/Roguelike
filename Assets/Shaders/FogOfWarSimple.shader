// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33687,y:32705,varname:node_4013,prsc:2|emission-2855-OUT,custl-9602-OUT;n:type:ShaderForge.SFN_Tex2d,id:2453,x:32737,y:32925,ptovrint:False,ptlb:FogMapA,ptin:_FogMapA,varname:node_2453,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dcfce7b76dc817448b640fccc33399a5,ntxv:0,isnm:False|UVIN-2406-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:8207,x:31820,y:33045,varname:node_8207,prsc:2;n:type:ShaderForge.SFN_Append,id:2680,x:32014,y:33045,varname:node_2680,prsc:2|A-8207-X,B-8207-Z;n:type:ShaderForge.SFN_Vector4Property,id:2002,x:32070,y:33238,ptovrint:False,ptlb:FogSize,ptin:_FogSize,varname:node_2002,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_ComponentMask,id:2097,x:32243,y:33238,varname:node_2097,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2002-XYZ;n:type:ShaderForge.SFN_Divide,id:2406,x:32518,y:33045,varname:node_2406,prsc:2|A-3827-OUT,B-2097-OUT;n:type:ShaderForge.SFN_Vector4Property,id:2714,x:31798,y:32830,ptovrint:False,ptlb:FogOffset,ptin:_FogOffset,varname:node_2714,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_ComponentMask,id:232,x:32014,y:32830,varname:node_232,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2714-XYZ;n:type:ShaderForge.SFN_Add,id:3827,x:32243,y:33045,varname:node_3827,prsc:2|A-232-OUT,B-2680-OUT;n:type:ShaderForge.SFN_VertexColor,id:5211,x:32604,y:33458,varname:node_5211,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9593,x:33212,y:33436,varname:node_9593,prsc:2|A-3388-OUT,B-1486-OUT,C-5066-RGB,D-3915-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:1486,x:32889,y:33660,varname:node_1486,prsc:2;n:type:ShaderForge.SFN_LightVector,id:1266,x:32378,y:33602,varname:node_1266,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5386,x:32378,y:33727,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:5824,x:32604,y:33663,varname:node_5824,prsc:2,dt:1|A-1266-OUT,B-5386-OUT;n:type:ShaderForge.SFN_LightColor,id:5066,x:32889,y:33786,varname:node_5066,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3388,x:32814,y:33511,varname:node_3388,prsc:2|A-5211-RGB,B-5824-OUT;n:type:ShaderForge.SFN_AmbientLight,id:3396,x:33141,y:32513,varname:node_3396,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8239,x:33395,y:32582,varname:node_8239,prsc:2|A-3396-RGB,B-3915-OUT,C-2181-RGB;n:type:ShaderForge.SFN_VertexColor,id:2181,x:33141,y:32645,varname:node_2181,prsc:2;n:type:ShaderForge.SFN_Desaturate,id:9602,x:33424,y:32936,varname:node_9602,prsc:2|COL-9593-OUT,DES-3074-OUT;n:type:ShaderForge.SFN_RemapRange,id:3074,x:33167,y:33087,varname:node_3074,prsc:2,frmn:0.5,frmx:1,tomn:1,tomx:0|IN-3915-OUT;n:type:ShaderForge.SFN_Desaturate,id:2855,x:33424,y:32779,varname:node_2855,prsc:2|COL-8239-OUT,DES-3074-OUT;n:type:ShaderForge.SFN_Tex2d,id:8342,x:32737,y:33139,ptovrint:False,ptlb:FogMapB,ptin:_FogMapB,varname:node_8342,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dcfce7b76dc817448b640fccc33399a5,ntxv:0,isnm:False|UVIN-2406-OUT;n:type:ShaderForge.SFN_Slider,id:2462,x:32580,y:33343,ptovrint:False,ptlb:FogMapBlend,ptin:_FogMapBlend,varname:node_2462,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:3915,x:32945,y:33087,varname:node_3915,prsc:2|A-2453-R,B-8342-R,T-2462-OUT;proporder:2453-8342-2462-2002-2714;pass:END;sub:END;*/

Shader "Lit/Fog Of War Simple" {
    Properties {
        _FogMapA ("FogMapA", 2D) = "white" {}
        _FogMapB ("FogMapB", 2D) = "white" {}
        _FogMapBlend ("FogMapBlend", Range(0, 1)) = 0
        _FogSize ("FogSize", Vector) = (0,0,0,0)
        _FogOffset ("FogOffset", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _FogMapA; uniform float4 _FogMapA_ST;
            uniform float4 _FogSize;
            uniform float4 _FogOffset;
            uniform sampler2D _FogMapB; uniform float4 _FogMapB_ST;
            uniform float _FogMapBlend;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float2 node_2406 = ((_FogOffset.rgb.rg+float2(i.posWorld.r,i.posWorld.b))/_FogSize.rgb.rg);
                float4 _FogMapA_var = tex2D(_FogMapA,TRANSFORM_TEX(node_2406, _FogMapA));
                float4 _FogMapB_var = tex2D(_FogMapB,TRANSFORM_TEX(node_2406, _FogMapB));
                float node_3915 = lerp(_FogMapA_var.r,_FogMapB_var.r,_FogMapBlend);
                float node_3074 = (node_3915*-2.0+2.0);
                float3 emissive = lerp((UNITY_LIGHTMODEL_AMBIENT.rgb*node_3915*i.vertexColor.rgb),dot((UNITY_LIGHTMODEL_AMBIENT.rgb*node_3915*i.vertexColor.rgb),float3(0.3,0.59,0.11)),node_3074);
                float3 finalColor = emissive + lerp(((i.vertexColor.rgb*max(0,dot(lightDirection,i.normalDir)))*attenuation*_LightColor0.rgb*node_3915),dot(((i.vertexColor.rgb*max(0,dot(lightDirection,i.normalDir)))*attenuation*_LightColor0.rgb*node_3915),float3(0.3,0.59,0.11)),node_3074);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _FogMapA; uniform float4 _FogMapA_ST;
            uniform float4 _FogSize;
            uniform float4 _FogOffset;
            uniform sampler2D _FogMapB; uniform float4 _FogMapB_ST;
            uniform float _FogMapBlend;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float2 node_2406 = ((_FogOffset.rgb.rg+float2(i.posWorld.r,i.posWorld.b))/_FogSize.rgb.rg);
                float4 _FogMapA_var = tex2D(_FogMapA,TRANSFORM_TEX(node_2406, _FogMapA));
                float4 _FogMapB_var = tex2D(_FogMapB,TRANSFORM_TEX(node_2406, _FogMapB));
                float node_3915 = lerp(_FogMapA_var.r,_FogMapB_var.r,_FogMapBlend);
                float node_3074 = (node_3915*-2.0+2.0);
                float3 finalColor = lerp(((i.vertexColor.rgb*max(0,dot(lightDirection,i.normalDir)))*attenuation*_LightColor0.rgb*node_3915),dot(((i.vertexColor.rgb*max(0,dot(lightDirection,i.normalDir)))*attenuation*_LightColor0.rgb*node_3915),float3(0.3,0.59,0.11)),node_3074);
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
