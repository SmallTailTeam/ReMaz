Shader "ReMaz!/Colors/SaturationValue"
{
    Properties
    {
        
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
            #include "HSV.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            int _Hue;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                float sat = i.uv.x;
                float val = i.uv.y;

                float4 col = hueToRGB(_Hue);
                
                return float4(val * lerp(1, col.r, sat), val * lerp(1, col.g, sat), val * lerp(1, col.b, sat), 1);
            }
            
            ENDCG
        }
    }
}