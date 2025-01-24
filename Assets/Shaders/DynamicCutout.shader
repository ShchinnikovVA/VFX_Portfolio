Shader "Custom/DynamicCutout"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
        _ShowHole ("Show Hole", Range(0,1)) = 0 // 0 - дырка, 1 - текстура
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        Cull Back

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Cutoff;
            float _ShowHole;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                if (_ShowHole == 0)
                {
                    // Возвращаем прозрачность при условии, что пиксель ниже порога
                    if (texColor.a < _Cutoff)
                        discard; // Отменяем пиксель
        
                    return texColor; // Возвращаем цвет
                }
                else
                {
                    // Показываем простую текстуру
                    return texColor; // Возвращаем цвет
                }
            }
            ENDCG
        }
    }
    
    FallBack "Diffuse"

}