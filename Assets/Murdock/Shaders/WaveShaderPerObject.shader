// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/WaveShaderPerObject" {
	Properties{
		_ExtraColor("Main Color", Color) = (1,1,1,1)
	}
		SubShader{
			Cull Off
			Blend One Zero
			Pass{
				CGPROGRAM
				
				#pragma vertex vert             
				#pragma fragment frag 
				#include "UnityCG.cginc"

				uniform int _WavesCount;
				uniform float3 _Waves[500];		// (x, y, z) = position
				uniform float _SpawnedIdWaveCount[100];	//Id from where it spawned
				uniform float _Radius[500];		// radius
				uniform float _Thickness[500];
				uniform float4 _Color[500];

				uniform float4 _ExtraColor;


				

				struct vertOutput {
					float4 pos : POSITION;
					float3 worldNormal: TEXCOORD0;
					fixed3 worldPos : TEXCOORD1;
				};

				vertOutput vert(appdata_base input) {
					vertOutput o;
					o.pos = mul(UNITY_MATRIX_MVP, input.vertex);
					o.worldNormal = UnityObjectToWorldNormal(input.normal);
					
					o.worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
					return o;
				}

				
				


				half4 frag(vertOutput output) : COLOR{
					// Loops over all the points
					float4 color = float4(0,0,0,1);
					


					int waveIndex = 0;
					for (int i = 0; i < _WavesCount; i++) {

						float4 waveColor = float4(0, 0, 0, 1);

						for (int j = 0; j < _SpawnedIdWaveCount[i]; j++) {
							
							//Calculates the contribution of each point
								half dist = distance(output.worldPos, _Waves[waveIndex].xyz);

							if (dist < _Radius[waveIndex] && dist > _Radius[waveIndex] - _Thickness[waveIndex])
							{
								half3 waveDir = output.worldPos - _Waves[waveIndex];
								float diff = dot(output.worldNormal, -waveDir);
								diff = abs(diff);
								waveColor += _Color[waveIndex] *_ExtraColor * 0.5f + diff*_Color[waveIndex] * _ExtraColor;
								
									
							}
							waveIndex += 1;
						}
						color += waveColor / _SpawnedIdWaveCount[i];
						
					}


					return color;
				// Converts (0-1) according to the heat texture

				
				}
					ENDCG
				}
	}
	Fallback "Diffuse"
}
