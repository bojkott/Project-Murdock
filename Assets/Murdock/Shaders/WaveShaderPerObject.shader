// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/WaveShaderPerObject" {

		SubShader{
			Cull Off
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
				uniform float3 _Waves[400];		// (x, y, z) = position
				uniform float _Radius[400];		// radius
				uniform float _Thickness[400];
				uniform float4 _Color[400];


				half4 frag(vertOutput output) : COLOR{
					// Loops over all the points
					float4 color = float4(0,0,0,1);
					
					for (int i = 0; i < _WavesCount; i++)
					{
						// Calculates the contribution of each point
						half dist = distance(output.worldPos, _Waves[i].xyz);

						if (dist < _Radius[i] && dist > _Radius[i]-_Thickness[i])
						{
							float shade = dist/(_Radius[i] - dist);
							if (_Radius[i] - dist > 0.2)
								color += _Color[i]*shade * 0.3;
										
							
						}

					}

					return color ;

				// Converts (0-1) according to the heat texture

				
				}
					ENDCG
				}
	}
	Fallback "Diffuse"
}
