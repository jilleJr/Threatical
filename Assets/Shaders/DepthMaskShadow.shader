Shader "Depth Mask/Depth Cutout" {
  
  Properties {
   _Color ("Main Color", Color) = (1,1,1,1)
   _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
   _Cutoff ("Alpha cutoff", Range(0,1)) = 0.1
  }
  
  
  SubShader {
  
   Tags {"RenderType"="TransparentCutoff"}
   LOD 200
   Blend Zero SrcColor
   Lighting Off
   ZTest LEqual
   ZWrite On
   ColorMask 0
   Pass {
   
   
   }
  
  CGPROGRAM
  #pragma surface surf ShadowOnly alphatest:_Cutoff
  
 
  uniform float temp;
  
  fixed4 _Color;
  
  struct Input {
  
   float2 uv_MainTex;
     
  };
  
 
  inline fixed4 LightingShadowOnly (SurfaceOutput s, fixed3 lightDir, fixed atten)
  
  {
  
   fixed4 c;
   c.rgb = s.Albedo*atten;
   c.a = s.Alpha;
   return c;
  }
  
  void surf (Input IN, inout SurfaceOutput o) {
  if (temp == 1)
  {
   fixed4 c = _Color;
   o.Albedo = c.rgb;
   o.Alpha = 1;
  }
  }
  
 
  ENDCG
  }
  Fallback "Transparent/Cutout/VertexLit"
  }