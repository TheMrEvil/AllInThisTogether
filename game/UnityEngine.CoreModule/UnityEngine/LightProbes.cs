using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	[NativeHeader("Runtime/Export/Graphics/Graphics.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightProbes : Object
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x0000E886 File Offset: 0x0000CA86
		private LightProbes()
		{
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060009AA RID: 2474 RVA: 0x0000E8BC File Offset: 0x0000CABC
		// (remove) Token: 0x060009AB RID: 2475 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public static event Action tetrahedralizationCompleted
		{
			[CompilerGenerated]
			add
			{
				Action action = LightProbes.tetrahedralizationCompleted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref LightProbes.tetrahedralizationCompleted, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = LightProbes.tetrahedralizationCompleted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref LightProbes.tetrahedralizationCompleted, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0000E924 File Offset: 0x0000CB24
		[RequiredByNativeCode]
		private static void Internal_CallTetrahedralizationCompletedFunction()
		{
			bool flag = LightProbes.tetrahedralizationCompleted != null;
			if (flag)
			{
				LightProbes.tetrahedralizationCompleted();
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060009AD RID: 2477 RVA: 0x0000E94C File Offset: 0x0000CB4C
		// (remove) Token: 0x060009AE RID: 2478 RVA: 0x0000E980 File Offset: 0x0000CB80
		public static event Action needsRetetrahedralization
		{
			[CompilerGenerated]
			add
			{
				Action action = LightProbes.needsRetetrahedralization;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref LightProbes.needsRetetrahedralization, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = LightProbes.needsRetetrahedralization;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref LightProbes.needsRetetrahedralization, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		[RequiredByNativeCode]
		private static void Internal_CallNeedsRetetrahedralizationFunction()
		{
			bool flag = LightProbes.needsRetetrahedralization != null;
			if (flag)
			{
				LightProbes.needsRetetrahedralization();
			}
		}

		// Token: 0x060009B0 RID: 2480
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Tetrahedralize();

		// Token: 0x060009B1 RID: 2481
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TetrahedralizeAsync();

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000E9D9 File Offset: 0x0000CBD9
		[FreeFunction]
		public static void GetInterpolatedProbe(Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe)
		{
			LightProbes.GetInterpolatedProbe_Injected(ref position, renderer, out probe);
		}

		// Token: 0x060009B3 RID: 2483
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool AreLightProbesAllowed(Renderer renderer);

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public static void CalculateInterpolatedLightAndOcclusionProbes(Vector3[] positions, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null && lightProbes.Length < positions.Length;
			if (flag3)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes has less elements than positions");
			}
			bool flag4 = occlusionProbes != null && occlusionProbes.Length < positions.Length;
			if (flag4)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes has less elements than positions");
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(positions, positions.Length, lightProbes, occlusionProbes);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public static void CalculateInterpolatedLightAndOcclusionProbes(List<Vector3> positions, List<SphericalHarmonicsL2> lightProbes, List<Vector4> occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null;
			if (flag3)
			{
				bool flag4 = lightProbes.Capacity < positions.Count;
				if (flag4)
				{
					lightProbes.Capacity = positions.Count;
				}
				bool flag5 = lightProbes.Count < positions.Count;
				if (flag5)
				{
					NoAllocHelpers.ResizeList<SphericalHarmonicsL2>(lightProbes, positions.Count);
				}
			}
			bool flag6 = occlusionProbes != null;
			if (flag6)
			{
				bool flag7 = occlusionProbes.Capacity < positions.Count;
				if (flag7)
				{
					occlusionProbes.Capacity = positions.Count;
				}
				bool flag8 = occlusionProbes.Count < positions.Count;
				if (flag8)
				{
					NoAllocHelpers.ResizeList<Vector4>(occlusionProbes, positions.Count);
				}
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(NoAllocHelpers.ExtractArrayFromListT<Vector3>(positions), positions.Count, NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes));
		}

		// Token: 0x060009B6 RID: 2486
		[NativeName("CalculateInterpolatedLightAndOcclusionProbes")]
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CalculateInterpolatedLightAndOcclusionProbes_Internal([Unmarshalled] Vector3[] positions, int positionsCount, [Unmarshalled] SphericalHarmonicsL2[] lightProbes, [Unmarshalled] Vector4[] occlusionProbes);

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009B7 RID: 2487
		public extern Vector3[] positions { [NativeName("GetLightProbePositions")] [FreeFunction(HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009B8 RID: 2488
		// (set) Token: 0x060009B9 RID: 2489
		public extern SphericalHarmonicsL2[] bakedProbes { [NativeName("GetBakedCoefficients")] [FreeFunction(HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(HasExplicitThis = true)] [NativeName("SetBakedCoefficients")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009BA RID: 2490
		public extern int count { [FreeFunction(HasExplicitThis = true)] [NativeName("GetLightProbeCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009BB RID: 2491
		public extern int cellCount { [FreeFunction(HasExplicitThis = true)] [NativeName("GetTetrahedraSize")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009BC RID: 2492
		[NativeName("GetLightProbeCount")]
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCount();

		// Token: 0x060009BD RID: 2493 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("Use GetInterpolatedProbe instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0000EB60 File Offset: 0x0000CD60
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00004563 File Offset: 0x00002763
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use bakedProbes instead.", true)]
		public float[] coefficients
		{
			get
			{
				return new float[0];
			}
			set
			{
			}
		}

		// Token: 0x060009C0 RID: 2496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterpolatedProbe_Injected(ref Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe);

		// Token: 0x040003DC RID: 988
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action tetrahedralizationCompleted;

		// Token: 0x040003DD RID: 989
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action needsRetetrahedralization;
	}
}
