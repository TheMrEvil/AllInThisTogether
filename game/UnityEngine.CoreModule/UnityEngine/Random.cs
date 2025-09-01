using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E5 RID: 485
	[NativeHeader("Runtime/Export/Random/Random.bindings.h")]
	public static class Random
	{
		// Token: 0x060015FD RID: 5629
		[NativeMethod("SetSeed")]
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitState(int seed);

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00023554 File Offset: 0x00021754
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00023569 File Offset: 0x00021769
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static Random.State state
		{
			get
			{
				Random.State result;
				Random.get_state_Injected(out result);
				return result;
			}
			set
			{
				Random.set_state_Injected(ref value);
			}
		}

		// Token: 0x06001600 RID: 5632
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float minInclusive, float maxInclusive);

		// Token: 0x06001601 RID: 5633 RVA: 0x00023574 File Offset: 0x00021774
		public static int Range(int minInclusive, int maxExclusive)
		{
			return Random.RandomRangeInt(minInclusive, maxExclusive);
		}

		// Token: 0x06001602 RID: 5634
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int minInclusive, int maxExclusive);

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001603 RID: 5635
		public static extern float value { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00023590 File Offset: 0x00021790
		public static Vector3 insideUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 result;
				Random.get_insideUnitSphere_Injected(out result);
				return result;
			}
		}

		// Token: 0x06001605 RID: 5637
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x000235A8 File Offset: 0x000217A8
		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 result;
				Random.GetRandomUnitCircle(out result);
				return result;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x000235C4 File Offset: 0x000217C4
		public static Vector3 onUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 result;
				Random.get_onUnitSphere_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x000235DC File Offset: 0x000217DC
		public static Quaternion rotation
		{
			[FreeFunction]
			get
			{
				Quaternion result;
				Random.get_rotation_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x000235F4 File Offset: 0x000217F4
		public static Quaternion rotationUniform
		{
			[FreeFunction]
			get
			{
				Quaternion result;
				Random.get_rotationUniform_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600160A RID: 5642
		// (set) Token: 0x0600160B RID: 5643
		[Obsolete("Deprecated. Use InitState() function or Random.state property instead.")]
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static extern int seed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600160C RID: 5644 RVA: 0x0002360C File Offset: 0x0002180C
		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00023628 File Offset: 0x00021828
		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00023644 File Offset: 0x00021844
		public static Color ColorHSV()
		{
			return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00023684 File Offset: 0x00021884
		public static Color ColorHSV(float hueMin, float hueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000236BC File Offset: 0x000218BC
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000236EC File Offset: 0x000218EC
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00023718 File Offset: 0x00021918
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float h = Mathf.Lerp(hueMin, hueMax, Random.value);
			float s = Mathf.Lerp(saturationMin, saturationMax, Random.value);
			float v = Mathf.Lerp(valueMin, valueMax, Random.value);
			Color result = Color.HSVToRGB(h, s, v, true);
			result.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
			return result;
		}

		// Token: 0x06001613 RID: 5651
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_state_Injected(out Random.State ret);

		// Token: 0x06001614 RID: 5652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_state_Injected(ref Random.State value);

		// Token: 0x06001615 RID: 5653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_insideUnitSphere_Injected(out Vector3 ret);

		// Token: 0x06001616 RID: 5654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_onUnitSphere_Injected(out Vector3 ret);

		// Token: 0x06001617 RID: 5655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x06001618 RID: 5656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationUniform_Injected(out Quaternion ret);

		// Token: 0x020001E6 RID: 486
		[Serializable]
		public struct State
		{
			// Token: 0x040007C4 RID: 1988
			[SerializeField]
			private int s0;

			// Token: 0x040007C5 RID: 1989
			[SerializeField]
			private int s1;

			// Token: 0x040007C6 RID: 1990
			[SerializeField]
			private int s2;

			// Token: 0x040007C7 RID: 1991
			[SerializeField]
			private int s3;
		}
	}
}
