Shader "Mobile/VertexLit (Single Color)" 
{
    Properties 
    {
        _Color ("Color", color) = (1,1,1,1)
    }
     
    SubShader 
    {
        Tags { "RenderType" = "Opaque" }
        LOD 80
     
        Pass 
        {
            Tags { "LightMode" = "Vertex" }
     
            Material 
            {
                Diffuse [_Color]
                Ambient [_Color]
            }
    
            Lighting On
    
            SetTexture [_MainTex] 
            {
                constantColor [_Color]
                Combine primary DOUBLE, constant 
            }
        }
     
        Pass 
        {
            Tags { "LightMode" = "VertexLM" }
     
            BindChannels 
            {
                Bind "Vertex", vertex
                Bind "normal", normal
                Bind "texcoord1", texcoord0 
            }
     
            SetTexture [unity_Lightmap] 
            {
                matrix [unity_LightmapMatrix]
                combine texture
            }
    
            SetTexture [_MainTex] 
            {
                constantColor [_Color]
                combine previous, constant
            }
        }
     
        Pass 
        {
            Tags { "LightMode" = "VertexLMRGBM" }
     
            BindChannels 
            {
                Bind "Vertex", vertex
                Bind "normal", normal
                Bind "texcoord1", texcoord0 
                Bind "texcoord", texcoord1
            }
     
            SetTexture [unity_Lightmap] 
            {
                matrix [unity_LightmapMatrix]
                combine texture * texture alpha DOUBLE
            }
    
            SetTexture [_MainTex] 
            {
                constantColor [_Color]
                combine previous QUAD, constant
            }
        }
     
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
     
            ZWrite On ZTest LEqual Cull Off
     
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"
     
            struct v2f 
            {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_OUTPUT_STEREO
            };
     
            v2f vert( appdata_base v )
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }
     
            float4 frag( v2f i ) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}