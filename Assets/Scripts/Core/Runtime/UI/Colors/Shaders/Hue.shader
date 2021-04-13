Shader "ReMaz!/Colors/Hue"
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
            #include "Colors.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                float dst = distance(i.uv, float2(0.5, 0.5));
                
                if (dst < 0.4 || dst > 0.5)
                {
                    discard;
                }
                
                float2 uvc = -1 * (2 * i.uv - 1);
                float angle = (atan2(uvc.y, uvc.x) + UNITY_PI) / UNITY_TWO_PI * 360;
                
                return hueToRGB(angle);
            }
            ENDCG
        }
    }
}
