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
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.5
		struct Input
		{
			float4 screenPos;
			float3 worldPos;
			float3 worldNormal;
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
			fixed4 temp_output_1_0 = _Color;
			o.Albedo = temp_output_1_0.rgb;
			float mulTime47 = _Time.y * 1;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float cos39 = cos( radians( _Rotation ) );
			float sin39 = sin( radians( _Rotation ) );
			float2 rotator39 = mul( (ase_grabScreenPosNorm).xyzw.xy - float2( 0,0 ) , float2x2( cos39 , -sin39 , sin39 , cos39 )) + float2( 0,0 );
			float temp_output_59_0 = ( ( 1.0 - tex2D( _HighlightPattern, ( ( _Speed * mulTime47 ) + ( rotator39 * _HighlightRepetition ) ) ).r ) * (( (float)_Highlight >= 1 ) ? 1 :  0 ) );
			float4 lerpResult53 = lerp( _Color , ( _HighlightColor * temp_output_59_0 ) , temp_output_59_0);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNDotV74 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode74 = ( 0 + 1 * pow( 1.0 - fresnelNDotV74, 3.0 ) );
			float clampResult79 = clamp( fresnelNode74 , 0 , 0.5 );
			o.Emission = ( lerpResult53 + clampResult79 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.5
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15101
273;227;1064;634;1422.823;216.5847;1.859122;True;True
Node;AmplifyShaderEditor.GrabScreenPosition;22;-1024,208;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-784,320;Fixed;False;Property;_Rotation;Rotation;3;0;Create;True;0;0;False;0;-45;-20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;25;-688,208;Float;False;True;True;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RadiansOpNode;45;-608,320;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;39;-448,208;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;26;-432,432;Half;False;Property;_HighlightRepetition;Highlight Repetition;1;0;Create;True;0;0;False;0;30,0;100,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;49;-368,-32;Half;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;-0.25,0;-2,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;47;-352,96;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-176,32;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-176,256;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;16,128;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;224,304;Float;True;Property;_HighlightPattern;Highlight Pattern;0;0;Create;True;0;0;False;0;63efcc999428262488a50d488da90e3c;63efcc999428262488a50d488da90e3c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;55;334.9453,544.8234;Float;False;Property;_Highlight;Highlight;6;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.TFHCCompareGreaterEqual;58;527.8195,507.2829;Float;False;4;0;INT;0;False;1;FLOAT;1;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;73;528,352;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;709.1484,366.8145;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;448,80;Float;False;Property;_HighlightColor;Highlight Color;4;0;Create;True;0;0;False;0;1,1,0,0;1,0.7655174,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;77;880,384;Float;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;829.2046,204.6141;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;1;716.0012,-48.43496;Fixed;False;Property;_Color;Color;2;0;Create;True;0;0;False;0;1,0,0,1;1,0,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;74;1024,320;Float;True;Tangent;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;79;1280,320;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;53;1044.105,114.8121;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalizeNode;66;416,656;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;67;592,656;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;63;16,576;Float;False;World;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;68;864,656;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;76;1440,256;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;1024,656;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;65;272,656;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;62;320,768;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;72;544,864;Float;False;Property;_SpecularStrength;Specular Strength;8;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;64;16,768;Float;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;70;560,768;Float;False;Property;_SpecularMagnitude;Specular Magnitude;7;0;Create;True;0;0;False;0;4;15;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1664,160;Float;False;True;3;Float;ASEMaterialInspector;0;0;Standard;Space Game/Ship Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0.1;1,1,1,1;VertexScale;False;False;Spherical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
WireConnection;28;1;50;0
WireConnection;58;0;55;0
WireConnection;73;0;28;1
WireConnection;59;0;73;0
WireConnection;59;1;58;0
WireConnection;54;0;5;0
WireConnection;54;1;59;0
WireConnection;74;3;77;0
WireConnection;79;0;74;0
WireConnection;53;0;1;0
WireConnection;53;1;54;0
WireConnection;53;2;59;0
WireConnection;66;0;65;0
WireConnection;67;0;66;0
WireConnection;67;1;62;0
WireConnection;68;0;67;0
WireConnection;68;1;70;0
WireConnection;76;0;53;0
WireConnection;76;1;79;0
WireConnection;71;0;68;0
WireConnection;71;1;72;0
WireConnection;65;0;63;0
WireConnection;65;1;64;0
WireConnection;0;0;1;0
WireConnection;0;2;76;0
ASEEND*/
//CHKSM=E1497350B8619145AFF8AF81ED8AAC4E57C9DFE9