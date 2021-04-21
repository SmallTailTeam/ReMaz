Shader "SmallTail/ColorPicker/SaturationValue"
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
            #include "../../Shaders/ColorLib.cginc"

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

            int _Hue;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                float sat = i.uv.x;
                float val = i.uv.y;

                float4 hueCol = hueToRGB(_Hue);
                
                return float4(val * lerp(1, hueCol.r, sat), val * lerp(1, hueCol.g, sat), val * lerp(1, hueCol.b, sat), i.color.a);
            }
            
            ENDCG
        }
    }
}