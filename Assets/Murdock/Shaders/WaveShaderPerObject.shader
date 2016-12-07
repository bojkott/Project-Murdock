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

				struct vertInput {
					float4 pos : POSITION;
				};

				struct vertOutput {
					float4 pos : POSITION;
					fixed3 worldPos : TEXCOORD1;
				};

				vertOutput vert(vertInput input) {
					vertOutput o;
					o.pos = mul(UNITY_MATRIX_MVP, input.pos);
					o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
					return o;
				}

				
				uniform int _WavesCount;
				uniform float3 _Waves[500];		// (x, y, z) = position
				uniform float _SpawnedIdWaveCount[100];	//Id from where it spawned
				uniform float _Radius[500];		// radius
				uniform float _Thickness[500];
				uniform float4 _Color[500];

				uniform float4 _ExtraColor;


				half4 frag(vertOutput output) : COLOR{
					// Loops over all the points
					float4 color = float4(0,0,0,1);
					
					//for (int i = 0; i < _WavesCount; i++)
					//{
					//	// Calculates the contribution of each point
					//	half dist = distance(output.worldPos, _Waves[i].xyz);

					//	if (dist < _Radius[i] && dist > _Radius[i]-_Thickness[i])
					//	{
					//		float shade = dist/(_Radius[i] - dist);
					//		if (_Radius[i] - dist > 0.2)
					//			color += _Color[i]*shade*_ExtraColor;
					//					
					//		
					//	}

					//}

					//color = color / _WavesCount;



					int waveIndex = 0;
					for (int i = 0; i < _WavesCount; i++) {

						float4 waveColor = float4(0, 0, 0, 1);

						for (int j = 0; j < _SpawnedIdWaveCount[i]; j++) {
							
							//Calculates the contribution of each point
								half dist = distance(output.worldPos, _Waves[waveIndex].xyz);

							if (dist < _Radius[waveIndex] && dist > _Radius[waveIndex] - _Thickness[waveIndex])
							{
								float shade = dist / (_Radius[waveIndex] - dist);
								if (_Radius[waveIndex] - dist > 0.2)
									waveColor += _Color[waveIndex] * shade*_ExtraColor;
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
