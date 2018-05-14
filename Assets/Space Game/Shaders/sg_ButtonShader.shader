// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Space Game/Button Shader"
{
	Properties
	{
		_ASEOutlineColor( "Outline Color", Color ) = (0,0,0,1)
		_ASEOutlineWidth( "Outline Width", Float ) = 0.02
		_HighlightPattern("Highlight Pattern", 2D) = "white" {}
		_HighlightRepetition("Highlight Repetition", Vector) = (30,0,0,0)
		_Color("Color", Color) = (0,0.6689653,1,1)
		_HighlightColor("Highlight Color", Color) = (1,1,0,0)
		_Speed("Speed", Vector) = (-0.25,0,0,0)
		_Input("Input", Range( 0 , 1)) = 0.6740544
		_Max("Max", Float) = 2
		_CompletionColor("CompletionColor", Color) = (1,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		
		
		struct Input {
			fixed filler;
		};
		uniform fixed4 _ASEOutlineColor;
		uniform fixed _ASEOutlineWidth;
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( v.normal * _ASEOutlineWidth );
		}
		inline fixed4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return fixed4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _ASEOutlineColor.rgb;
			o.Alpha = 1;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.5
		#pragma surface surf Standard keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
		};

		uniform fixed4 _Color;
		uniform float4 _CompletionColor;
		uniform float _Max;
		uniform float _Input;
		uniform float4 _HighlightColor;
		uniform sampler2D _HighlightPattern;
		uniform half2 _Speed;
		uniform half2 _HighlightRepetition;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float clampResult94 = clamp( round( ( (0 + (ase_vertex3Pos.x - 0) * (1 - 0) / (_Max - 0)) + _Input ) ) , 0 , 1 );
			float4 lerpResult102 = lerp( _Color , ( _CompletionColor * clampResult94 ) , clampResult94);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNDotV74 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode74 = ( 0 + 1 * pow( 1.0 - fresnelNDotV74, 5 ) );
			float temp_output_80_0 = round( ( 1.0 - fresnelNode74 ) );
			o.Albedo = ( lerpResult102 * temp_output_80_0 ).rgb;
			float fresnelNDotV89 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode89 = ( 0 + 1 * pow( 1.0 - fresnelNDotV89, 3 ) );
			float mulTime47 = _Time.y * 1;
			float cos39 = cos( radians( -45.0 ) );
			float sin39 = sin( radians( -45.0 ) );
			float2 rotator39 = mul( (ase_worldPos).xyz.xy - float2( 0,0 ) , float2x2( cos39 , -sin39 , sin39 , cos39 )) + float2( 0,0 );
			float temp_output_73_0 = ( 1.0 - tex2D( _HighlightPattern, ( ( _Speed * mulTime47 ) + ( rotator39 * _HighlightRepetition ) ) ).r );
			float4 lerpResult53 = lerp( ( ( lerpResult102 + fresnelNode89 ) * temp_output_80_0 ) , ( _HighlightColor * temp_output_73_0 ) , temp_output_73_0);
			o.Emission = lerpResult53.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15101
7;29;1906;1004;1755.776;51.80573;1.275501;True;True
Node;AmplifyShaderEditor.PosVertexDataNode;122;564.8663,914.0233;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;98;615.8135,1108.482;Float;False;Property;_Max;Max;6;0;Create;True;0;0;False;0;2;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;91;674.6485,1221.946;Float;False;Property;_Input;Input;5;0;Create;True;0;0;False;0;0.6740544;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-710.4995,506.6423;Float;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;-45;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;123;796.253,906.0769;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;126;-912.6705,370.3853;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RadiansOpNode;45;-544,512;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;125;1023.361,1005.184;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;25;-640,400;Float;False;True;True;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RotatorNode;39;-384,400;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;49;-320,160;Half;False;Property;_Speed;Speed;4;0;Create;True;0;0;False;0;-0.25,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;26;-384,624;Half;False;Property;_HighlightRepetition;Highlight Repetition;1;0;Create;True;0;0;False;0;30,0;-0.001,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RoundOpNode;95;1262.13,944.9068;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;47;-288,288;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-128,224;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-128,448;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;100;1498.695,803.7155;Float;False;Property;_CompletionColor;CompletionColor;7;0;Create;True;0;0;False;0;1,0,0,0;1,0,0.2274509,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;94;1443.116,1003.077;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;1752.241,977.5841;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;64,320;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;1;716.0012,-48.43496;Fixed;False;Property;_Color;Color;2;0;Create;True;0;0;False;0;0,0.6689653,1,1;0.5647059,0.3607832,0.9333333,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;74;1040,352;Float;True;Tangent;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;224,304;Float;True;Property;_HighlightPattern;Highlight Pattern;0;0;Create;True;0;0;False;0;63efcc999428262488a50d488da90e3c;335a3012cca5e21499eacf86deb82736;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;89;1088,576;Float;False;Tangent;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;81;1336.799,366.2999;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;102;1918.913,779.7465;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;5;448,80;Float;False;Property;_HighlightColor;Highlight Color;3;0;Create;True;0;0;False;0;1,1,0,0;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;73;528,352;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RoundOpNode;80;1503.503,411.7996;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;76;1344,112;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;1696,112;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;829.2046,204.6141;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;53;1921.844,149.556;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;103;1699.352,1246.924;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;1664,-16;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2160,-16;Float;False;True;3;Float;ASEMaterialInspector;0;0;Standard;Space Game/Button Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;True;0.02;0,0,0,1;VertexOffset;False;False;Spherical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;123;0;122;1
WireConnection;123;2;98;0
WireConnection;45;0;38;0
WireConnection;125;0;123;0
WireConnection;125;1;91;0
WireConnection;25;0;126;0
WireConnection;39;0;25;0
WireConnection;39;2;45;0
WireConnection;95;0;125;0
WireConnection;48;0;49;0
WireConnection;48;1;47;0
WireConnection;27;0;39;0
WireConnection;27;1;26;0
WireConnection;94;0;95;0
WireConnection;101;0;100;0
WireConnection;101;1;94;0
WireConnection;50;0;48;0
WireConnection;50;1;27;0
WireConnection;28;1;50;0
WireConnection;81;0;74;0
WireConnection;102;0;1;0
WireConnection;102;1;101;0
WireConnection;102;2;94;0
WireConnection;73;0;28;1
WireConnection;80;0;81;0
WireConnection;76;0;102;0
WireConnection;76;1;89;0
WireConnection;85;0;76;0
WireConnection;85;1;80;0
WireConnection;54;0;5;0
WireConnection;54;1;73;0
WireConnection;53;0;85;0
WireConnection;53;1;54;0
WireConnection;53;2;73;0
WireConnection;103;0;94;0
WireConnection;88;0;102;0
WireConnection;88;1;80;0
WireConnection;0;0;88;0
WireConnection;0;2;53;0
ASEEND*/
//CHKSM=CF25BBF5102102F8A3AA18B27AF68D28023A3C1D