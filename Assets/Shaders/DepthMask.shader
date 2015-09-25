Shader "Depth Mask/Depth Mask" {
	
	SubShader
    {
        Tags {"Queue" = "Geometry-1" }
        Lighting Off
        Pass
        {
            ZWrite On
            ZTest LEqual
            ColorMask 0    
        }
    }
}