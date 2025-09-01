using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000144 RID: 324
	[NativeHeader("Runtime/Graphics/TrailRenderer.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public sealed class TrailRenderer : Renderer
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0000F338 File Offset: 0x0000D538
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A77 RID: 2679
		// (set) Token: 0x06000A78 RID: 2680
		public extern float time { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A79 RID: 2681
		// (set) Token: 0x06000A7A RID: 2682
		public extern float startWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A7B RID: 2683
		// (set) Token: 0x06000A7C RID: 2684
		public extern float endWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A7D RID: 2685
		// (set) Token: 0x06000A7E RID: 2686
		public extern float widthMultiplier { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A7F RID: 2687
		// (set) Token: 0x06000A80 RID: 2688
		public extern bool autodestruct { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A81 RID: 2689
		// (set) Token: 0x06000A82 RID: 2690
		public extern bool emitting { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A83 RID: 2691
		// (set) Token: 0x06000A84 RID: 2692
		public extern int numCornerVertices { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A85 RID: 2693
		// (set) Token: 0x06000A86 RID: 2694
		public extern int numCapVertices { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A87 RID: 2695
		// (set) Token: 0x06000A88 RID: 2696
		public extern float minVertexDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0000F350 File Offset: 0x0000D550
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0000F366 File Offset: 0x0000D566
		public Color startColor
		{
			get
			{
				Color result;
				this.get_startColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_startColor_Injected(ref value);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0000F370 File Offset: 0x0000D570
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0000F386 File Offset: 0x0000D586
		public Color endColor
		{
			get
			{
				Color result;
				this.get_endColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_endColor_Injected(ref value);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A8D RID: 2701
		[NativeProperty("PositionsCount")]
		public extern int positionCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000F390 File Offset: 0x0000D590
		public void SetPosition(int index, Vector3 position)
		{
			this.SetPosition_Injected(index, ref position);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000F39C File Offset: 0x0000D59C
		public Vector3 GetPosition(int index)
		{
			Vector3 result;
			this.GetPosition_Injected(index, out result);
			return result;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A90 RID: 2704
		// (set) Token: 0x06000A91 RID: 2705
		public extern float shadowBias { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A92 RID: 2706
		// (set) Token: 0x06000A93 RID: 2707
		public extern bool generateLightingData { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A94 RID: 2708
		// (set) Token: 0x06000A95 RID: 2709
		public extern LineTextureMode textureMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A96 RID: 2710
		// (set) Token: 0x06000A97 RID: 2711
		public extern LineAlignment alignment { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A98 RID: 2712
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x06000A99 RID: 2713 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x06000A9A RID: 2714
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, bool useTransform = false);

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		public AnimationCurve widthCurve
		{
			get
			{
				return this.GetWidthCurveCopy();
			}
			set
			{
				this.SetWidthCurve(value);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0000F400 File Offset: 0x0000D600
		public Gradient colorGradient
		{
			get
			{
				return this.GetColorGradientCopy();
			}
			set
			{
				this.SetColorGradient(value);
			}
		}

		// Token: 0x06000A9F RID: 2719
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationCurve GetWidthCurveCopy();

		// Token: 0x06000AA0 RID: 2720
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetWidthCurve([NotNull("ArgumentNullException")] AnimationCurve curve);

		// Token: 0x06000AA1 RID: 2721
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Gradient GetColorGradientCopy();

		// Token: 0x06000AA2 RID: 2722
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorGradient([NotNull("ArgumentNullException")] Gradient curve);

		// Token: 0x06000AA3 RID: 2723
		[FreeFunction(Name = "TrailRendererScripting::GetPositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPositions([NotNull("ArgumentNullException")] [Out] Vector3[] positions);

		// Token: 0x06000AA4 RID: 2724
		[FreeFunction(Name = "TrailRendererScripting::GetVisiblePositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetVisiblePositions([NotNull("ArgumentNullException")] [Out] Vector3[] positions);

		// Token: 0x06000AA5 RID: 2725
		[FreeFunction(Name = "TrailRendererScripting::SetPositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPositions([NotNull("ArgumentNullException")] Vector3[] positions);

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0000F40B File Offset: 0x0000D60B
		[FreeFunction(Name = "TrailRendererScripting::AddPosition", HasExplicitThis = true)]
		public void AddPosition(Vector3 position)
		{
			this.AddPosition_Injected(ref position);
		}

		// Token: 0x06000AA7 RID: 2727
		[FreeFunction(Name = "TrailRendererScripting::AddPositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddPositions([NotNull("ArgumentNullException")] Vector3[] positions);

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000F415 File Offset: 0x0000D615
		public void SetPositions(NativeArray<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0000F433 File Offset: 0x0000D633
		public void SetPositions(NativeSlice<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0000F454 File Offset: 0x0000D654
		public int GetPositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000F480 File Offset: 0x0000D680
		public int GetPositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		public int GetVisiblePositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetVisiblePositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
		public int GetVisiblePositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetVisiblePositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0000F503 File Offset: 0x0000D703
		public void AddPositions([Out] NativeArray<Vector3> positions)
		{
			this.AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000F521 File Offset: 0x0000D721
		public void AddPositions([Out] NativeSlice<Vector3> positions)
		{
			this.AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AB0 RID: 2736
		[FreeFunction(Name = "TrailRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPositionsWithNativeContainer(IntPtr positions, int count);

		// Token: 0x06000AB1 RID: 2737
		[FreeFunction(Name = "TrailRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPositionsWithNativeContainer(IntPtr positions, int length);

		// Token: 0x06000AB2 RID: 2738
		[FreeFunction(Name = "TrailRendererScripting::GetVisiblePositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVisiblePositionsWithNativeContainer(IntPtr positions, int length);

		// Token: 0x06000AB3 RID: 2739
		[FreeFunction(Name = "TrailRendererScripting::AddPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddPositionsWithNativeContainer(IntPtr positions, int length);

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000BF29 File Offset: 0x0000A129
		public TrailRenderer()
		{
		}

		// Token: 0x06000AB5 RID: 2741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_startColor_Injected(out Color ret);

		// Token: 0x06000AB6 RID: 2742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_startColor_Injected(ref Color value);

		// Token: 0x06000AB7 RID: 2743
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_endColor_Injected(out Color ret);

		// Token: 0x06000AB8 RID: 2744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_endColor_Injected(ref Color value);

		// Token: 0x06000AB9 RID: 2745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPosition_Injected(int index, ref Vector3 position);

		// Token: 0x06000ABA RID: 2746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPosition_Injected(int index, out Vector3 ret);

		// Token: 0x06000ABB RID: 2747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddPosition_Injected(ref Vector3 position);
	}
}
