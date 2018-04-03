// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Space Game/Ship Shader"
{
	Properties
	{
		_HighlightPattern("Highlight Pattern", 2D) = "white" {}
		_HighlightRepetition("Highlight Repetition", Vector) = (30,0,0,0)
		_Color("Color", Color) = (1,0,0,1)
		_Rotation("Rotation", Float) = -45
		_HighlightColor("Highlight Color", Color) = (1,1,0,0)
		_Speed("Speed", Vector) = (-0.25,0,0,0)
		_Highlight("Highlight", Int) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float4 screenPos;
		};

		uniform fixed4 _Color;
		uniform float4 _HighlightColor;
		uniform sampler2D _HighlightPattern;
		uniform half2 _Speed;
		uniform fixed _Rotation;
		uniform half2 _HighlightRepetition;
		uniform int _Highlight;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime47 = _Time.y * 1;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float cos39 = cos( radians( _Rotation ) );
			float sin39 = sin( radians( _Rotation ) );
			float2 rotator39 = mul( (ase_grabScreenPosNorm).xyzw.xy - float2( 0,0 ) , float2x2( cos39 , -sin39 , sin39 , cos39 )) + float2( 0,0 );
			float temp_output_59_0 = ( tex2D( _HighlightPattern, ( ( _Speed * mulTime47 ) + ( rotator39 * _HighlightRepetition ) ) ).r * (( (float)_Highlight >= 1 ) ? 1 :  0 ) );
			float4 lerpResult53 = lerp( _Color , _HighlightColor , temp_output_59_0);
			o.Albedo = lerpResult53.rgb;
			o.Emission = ( _HighlightColor * temp_output_59_0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15101
-1716;93;1357;655;1306.289;518.8894;2.326117;True;True
Node;AmplifyShaderEditor.RangedFloatNode;38;-608,320;Fixed;False;Property;_Rotation;Rotation;3;0;Create;True;0;0;False;0;-45;-20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;22;-848,208;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;25;-512,208;Float;False;True;True;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RadiansOpNode;45;-432,320;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;39;-272,208;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;26;-256,336;Half;False;Property;_HighlightRepetition;Highlight Repetition;1;0;Create;True;0;0;False;0;30,0;30,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;49;-192,-32;Half;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;-0.25,0;-0.25,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;47;-176,96;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;0,32;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;0,256;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.IntNode;55;452.9802,517.1589;Float;False;Property;_Highlight;Highlight;6;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;192,128;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCCompareGreaterEqual;58;621.8787,498.0614;Float;False;4;0;INT;0;False;1;FLOAT;1;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;368,304;Float;True;Property;_HighlightPattern;Highlight Pattern;0;0;Create;True;0;0;False;0;63efcc999428262488a50d488da90e3c;63efcc999428262488a50d488da90e3c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;716.0012,-48.43496;Fixed;False;Property;_Color;Color;2;0;Create;True;0;0;False;0;1,0,0,1;1,0,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;790.4574,318.0627;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;448,80;Float;False;Property;_HighlightColor;Highlight Color;4;0;Create;True;0;0;False;0;1,1,0,0;1,0.634,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;968.2046,243.7967;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;53;998.6445,75.18639;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1212,-13;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Space Game/Ship Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;2;1,1,1,0;VertexOffset;True;False;Spherical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;22;0
WireConnection;45;0;38;0
WireConnection;39;0;25;0
WireConnection;39;2;45;0
WireConnection;48;0;49;0
WireConnection;48;1;47;0
WireConnection;27;0;39;0
WireConnection;27;1;26;0
WireConnection;50;0;48;0
WireConnection;50;1;27;0
WireConnection;58;0;55;0
WireConnection;28;1;50;0
WireConnection;59;0;28;1
WireConnection;59;1;58;0
WireConnection;54;0;5;0
WireConnection;54;1;59;0
WireConnection;53;0;1;0
WireConnection;53;1;5;0
WireConnection;53;2;59;0
WireConnection;0;0;53;0
WireConnection;0;2;54;0
ASEEND*/
//CHKSM=C0611D60BF7BE32B335FF5540F50798E4DAF0367