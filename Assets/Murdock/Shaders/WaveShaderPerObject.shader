// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/WaveShaderPerObject" {

		SubShader{

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
				uniform float3 _Waves[200];		// (x, y, z) = position
				uniform float _Radius[200];		// radius
				uniform float4 _Color[200];


				half4 frag(vertOutput output) : COLOR{
					// Loops over all the points
					float4 color = float4(0,0,0,1);
					
					for (int i = 0; i < _WavesCount; i++)
					{
						// Calculates the contribution of each point
						half dist = distance(output.worldPos, _Waves[i].xyz);

						if (dist < _Radius[i] && dist > _Radius[i]*0.3)
						{
							float shade = 0;
							if(dist < _Radius[i]*0.5 && dist > _Radius[i] * 0.3)
								shade = dist/(_Radius[i] - dist);
							color += _Color[i]*shade;
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
