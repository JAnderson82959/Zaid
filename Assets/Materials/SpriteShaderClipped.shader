Shader "Custom/SpriteShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _DissolveIntensity ("Dissolve Intensity", Range(0,1)) = 0
        _DissolveEdgeColor ("Dissolve Edge Color", Color) = (1, 0, 0, 1)
        _DissolveEdgeRange ("Dissolve Edge Range", Range(0,1)) = 0
        _DissolveEdgeMultiplier ("Dissolve Edge Multiplier", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off 

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _DissolveTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DissolveTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        uniform float4 _DissolveEdgeColor;      
        uniform float _DissolveEdgeRange;
        uniform float _DissolveIntensity;
        uniform float _DissolveEdgeMultiplier;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 dissolveColor = tex2D(_DissolveTex, IN.uv_DissolveTex);
            half dissolveClip = dissolveColor.r - (_DissolveIntensity * 1.2);
            half dissolveRamp = max(0, _DissolveEdgeRange - dissolveClip);
            clip(dissolveClip);
            float3 nullColor = (0.0f, 0.0f, 0.0f);
            fixed4 texCord = tex2D (_MainTex, IN.uv_MainTex);
            bool clipCond = (texCord.r + texCord.g + texCord.b <= 1/255);

            //shader clips pixels if they're perfect near-black in the texture          
            clip(-1 * clipCond);
            
            // Albedo comes from a texture tinted by color
            fixed4 c = texCord * _Color;
            o.Albedo = lerp( c.rgb, _DissolveEdgeColor.rgb, min(1, dissolveRamp * _DissolveEdgeMultiplier) );
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
