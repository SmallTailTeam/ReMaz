Shader "SmallTail/ColorPicker/HueCircle"
{
    Properties
    {
        _Mask ("Mask", 2D) = "white" {}
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

            sampler2D _Mask;

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
                float2 uvc = -1 * (2 * i.uv - 1);
                float angle = (atan2(uvc.y, uvc.x) + UNITY_PI) / UNITY_TWO_PI * 360;
                
                return hueToRGB(angle) * tex2D(_Mask, i.uv).a * i.color.a;
            }
            ENDCG
        }
    }
}
