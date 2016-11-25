// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/WaveShaderPerObject" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
	}
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

				
				uniform int _WavesCount = 0;
				uniform float3 _Waves[20];		// (x, y, z) = position
				uniform float _Radius[20];		// radius
				uniform float4 _Color[20];


				half4 frag(vertOutput output) : COLOR{
					// Loops over all the points
					half4 color = (0,0,0,0);
					half w = 0;
				for (int i = 0; i < _WavesCount; i++)
				{
					// Calculates the contribution of each point
					half dist = distance(output.worldPos, _Waves[i].xyz);

					if (dist < _Radius[i])
					{
						color = _Color[i];
					}

				}

				// Converts (0-1) according to the heat texture

				return color;
				}
					ENDCG
				}
	}
	Fallback "Diffuse"
}
