using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainCallbacks
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007F RID: 127 RVA: 0x00002360 File Offset: 0x00000560
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x00002394 File Offset: 0x00000594
		public static event TerrainCallbacks.HeightmapChangedCallback heightmapChanged
		{
			[CompilerGenerated]
			add
			{
				TerrainCallbacks.HeightmapChangedCallback heightmapChangedCallback = TerrainCallbacks.heightmapChanged;
				TerrainCallbacks.HeightmapChangedCallback heightmapChangedCallback2;
				do
				{
					heightmapChangedCallback2 = heightmapChangedCallback;
					TerrainCallbacks.HeightmapChangedCallback value2 = (TerrainCallbacks.HeightmapChangedCallback)Delegate.Combine(heightmapChangedCallback2, value);
					heightmapChangedCallback = Interlocked.CompareExchange<TerrainCallbacks.HeightmapChangedCallback>(ref TerrainCallbacks.heightmapChanged, value2, heightmapChangedCallback2);
				}
				while (heightmapChangedCallback != heightmapChangedCallback2);
			}
			[CompilerGenerated]
			remove
			{
				TerrainCallbacks.HeightmapChangedCallback heightmapChangedCallback = TerrainCallbacks.heightmapChanged;
				TerrainCallbacks.HeightmapChangedCallback heightmapChangedCallback2;
				do
				{
					heightmapChangedCallback2 = heightmapChangedCallback;
					TerrainCallbacks.HeightmapChangedCallback value2 = (TerrainCallbacks.HeightmapChangedCallback)Delegate.Remove(heightmapChangedCallback2, value);
					heightmapChangedCallback = Interlocked.CompareExchange<TerrainCallbacks.HeightmapChangedCallback>(ref TerrainCallbacks.heightmapChanged, value2, heightmapChangedCallback2);
				}
				while (heightmapChangedCallback != heightmapChangedCallback2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000081 RID: 129 RVA: 0x000023C8 File Offset: 0x000005C8
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x000023FC File Offset: 0x000005FC
		public static event TerrainCallbacks.TextureChangedCallback textureChanged
		{
			[CompilerGenerated]
			add
			{
				TerrainCallbacks.TextureChangedCallback textureChangedCallback = TerrainCallbacks.textureChanged;
				TerrainCallbacks.TextureChangedCallback textureChangedCallback2;
				do
				{
					textureChangedCallback2 = textureChangedCallback;
					TerrainCallbacks.TextureChangedCallback value2 = (TerrainCallbacks.TextureChangedCallback)Delegate.Combine(textureChangedCallback2, value);
					textureChangedCallback = Interlocked.CompareExchange<TerrainCallbacks.TextureChangedCallback>(ref TerrainCallbacks.textureChanged, value2, textureChangedCallback2);
				}
				while (textureChangedCallback != textureChangedCallback2);
			}
			[CompilerGenerated]
			remove
			{
				TerrainCallbacks.TextureChangedCallback textureChangedCallback = TerrainCallbacks.textureChanged;
				TerrainCallbacks.TextureChangedCallback textureChangedCallback2;
				do
				{
					textureChangedCallback2 = textureChangedCallback;
					TerrainCallbacks.TextureChangedCallback value2 = (TerrainCallbacks.TextureChangedCallback)Delegate.Remove(textureChangedCallback2, value);
					textureChangedCallback = Interlocked.CompareExchange<TerrainCallbacks.TextureChangedCallback>(ref TerrainCallbacks.textureChanged, value2, textureChangedCallback2);
				}
				while (textureChangedCallback != textureChangedCallback2);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002430 File Offset: 0x00000630
		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			bool flag = TerrainCallbacks.heightmapChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.heightmapChanged(terrain, heightRegion, synched);
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002474 File Offset: 0x00000674
		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			bool flag = TerrainCallbacks.textureChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.textureChanged(terrain, textureName, texelRegion, synched);
				}
			}
		}

		// Token: 0x04000019 RID: 25
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static TerrainCallbacks.HeightmapChangedCallback heightmapChanged;

		// Token: 0x0400001A RID: 26
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static TerrainCallbacks.TextureChangedCallback textureChanged;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000086 RID: 134
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600008A RID: 138
		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);
	}
}
