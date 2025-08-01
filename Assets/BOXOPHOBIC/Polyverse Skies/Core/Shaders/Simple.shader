// Made with Amplify Shader Editor v1.9.8.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/Polyverse Skies/Simple"
{
	Properties
	{
		[StyledBanner(Polyverse Skies Simple)] _Banner( "< Banner >", Float ) = 1
		[StyledCategory(Background Settings, 5, 10)] _BackgroundCat( "[ Background Cat ]", Float ) = 1
		[KeywordEnum( Colors,Cubemap,Combined )] _BackgroundMode( "Background Mode", Float ) = 0
		[Space(10)] _SkyColor( "Sky Color", Color ) = ( 0.4980392, 0.7450981, 1, 1 )
		_EquatorColor( "Equator Color", Color ) = ( 1, 0.747, 0, 1 )
		_GroundColor( "Ground Color", Color ) = ( 0.4980392, 0.497, 0, 1 )
		_EquatorHeight( "Equator Height", Range( 0, 1 ) ) = 0.5
		_EquatorSmoothness( "Equator Smoothness", Range( 0.01, 1 ) ) = 0.5
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _BackgroundCubemap( "Background Cubemap", CUBE ) = "black" {}
		[Space(10)] _BackgroundExposure( "Background Exposure", Range( 0, 8 ) ) = 1
		[StyledCategory(Stars Settings)] _StarsCat( "[ Stars Cat ]", Float ) = 1
		[Toggle( _ENABLESTARS_ON )] _EnableStars( "Enable Stars", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _StarsCubemap( "Stars Cubemap", CUBE ) = "white" {}
		[Space(10)] _StarsSize( "Stars Size", Range( 0, 0.99 ) ) = 0.5
		_StarsIntensity( "Stars Intensity", Range( 0, 5 ) ) = 2
		_StarsHeightMask( "Stars Height Mask", Range( 0, 1 ) ) = 0
		[StyledToggle] _StarsBottomMask( "Stars Bottom Mask", Float ) = 0
		[StyledCategory(Sun Settings)] _SunCat( "[ Sun Cat ]", Float ) = 1
		[Toggle( _ENABLESUN_ON )] _EnableSun( "Enable Sun", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _SunTexture( "Sun Texture", 2D ) = "black" {}
		[Space(10)] _SunColor( "Sun Color", Color ) = ( 1, 1, 1, 1 )
		_SunSize( "Sun Size", Range( 0.1, 1 ) ) = 0.5
		_SunIntensity( "Sun Intensity", Range( 0, 10 ) ) = 1
		[Enum(Global From Script,0,Local From Material,1)] _SunDirectionMode( "Sun Direction", Float ) = 0
		[StyledVector(18)] _SunDirection( "Sun Direction", Vector ) = ( 0, 0, 1, 0 )
		[StyledCategory(Clouds Settings)] _CloudsCat( "[ Clouds Cat ]", Float ) = 1
		[Toggle( _ENABLECLOUDS_ON )] _EnableClouds( "Enable Clouds", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _CloudsCubemap( "Clouds Cubemap", CUBE ) = "black" {}
		[Space(10)] _CloudsIntensity( "Clouds Intensity", Range( 0, 1 ) ) = 1
		_CloudsHeight( "Clouds Height", Range( -0.5, 0.5 ) ) = 0
		_CloudsLightColor( "Clouds Light Color", Color ) = ( 1, 1, 1, 1 )
		_CloudsShadowColor( "Clouds Shadow Color", Color ) = ( 0.4980392, 0.7450981, 1, 1 )
		[StyledCategory(Fog Settings)] _FogCat( "[ Fog Cat ]", Float ) = 1
		[Toggle( _ENABLEBUILTINFOG_ON )] _EnableBuiltinFog( "Enable Fog", Float ) = 0
		[StyledMessage(Info, The fog color is controlled by the fog color set in the Lighting panel., _EnableBuiltinFog, 1, 10, 0)] _EnableFogMessage( "EnableFogMessage", Float ) = 0
		[Space(10)] _FogHeight( "Fog Height", Range( 0, 1 ) ) = 0
		_FogSmoothness( "Fog Smoothness", Range( 0.01, 1 ) ) = 0
		_FogFill( "Fog Fill", Range( 0, 1 ) ) = 0
		[StyledCategory(Skybox Settings)] _SkyboxCat( "[ Skybox Cat ]", Float ) = 1
		_SkyboxOffset( "Skybox Offset", Range( -1, 1 ) ) = 0
		_SkyboxRotation( "Skybox Rotation", Range( 0, 360 ) ) = 0
		[StyledVector(18)] _SkyboxRotationAxis( "Skybox Rotation Axis", Vector ) = ( 0, 1, 0, 0 )

	}

	SubShader
	{
		

		Tags { "RenderType"="Background" "Queue"="Background" "PreviewType"="Skybox" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		

		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			#define ASE_VERSION 19802


			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#pragma shader_feature_local _ENABLEBUILTINFOG_ON
			#pragma shader_feature_local _ENABLECLOUDS_ON
			#pragma shader_feature_local _ENABLESUN_ON
			#pragma shader_feature_local _ENABLESTARS_ON
			#pragma shader_feature_local _BACKGROUNDMODE_COLORS _BACKGROUNDMODE_CUBEMAP _BACKGROUNDMODE_COMBINED


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _SkyboxCat;
			uniform half _FogCat;
			uniform half _Banner;
			uniform float _EnableFogMessage;
			uniform half _SunCat;
			uniform half _BackgroundCat;
			uniform half _CloudsCat;
			uniform half _StarsCat;
			uniform half4 _EquatorColor;
			uniform half4 _GroundColor;
			uniform half4 _SkyColor;
			uniform float3 _SkyboxRotationAxis;
			uniform float _SkyboxOffset;
			uniform float _SkyboxRotation;
			uniform half _EquatorHeight;
			uniform half _EquatorSmoothness;
			uniform samplerCUBE _BackgroundCubemap;
			uniform half _BackgroundExposure;
			uniform half _StarsHeightMask;
			uniform samplerCUBE _StarsCubemap;
			uniform half _StarsSize;
			uniform half _StarsIntensity;
			uniform half _StarsBottomMask;
			uniform sampler2D _SunTexture;
			uniform half3 PolyverseSunDir;
			uniform half3 _SunDirection;
			uniform half _SunDirectionMode;
			uniform half _SunSize;
			uniform half4 _SunColor;
			uniform half _SunIntensity;
			uniform half4 _CloudsShadowColor;
			uniform half4 _CloudsLightColor;
			uniform samplerCUBE _CloudsCubemap;
			uniform half _CloudsHeight;
			uniform half _CloudsIntensity;
			uniform half _FogHeight;
			uniform half _FogSmoothness;
			uniform half _FogFill;


			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 normalizeResult46_g3 = normalize( _SkyboxRotationAxis );
				half3 RotationAxis42_g3 = normalizeResult46_g3;
				float3 appendResult1239 = (float3(0.0 , -_SkyboxOffset , 0.0));
				half3 VertexPos40_g3 = ( v.vertex.xyz + appendResult1239 );
				float dotResult48_g3 = dot( RotationAxis42_g3 , VertexPos40_g3 );
				float3 temp_output_47_0_g3 = ( RotationAxis42_g3 * dotResult48_g3 );
				float3 CPA50_g3 = temp_output_47_0_g3;
				half Angle44_g3 = radians( _SkyboxRotation );
				float3 normalizeResult1241 = normalize( ( CPA50_g3 + ( ( VertexPos40_g3 - CPA50_g3 ) * cos( Angle44_g3 ) ) + ( cross( RotationAxis42_g3 , ( VertexPos40_g3 - CPA50_g3 ) ) * sin( Angle44_g3 ) ) ) );
				float3 vertexToFrag1216 = normalizeResult1241;
				o.ase_texcoord1.xyz = vertexToFrag1216;
				#ifdef _ENABLESTARS_ON
				float staticSwitch1166 = saturate(  (0.1 + ( abs( v.vertex.xyz.y ) - 0.0 ) * ( 1.0 - 0.1 ) / ( _StarsHeightMask - 0.0 ) ) );
				#else
				float staticSwitch1166 = 0.0;
				#endif
				float vertexToFrag856 = staticSwitch1166;
				o.ase_texcoord1.w = vertexToFrag856;
				half3 VertexPos1217 = vertexToFrag1216;
				float3 break1223 = VertexPos1217;
				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float temp_output_673_0 = ( break1223.y * CAMERA_MODE300 );
				float3 appendResult675 = (float3(break1223.x , temp_output_673_0 , break1223.z));
				#ifdef _ENABLESTARS_ON
				float3 staticSwitch1165 = appendResult675;
				#else
				float3 staticSwitch1165 = float3( 0,0,0 );
				#endif
				float3 vertexToFrag763 = staticSwitch1165;
				o.ase_texcoord2.xyz = vertexToFrag763;
				float3 normalizeResult1258 = normalize( _SunDirection );
				float3 lerpResult1256 = lerp( PolyverseSunDir , normalizeResult1258 , _SunDirectionMode);
				float3 temp_output_962_0 = cross( -lerpResult1256 , half3( 0, 1, 0 ) );
				float3 normalizeResult967 = normalize( temp_output_962_0 );
				float dotResult968 = dot( normalizeResult967 , v.vertex.xyz );
				half3 GlobalSunDirection1005 = -lerpResult1256;
				float3 normalizeResult965 = normalize( cross( GlobalSunDirection1005 , temp_output_962_0 ) );
				float dotResult969 = dot( normalizeResult965 , v.vertex.xyz );
				float2 appendResult970 = (float2(dotResult968 , dotResult969));
				float2 break972 = appendResult970;
				float2 appendResult980 = (float2(break972.x , ( break972.y * CAMERA_MODE300 )));
				float lerpResult1246 = lerp( 20.0 , 2.0 , _SunSize);
				#ifdef _ENABLESUN_ON
				float2 staticSwitch1168 = (( appendResult980 * lerpResult1246 )*0.5 + 0.5);
				#else
				float2 staticSwitch1168 = float2( 0,0 );
				#endif
				float2 vertexToFrag993 = staticSwitch1168;
				o.ase_texcoord3.xy = vertexToFrag993;
				float dotResult988 = dot( GlobalSunDirection1005 , v.vertex.xyz );
				#ifdef _ENABLESUN_ON
				float staticSwitch1169 = saturate( dotResult988 );
				#else
				float staticSwitch1169 = 0.0;
				#endif
				float vertexToFrag997 = staticSwitch1169;
				o.ase_texcoord2.w = vertexToFrag997;
				float3 break1225 = VertexPos1217;
				float3 appendResult246 = (float3(break1225.x , ( ( break1225.y + ( _CloudsHeight * -1.0 ) ) * CAMERA_MODE300 ) , break1225.z));
				#ifdef _ENABLECLOUDS_ON
				float3 staticSwitch1163 = appendResult246;
				#else
				float3 staticSwitch1163 = float3( 0,0,0 );
				#endif
				float3 vertexToFrag1133 = staticSwitch1163;
				o.ase_texcoord4.xyz = vertexToFrag1133;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				o.ase_texcoord4.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}

			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float3 vertexToFrag1216 = i.ase_texcoord1.xyz;
				half3 VertexPos1217 = vertexToFrag1216;
				float4 lerpResult180 = lerp( _GroundColor , _SkyColor , step( 0.0 , VertexPos1217.y ));
				float temp_output_7_0_g7 = 0.0;
				float saferPower470 = abs( saturate( ( ( abs( VertexPos1217.y ) - temp_output_7_0_g7 ) / ( _EquatorHeight - temp_output_7_0_g7 ) ) ) );
				float4 lerpResult288 = lerp( _EquatorColor , lerpResult180 , pow( saferPower470 , ( 1.0 - _EquatorSmoothness ) ));
				half4 SKY218 = lerpResult288;
				half4 BACKGROUND1202 = ( texCUBE( _BackgroundCubemap, VertexPos1217 ) * _BackgroundExposure );
				#if defined( _BACKGROUNDMODE_COLORS )
				float4 staticSwitch1207 = SKY218;
				#elif defined( _BACKGROUNDMODE_CUBEMAP )
				float4 staticSwitch1207 = BACKGROUND1202;
				#elif defined( _BACKGROUNDMODE_COMBINED )
				float4 staticSwitch1207 = ( SKY218 * BACKGROUND1202 );
				#else
				float4 staticSwitch1207 = SKY218;
				#endif
				float vertexToFrag856 = i.ase_texcoord1.w;
				float3 vertexToFrag763 = i.ase_texcoord2.xyz;
				float3 break1223 = VertexPos1217;
				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float temp_output_673_0 = ( break1223.y * CAMERA_MODE300 );
				half Starts_Bottom_Mask1230 = step( 0.0 , temp_output_673_0 );
				float lerpResult1234 = lerp( 1.0 , saturate( Starts_Bottom_Mask1230 ) , _StarsBottomMask);
				half STARS630 = ( floor( ( vertexToFrag856 * ( texCUBE( _StarsCubemap, vertexToFrag763 ).g + _StarsSize ) ) ) * _StarsIntensity * lerpResult1234 );
				#ifdef _ENABLESTARS_ON
				float4 staticSwitch1170 = ( staticSwitch1207 + STARS630 );
				#else
				float4 staticSwitch1170 = staticSwitch1207;
				#endif
				float2 vertexToFrag993 = i.ase_texcoord3.xy;
				float4 tex2DNode995 = tex2D( _SunTexture, vertexToFrag993 );
				half4 SUN1004 = ( tex2DNode995.r * _SunColor * max( _SunIntensity , 1.0 ) );
				float vertexToFrag997 = i.ase_texcoord2.w;
				half SUN_MASK1003 = ( tex2DNode995.a * saturate( _SunIntensity ) * vertexToFrag997 );
				float4 lerpResult176 = lerp( staticSwitch1170 , SUN1004 , SUN_MASK1003);
				#ifdef _ENABLESUN_ON
				float4 staticSwitch1167 = lerpResult176;
				#else
				float4 staticSwitch1167 = staticSwitch1170;
				#endif
				float3 vertexToFrag1133 = i.ase_texcoord4.xyz;
				float4 texCUBENode41 = texCUBE( _CloudsCubemap, vertexToFrag1133 );
				float4 lerpResult101 = lerp( _CloudsShadowColor , _CloudsLightColor , texCUBENode41);
				half4 CLOUDS222 = lerpResult101;
				half CLOUDS_MASK223 = ( texCUBENode41.a * _CloudsIntensity );
				float4 lerpResult227 = lerp( staticSwitch1167 , CLOUDS222 , CLOUDS_MASK223);
				#ifdef _ENABLECLOUDS_ON
				float4 staticSwitch1162 = lerpResult227;
				#else
				float4 staticSwitch1162 = staticSwitch1167;
				#endif
				float temp_output_7_0_g8 = 0.0;
				float lerpResult678 = lerp( saturate( pow( ( ( abs( VertexPos1217.y ) - temp_output_7_0_g8 ) / ( _FogHeight - temp_output_7_0_g8 ) ) , ( 1.0 - _FogSmoothness ) ) ) , 0.0 , _FogFill);
				half FOG_MASK359 = lerpResult678;
				float4 lerpResult317 = lerp( unity_FogColor , staticSwitch1162 , FOG_MASK359);
				#ifdef _ENABLEBUILTINFOG_ON
				float4 staticSwitch921 = lerpResult317;
				#else
				float4 staticSwitch921 = staticSwitch1162;
				#endif
				

				finalColor = staticSwitch921;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "PolyverseSkiesAsset.MaterialGUI"
	
	Fallback Off
}
/*ASEBEGIN
Version=19802
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1236;-896,-4592;Inherit;False;Property;_SkyboxOffset;Skybox Offset;39;0;Create;True;0;0;0;False;0;False;0;0.21;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1237;-608,-4592;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1238;-896,-4736;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1239;-448,-4672;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1211;-896,-4320;Inherit;False;Property;_SkyboxRotation;Skybox Rotation;40;0;Create;True;0;0;0;False;0;False;0;0.115;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RadiansOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1251;-608,-4320;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1212;-896,-4480;Inherit;False;Property;_SkyboxRotationAxis;Skybox Rotation Axis;41;0;Create;True;0;0;0;False;1;StyledVector(18);False;0,1,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1240;-256,-4736;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1259;-1408,-864;Half;False;Property;_SunDirection;Sun Direction;24;0;Create;True;0;0;0;False;1;StyledVector(18);False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1252;-64,-4736;Inherit;False;Compute Rotation;-1;;3;d0fbe60918b2bc146b660122744f3d02;0;4;38;FLOAT3;0,0,0;False;72;FLOAT3;0,0,0;False;41;FLOAT3;0,0,0;False;43;FLOAT;0;False;1;FLOAT3;19
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1258;-1232,-864;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1257;-1408,-1024;Half;False;Global;PolyverseSunDir;PolyverseSunDir;38;0;Create;True;0;0;0;False;0;False;0,0,0;-0.08641268,-0.4458854,0.8909091;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1260;-1408,-704;Half;False;Property;_SunDirectionMode;Sun Direction;23;1;[Enum];Create;False;0;2;Global From Script;0;Local From Material;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OrthoParams, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;267;-896,-5248;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1241;192,-4736;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1256;-1088,-1024;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;961;-896,-864;Half;False;Constant;_Vector2;Vector 2;9;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1007;-448,-5248;Half;False;Constant;_Float7;Float 7;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1216;384,-4736;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleDivideOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;309;-592,-5248;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1255;-896,-1024;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1217;640,-4736;Half;False;VertexPos;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1005;-640,-1024;Half;False;GlobalSunDirection;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;268;-256,-5248;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0.5;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;962;-576,-896;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1222;-896,-1792;Inherit;False;1217;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;300;64,-5248;Half;False;CAMERA_MODE;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;964;-384,-1024;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1220;-896,-2944;Inherit;False;1217;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;967;-384,-896;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;965;-192,-1024;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1176;-384,-640;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1223;-704,-1792;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;674;-896,-1600;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;968;0,-896;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1221;-704,-2944;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1175;1024,-1792;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;969;0,-1024;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;673;-640,-1616;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1171;1648,-1696;Half;False;Constant;_Float0;Float 0;47;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1017;1408,-1680;Half;False;Constant;_Float17;Float 17;47;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;970;160,-1024;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;831;1600,-1600;Half;False;Property;_StarsHeightMask;Stars Height Mask;15;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;212;-384,-2496;Half;False;Property;_EquatorHeight;Equator Height;6;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;675;-448,-1792;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.AbsOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;828;1664,-1792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1018;1408,-1600;Half;False;Constant;_Float18;Float 18;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;287;-192,-2944;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;971;320,-896;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;832;1920,-1792;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;471;-384,-2368;Half;False;Property;_EquatorSmoothness;Equator Smoothness;7;0;Create;True;0;0;0;False;0;False;0.5;0.5;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;972;320,-1024;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1165;-208,-1744;Float;False;Property;_ENABLESTARS_ON;Enable Stars;11;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1170;False;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1254;0,-2560;Inherit;False;Remap To 0-1;-1;;7;d6eb10c035ec242479a0519a8ea4fd0f;0;3;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;208;224,-2560;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;763;48,-1728;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;475;-64,-2368;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;822;2112,-1792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1244;-512,-2432;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1243;-448,-1616;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;976;640,-928;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;975;0,-768;Half;False;Property;_SunSize;Sun Size;21;0;Create;True;0;0;0;False;0;False;0.5;0.5;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;980;832,-1024;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1230;-208,-1600;Half;False;Starts_Bottom_Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;182;640,-2592;Half;False;Property;_SkyColor;Sky Color;3;0;Create;True;0;0;0;False;1;Space(10);False;0.4980392,0.7450981,1,1;0.4980389,0.7450981,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1224;-896,0;Inherit;False;1217;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1090;-896,320;Half;False;Constant;_Float31;Float 31;53;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1219;-896,-3584;Inherit;False;1217;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;194;640,-2768;Half;False;Property;_GroundColor;Ground Color;5;0;Create;True;0;0;0;False;0;False;0.4980392,0.497,0,1;0.4980389,0.497,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;241;-896,192;Half;False;Property;_CloudsHeight;Clouds Height;29;0;Create;True;0;0;0;False;0;False;0;0;-0.5;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;619;384,-1600;Half;False;Property;_StarsSize;Stars Size;13;0;Create;True;0;0;0;False;1;Space(10);False;0.5;0.5;0;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;564;384,-1792;Inherit;True;Property;_StarsCubemap;Stars Cubemap;12;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;4136c4d5d7422cf49a21b885d09f4518;True;0;False;white;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1166;2305.3,-1744;Float;False;Property;_ENABLESTARS_ON;Enable Stars;11;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1170;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;470;400,-2384;Inherit;False;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1246;640,-768;Inherit;False;3;0;FLOAT;20;False;1;FLOAT;2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;303;896,-2448;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;626;768,-1552;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1231;3200,-1536;Inherit;False;1230;Starts_Bottom_Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;278;-512,256;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1225;-704,0;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;856;2560,-1728;Inherit;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;985;1024,-1024;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;180;1024,-2752;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1028;2816,-640;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;181;640,-2944;Half;False;Property;_EquatorColor;Equator Color;4;0;Create;True;0;0;0;False;0;False;1,0.747,0,1;1,0.747,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1177;2816,-512;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RelayNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;417;1152,-2368;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1200;-640,-3392;Half;False;Property;_BackgroundExposure;Background Exposure;9;0;Create;True;0;0;0;False;1;Space(10);False;1;1;0;8;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1199;-640,-3584;Inherit;True;Property;_BackgroundCubemap;Background Cubemap;8;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;None;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1201;-256,-3584;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1152;-256,320;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;988;3072,-640;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1232;3200,-1472;Half;False;Property;_StarsBottomMask;Stars Bottom Mask;16;0;Create;True;0;0;0;False;1;StyledToggle;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1233;3440,-1536;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;244;-256,192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;876;2944,-1584;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;288;1280,-2944;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1247;1280,-1024;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1151;0,192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;994;3232,-640;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;629;3200,-1664;Half;False;Property;_StarsIntensity;Stars Intensity;14;0;Create;True;0;0;0;False;0;False;2;2.71;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1202;256,-3584;Half;False;BACKGROUND;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;218;1664,-2944;Half;False;SKY;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1168;1568,-976;Float;False;Property;_ENABLESUN_ON;Enable Sun;18;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1167;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FloorOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;886;3200,-1792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1234;3584,-1536;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1169;3392,-592;Float;False;Property;_ENABLESUN_ON;Enable Sun;18;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1167;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;993;1808,-960;Inherit;False;False;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1204;-896,-6576;Inherit;False;1202;BACKGROUND;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;246;128,0;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;996;2176,-576;Half;False;Property;_SunIntensity;Sun Intensity;22;0;Create;True;0;0;0;False;0;False;1;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1205;-896,-6656;Inherit;False;218;SKY;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;882;3840,-1792;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1226;-896,896;Inherit;False;1217;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1227;-704,896;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1206;-640,-6528;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1249;2688,-832;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1250;2560,-576;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1163;384,80;Float;False;Property;_ENABLECLOUDS_ON;Enable Clouds;26;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1162;False;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;630;4032,-1792;Half;False;STARS;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;997;3632,-576;Inherit;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;998;2176,-768;Half;False;Property;_SunColor;Sun Color;20;0;Create;True;0;0;0;False;1;Space(10);False;1,1,1,1;1,1,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;995;2176,-1024;Inherit;True;Property;_SunTexture;Sun Texture;19;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1133;640,96;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1207;-384,-6656;Float;False;Property;_BackgroundMode;Background Mode;2;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;3;Colors;Cubemap;Combined;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.AbsOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;314;-512,896;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;632;-256,-6336;Inherit;False;630;STARS;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;325;-896,1216;Half;False;Property;_FogSmoothness;Fog Smoothness;36;0;Create;True;0;0;0;False;0;False;0;0;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1002;2688,-1024;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;313;-896,1088;Half;False;Property;_FogHeight;Fog Height;35;0;Create;True;0;0;0;False;1;Space(10);False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1001;4096,-928;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;41;1024,0;Inherit;True;Property;_CloudsCubemap;Clouds Cubemap;27;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;5bf545dec997743419de1df4195b3a0d;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1004;4352,-1024;Half;False;SUN;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1003;4352,-768;Half;False;SUN_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;329;-256,1216;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1229;1024,256;Half;False;Property;_CloudsIntensity;Clouds Intensity;28;0;Create;True;0;0;0;False;1;Space(10);False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;631;80,-6528;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;232;1792,0;Half;False;Property;_CloudsShadowColor;Clouds Shadow Color;31;0;Create;True;0;0;0;False;0;False;0.4980392,0.7450981,1,1;1,0.1853116,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;261;1792,224;Half;False;Property;_CloudsLightColor;Clouds Light Color;30;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1253;-320,896;Inherit;False;Remap To 0-1;-1;;8;d6eb10c035ec242479a0519a8ea4fd0f;0;3;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1228;1377.863,200.6354;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;101;2176,0;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;677;-64,896;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1170;256,-6656;Float;False;Property;_EnableStars;Enable Stars;11;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1112;256,-6432;Inherit;False;1004;SUN;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1111;256,-6336;Inherit;False;1003;SUN_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;222;2560,0;Half;False;CLOUDS;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1110;128,1088;Half;False;Constant;_Float41;Float 41;55;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;176;576,-6528;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;679;128,1216;Half;False;Property;_FogFill;Fog Fill;37;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;316;128,896;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;223;1536,128;Half;False;CLOUDS_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;228;768,-6432;Inherit;False;222;CLOUDS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;229;768,-6336;Inherit;False;223;CLOUDS_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;678;384,896;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1167;768,-6656;Float;False;Property;_EnableSun;Enable Sun;18;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;359;640,896;Half;False;FOG_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;227;1088,-6528;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;436;1280,-6336;Inherit;False;359;FOG_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FogAndAmbientColorsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;312;1280,-6432;Inherit;False;unity_FogColor;0;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1162;1280,-6656;Float;False;Property;_EnableClouds;Enable Clouds;26;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;317;1664,-6528;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1218;256,-7296;Half;False;Property;_SkyboxCat;[ Skybox Cat ];38;0;Create;True;0;0;0;True;1;StyledCategory(Skybox Settings);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1192;96,-7296;Half;False;Property;_FogCat;[ Fog Cat ];32;0;Create;True;0;0;0;True;1;StyledCategory(Fog Settings);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1180;-896,-7296;Half;False;Property;_Banner;< Banner >;0;0;Create;True;0;0;0;True;1;StyledBanner(Polyverse Skies Simple);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1209;-896,1344;Inherit;False;Property;_EnableFogMessage;EnableFogMessage;34;0;Create;True;0;0;0;True;1;StyledMessage(Info, The fog color is controlled by the fog color set in the Lighting panel., _EnableBuiltinFog, 1, 10, 0);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1190;-240,-7296;Half;False;Property;_SunCat;[ Sun Cat ];17;0;Create;True;0;0;0;True;1;StyledCategory(Sun Settings);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;921;1920,-6656;Float;False;Property;_EnableBuiltinFog;Enable Fog;33;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1203;-640,-7296;Half;False;Property;_BackgroundCat;[ Background Cat ];1;0;Create;True;0;0;0;True;1;StyledCategory(Background Settings, 5, 10);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1191;-80,-7296;Half;False;Property;_CloudsCat;[ Clouds Cat ];25;0;Create;True;0;0;0;True;1;StyledCategory(Clouds Settings);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1189;-416,-7296;Half;False;Property;_StarsCat;[ Stars Cat ];10;0;Create;True;0;0;0;True;1;StyledCategory(Stars Settings);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1197;2432,-6656;Float;False;True;-1;2;PolyverseSkiesAsset.MaterialGUI;100;5;BOXOPHOBIC/Polyverse Skies/Simple;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;True;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;True;3;RenderType=Background=RenderType;Queue=Background=Queue=0;PreviewType=Skybox;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1193;-896,-6784;Inherit;False;3583.845;100;Final;0;FINAL OUTPUT;0.4980392,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;914;1792,-128;Inherit;False;515;100;Cloud Colors;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;991;2176,-1152;Inherit;False;517;100;Sun Texture, Color and Intensity;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;480;640,-3072;Inherit;False;774.4041;100;Color Gradient Colors;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1159;384,-1920;Inherit;False;273;100;Stars Cubemap;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1026;2816,-768;Inherit;False;1028.332;100;Direction Negative Z Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;952;1024,-1920;Inherit;False;3200.168;100;Stars Horizon Height Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1188;-896,-7424;Inherit;False;1276.25;100;Drawers;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1195;-896,-3072;Inherit;False;1406.953;100;Color Gradient Calculation;0;GRADIENT;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1154;-896,-128;Inherit;False;1791.123;100;Clouds Coordinates;0;CLOUDS;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1006;-896,-1152;Inherit;False;2940.454;100;Calculate Sun Position;0;SUN;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1194;-896,-5376;Inherit;False;771;100;Switch between Perspective / Orthographic camera;0;CAMERA MODE;1,0,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1155;1024,-128;Inherit;False;579;100;Clouds Cubemap;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1157;-896,-1920;Inherit;False;1150;100;Stars Cubemaps Coords;0;STARS;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;700;-896,768;Inherit;False;1415.783;100;Fog Coords on Screen;0;BUILT-IN FOG;0,0.4980392,0,1;0;0
WireConnection;1237;0;1236;0
WireConnection;1239;1;1237;0
WireConnection;1251;0;1211;0
WireConnection;1240;0;1238;0
WireConnection;1240;1;1239;0
WireConnection;1252;38;1240;0
WireConnection;1252;41;1212;0
WireConnection;1252;43;1251;0
WireConnection;1258;0;1259;0
WireConnection;1241;0;1252;19
WireConnection;1256;0;1257;0
WireConnection;1256;1;1258;0
WireConnection;1256;2;1260;0
WireConnection;1216;0;1241;0
WireConnection;309;0;267;2
WireConnection;309;1;267;1
WireConnection;1255;0;1256;0
WireConnection;1217;0;1216;0
WireConnection;1005;0;1255;0
WireConnection;268;0;1007;0
WireConnection;268;1;309;0
WireConnection;268;2;267;4
WireConnection;962;0;1255;0
WireConnection;962;1;961;0
WireConnection;300;0;268;0
WireConnection;964;0;1005;0
WireConnection;964;1;962;0
WireConnection;967;0;962;0
WireConnection;965;0;964;0
WireConnection;1223;0;1222;0
WireConnection;968;0;967;0
WireConnection;968;1;1176;0
WireConnection;1221;0;1220;0
WireConnection;969;0;965;0
WireConnection;969;1;1176;0
WireConnection;673;0;1223;1
WireConnection;673;1;674;0
WireConnection;970;0;968;0
WireConnection;970;1;969;0
WireConnection;675;0;1223;0
WireConnection;675;1;673;0
WireConnection;675;2;1223;2
WireConnection;828;0;1175;2
WireConnection;287;0;1221;1
WireConnection;832;0;828;0
WireConnection;832;1;1017;0
WireConnection;832;2;831;0
WireConnection;832;3;1171;0
WireConnection;832;4;1018;0
WireConnection;972;0;970;0
WireConnection;1165;0;675;0
WireConnection;1254;6;287;0
WireConnection;1254;8;212;0
WireConnection;208;0;1254;0
WireConnection;763;0;1165;0
WireConnection;475;0;471;0
WireConnection;822;0;832;0
WireConnection;1244;1;1221;1
WireConnection;1243;1;673;0
WireConnection;976;0;972;1
WireConnection;976;1;971;0
WireConnection;980;0;972;0
WireConnection;980;1;976;0
WireConnection;1230;0;1243;0
WireConnection;564;1;763;0
WireConnection;1166;0;822;0
WireConnection;470;0;208;0
WireConnection;470;1;475;0
WireConnection;1246;2;975;0
WireConnection;303;0;1244;0
WireConnection;626;0;564;2
WireConnection;626;1;619;0
WireConnection;278;0;241;0
WireConnection;278;1;1090;0
WireConnection;1225;0;1224;0
WireConnection;856;0;1166;0
WireConnection;985;0;980;0
WireConnection;985;1;1246;0
WireConnection;180;0;194;0
WireConnection;180;1;182;0
WireConnection;180;2;303;0
WireConnection;417;0;470;0
WireConnection;1199;1;1219;0
WireConnection;1201;0;1199;0
WireConnection;1201;1;1200;0
WireConnection;988;0;1028;0
WireConnection;988;1;1177;0
WireConnection;1233;0;1231;0
WireConnection;244;0;1225;1
WireConnection;244;1;278;0
WireConnection;876;0;856;0
WireConnection;876;1;626;0
WireConnection;288;0;181;0
WireConnection;288;1;180;0
WireConnection;288;2;417;0
WireConnection;1247;0;985;0
WireConnection;1151;0;244;0
WireConnection;1151;1;1152;0
WireConnection;994;0;988;0
WireConnection;1202;0;1201;0
WireConnection;218;0;288;0
WireConnection;1168;0;1247;0
WireConnection;886;0;876;0
WireConnection;1234;1;1233;0
WireConnection;1234;2;1232;0
WireConnection;1169;0;994;0
WireConnection;993;0;1168;0
WireConnection;246;0;1225;0
WireConnection;246;1;1151;0
WireConnection;246;2;1225;2
WireConnection;882;0;886;0
WireConnection;882;1;629;0
WireConnection;882;2;1234;0
WireConnection;1227;0;1226;0
WireConnection;1206;0;1205;0
WireConnection;1206;1;1204;0
WireConnection;1249;0;996;0
WireConnection;1250;0;996;0
WireConnection;1163;0;246;0
WireConnection;630;0;882;0
WireConnection;997;0;1169;0
WireConnection;995;1;993;0
WireConnection;1133;0;1163;0
WireConnection;1207;1;1205;0
WireConnection;1207;0;1204;0
WireConnection;1207;2;1206;0
WireConnection;314;0;1227;1
WireConnection;1002;0;995;1
WireConnection;1002;1;998;0
WireConnection;1002;2;1250;0
WireConnection;1001;0;995;4
WireConnection;1001;1;1249;0
WireConnection;1001;2;997;0
WireConnection;41;1;1133;0
WireConnection;1004;0;1002;0
WireConnection;1003;0;1001;0
WireConnection;329;0;325;0
WireConnection;631;0;1207;0
WireConnection;631;1;632;0
WireConnection;1253;6;314;0
WireConnection;1253;8;313;0
WireConnection;1228;0;41;4
WireConnection;1228;1;1229;0
WireConnection;101;0;232;0
WireConnection;101;1;261;0
WireConnection;101;2;41;0
WireConnection;677;0;1253;0
WireConnection;677;1;329;0
WireConnection;1170;1;1207;0
WireConnection;1170;0;631;0
WireConnection;222;0;101;0
WireConnection;176;0;1170;0
WireConnection;176;1;1112;0
WireConnection;176;2;1111;0
WireConnection;316;0;677;0
WireConnection;223;0;1228;0
WireConnection;678;0;316;0
WireConnection;678;1;1110;0
WireConnection;678;2;679;0
WireConnection;1167;1;1170;0
WireConnection;1167;0;176;0
WireConnection;359;0;678;0
WireConnection;227;0;1167;0
WireConnection;227;1;228;0
WireConnection;227;2;229;0
WireConnection;1162;1;1167;0
WireConnection;1162;0;227;0
WireConnection;317;0;312;0
WireConnection;317;1;1162;0
WireConnection;317;2;436;0
WireConnection;921;1;1162;0
WireConnection;921;0;317;0
WireConnection;1197;0;921;0
ASEEND*/
//CHKSM=5E6F7B2ADF6C668A5728E7A0768D4964943952D7