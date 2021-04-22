Shader "ReMaz!/LevelEditing/Waveform"
{
    Properties
    {
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            StructuredBuffer<float> _Waveform;
            int _Length;
            
            float4 frag (v2f i) : SV_Target
            {
                int index = floor(i.uv.y * _Length);
                float wave = _Waveform[index];
                
                float p = i.uv.x *2-1;

                if(p > wave || -p > wave)
                {
                    discard;
                }
                
                return i.color;
            }
            
            ENDCG
        }
    }
}