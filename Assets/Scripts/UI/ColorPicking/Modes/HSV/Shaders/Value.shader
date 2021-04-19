Shader "SmallTail/ColorPicker/Value"
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
            #include "../../../Shaders/ColorLib.cginc"

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

            int _Hue;
            
            float4 frag (v2f i) : SV_Target
            {
                float val = i.uv.x;

                float4 hueCol = hueToRGB(_Hue);
                
                return float4(val * hueCol.r, val * hueCol.g, val * hueCol.b, i.color.a);
            }
            
            ENDCG
        }
    }
}