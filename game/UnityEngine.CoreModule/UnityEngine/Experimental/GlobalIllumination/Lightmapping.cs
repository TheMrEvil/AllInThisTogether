using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000465 RID: 1125
	public static class Lightmapping
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x000427BC File Offset: 0x000409BC
		[RequiredByNativeCode]
		public static void SetDelegate(Lightmapping.RequestLightsDelegate del)
		{
			Lightmapping.s_RequestLightsDelegate = ((del != null) ? del : Lightmapping.s_DefaultDelegate);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000427D0 File Offset: 0x000409D0
		[RequiredByNativeCode]
		public static Lightmapping.RequestLightsDelegate GetDelegate()
		{
			return Lightmapping.s_RequestLightsDelegate;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000427E7 File Offset: 0x000409E7
		[RequiredByNativeCode]
		public static void ResetDelegate()
		{
			Lightmapping.s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000427F4 File Offset: 0x000409F4
		[RequiredByNativeCode]
		internal unsafe static void RequestLights(Light[] lights, IntPtr outLightsPtr, int outLightsCount)
		{
			NativeArray<LightDataGI> lightsOutput = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LightDataGI>((void*)outLightsPtr, outLightsCount, Allocator.None);
			Lightmapping.s_RequestLightsDelegate(lights, lightsOutput);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0004281D File Offset: 0x00040A1D
		// Note: this type is marked as 'beforefieldinit'.
		static Lightmapping()
		{
		}

		// Token: 0x04000EC3 RID: 3779
		[RequiredByNativeCode]
		private static readonly Lightmapping.RequestLightsDelegate s_DefaultDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			DirectionalLight directionalLight = default(DirectionalLight);
			PointLight pointLight = default(PointLight);
			SpotLight spotLight = default(SpotLight);
			RectangleLight rectangleLight = default(RectangleLight);
			DiscLight discLight = default(DiscLight);
			Cookie cookie = default(Cookie);
			LightDataGI value = default(LightDataGI);
			for (int i = 0; i < requests.Length; i++)
			{
				Light light = requests[i];
				switch (light.type)
				{
				case LightType.Spot:
					LightmapperUtils.Extract(light, ref spotLight);
					LightmapperUtils.Extract(light, out cookie);
					value.Init(ref spotLight, ref cookie);
					break;
				case LightType.Directional:
					LightmapperUtils.Extract(light, ref directionalLight);
					LightmapperUtils.Extract(light, out cookie);
					value.Init(ref directionalLight, ref cookie);
					break;
				case LightType.Point:
					LightmapperUtils.Extract(light, ref pointLight);
					LightmapperUtils.Extract(light, out cookie);
					value.Init(ref pointLight, ref cookie);
					break;
				case LightType.Area:
					LightmapperUtils.Extract(light, ref rectangleLight);
					LightmapperUtils.Extract(light, out cookie);
					value.Init(ref rectangleLight, ref cookie);
					break;
				case LightType.Disc:
					LightmapperUtils.Extract(light, ref discLight);
					LightmapperUtils.Extract(light, out cookie);
					value.Init(ref discLight, ref cookie);
					break;
				default:
					value.InitNoBake(light.GetInstanceID());
					break;
				}
				lightsOutput[i] = value;
			}
		};

		// Token: 0x04000EC4 RID: 3780
		[RequiredByNativeCode]
		private static Lightmapping.RequestLightsDelegate s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;

		// Token: 0x02000466 RID: 1126
		// (Invoke) Token: 0x060027EC RID: 10220
		public delegate void RequestLightsDelegate(Light[] requests, NativeArray<LightDataGI> lightsOutput);

		// Token: 0x02000467 RID: 1127
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060027EF RID: 10223 RVA: 0x0004283E File Offset: 0x00040A3E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060027F0 RID: 10224 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x060027F1 RID: 10225 RVA: 0x0004284C File Offset: 0x00040A4C
			internal void <.cctor>b__7_0(Light[] requests, NativeArray<LightDataGI> lightsOutput)
			{
				DirectionalLight directionalLight = default(DirectionalLight);
				PointLight pointLight = default(PointLight);
				SpotLight spotLight = default(SpotLight);
				RectangleLight rectangleLight = default(RectangleLight);
				DiscLight discLight = default(DiscLight);
				Cookie cookie = default(Cookie);
				LightDataGI value = default(LightDataGI);
				for (int i = 0; i < requests.Length; i++)
				{
					Light light = requests[i];
					switch (light.type)
					{
					case LightType.Spot:
						LightmapperUtils.Extract(light, ref spotLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref spotLight, ref cookie);
						break;
					case LightType.Directional:
						LightmapperUtils.Extract(light, ref directionalLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref directionalLight, ref cookie);
						break;
					case LightType.Point:
						LightmapperUtils.Extract(light, ref pointLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref pointLight, ref cookie);
						break;
					case LightType.Area:
						LightmapperUtils.Extract(light, ref rectangleLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref rectangleLight, ref cookie);
						break;
					case LightType.Disc:
						LightmapperUtils.Extract(light, ref discLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref discLight, ref cookie);
						break;
					default:
						value.InitNoBake(light.GetInstanceID());
						break;
					}
					lightsOutput[i] = value;
				}
			}

			// Token: 0x04000EC5 RID: 3781
			public static readonly Lightmapping.<>c <>9 = new Lightmapping.<>c();
		}
	}
}
