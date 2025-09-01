using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200011A RID: 282
	[NativeHeader("Runtime/GI/DynamicGI.h")]
	public sealed class DynamicGI
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600077E RID: 1918
		// (set) Token: 0x0600077F RID: 1919
		public static extern float indirectScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000780 RID: 1920
		// (set) Token: 0x06000781 RID: 1921
		public static extern float updateThreshold { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000782 RID: 1922
		// (set) Token: 0x06000783 RID: 1923
		public static extern int materialUpdateTimeSlice { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000784 RID: 1924 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public static void SetEmissive(Renderer renderer, Color color)
		{
			DynamicGI.SetEmissive_Injected(renderer, ref color);
		}

		// Token: 0x06000785 RID: 1925
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetEnvironmentData([NotNull("ArgumentNullException")] float[] input);

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000786 RID: 1926
		// (set) Token: 0x06000787 RID: 1927
		public static extern bool synchronousMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000788 RID: 1928
		public static extern bool isConverged { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000789 RID: 1929
		internal static extern int scheduledMaterialUpdatesCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600078A RID: 1930
		// (set) Token: 0x0600078B RID: 1931
		internal static extern bool asyncMaterialUpdates { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600078C RID: 1932
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateEnvironment();

		// Token: 0x0600078D RID: 1933 RVA: 0x00004563 File Offset: 0x00002763
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("DynamicGI.UpdateMaterials(Renderer) is deprecated; instead, use extension method from RendererExtensions: 'renderer.UpdateGIMaterials()' (UnityUpgradable).", true)]
		public static void UpdateMaterials(Renderer renderer)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("DynamicGI.UpdateMaterials(Terrain) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials()' (UnityUpgradable).", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateMaterials(Object renderer)
		{
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("DynamicGI.UpdateMaterials(Terrain, int, int, int, int) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials(x, y, width, height)' (UnityUpgradable).", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateMaterials(Object renderer, int x, int y, int width, int height)
		{
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00002072 File Offset: 0x00000272
		public DynamicGI()
		{
		}

		// Token: 0x06000791 RID: 1937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEmissive_Injected(Renderer renderer, ref Color color);
	}
}
