using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.U2D;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000E RID: 14
	[NativeType(Header = "Modules/Tilemap/Public/TilemapRenderer.h")]
	[RequireComponent(typeof(Tilemap))]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	[NativeHeader("Modules/Tilemap/TilemapRendererJobs.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	public sealed class TilemapRenderer : Renderer
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002D3C File Offset: 0x00000F3C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002D52 File Offset: 0x00000F52
		public Vector3Int chunkSize
		{
			get
			{
				Vector3Int result;
				this.get_chunkSize_Injected(out result);
				return result;
			}
			set
			{
				this.set_chunkSize_Injected(ref value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002D72 File Offset: 0x00000F72
		public Vector3 chunkCullingBounds
		{
			[FreeFunction("TilemapRendererBindings::GetChunkCullingBounds", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_chunkCullingBounds_Injected(out result);
				return result;
			}
			[FreeFunction("TilemapRendererBindings::SetChunkCullingBounds", HasExplicitThis = true)]
			set
			{
				this.set_chunkCullingBounds_Injected(ref value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		public extern int maxChunkCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C1 RID: 193
		// (set) Token: 0x060000C2 RID: 194
		public extern int maxFrameAge { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C3 RID: 195
		// (set) Token: 0x060000C4 RID: 196
		public extern TilemapRenderer.SortOrder sortOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		[NativeProperty("RenderMode")]
		public extern TilemapRenderer.Mode mode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C7 RID: 199
		// (set) Token: 0x060000C8 RID: 200
		public extern TilemapRenderer.DetectChunkCullingBounds detectChunkCullingBounds { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C9 RID: 201
		// (set) Token: 0x060000CA RID: 202
		public extern SpriteMaskInteraction maskInteraction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000CB RID: 203 RVA: 0x00002D7C File Offset: 0x00000F7C
		[RequiredByNativeCode]
		internal void RegisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered += this.OnSpriteAtlasRegistered;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002D91 File Offset: 0x00000F91
		[RequiredByNativeCode]
		internal void UnregisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered -= this.OnSpriteAtlasRegistered;
		}

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnSpriteAtlasRegistered(SpriteAtlas atlas);

		// Token: 0x060000CE RID: 206 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public TilemapRenderer()
		{
		}

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_chunkSize_Injected(out Vector3Int ret);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_chunkSize_Injected(ref Vector3Int value);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_chunkCullingBounds_Injected(out Vector3 ret);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_chunkCullingBounds_Injected(ref Vector3 value);

		// Token: 0x0200000F RID: 15
		public enum SortOrder
		{
			// Token: 0x04000032 RID: 50
			BottomLeft,
			// Token: 0x04000033 RID: 51
			BottomRight,
			// Token: 0x04000034 RID: 52
			TopLeft,
			// Token: 0x04000035 RID: 53
			TopRight
		}

		// Token: 0x02000010 RID: 16
		public enum Mode
		{
			// Token: 0x04000037 RID: 55
			Chunk,
			// Token: 0x04000038 RID: 56
			Individual
		}

		// Token: 0x02000011 RID: 17
		public enum DetectChunkCullingBounds
		{
			// Token: 0x0400003A RID: 58
			Auto,
			// Token: 0x0400003B RID: 59
			Manual
		}
	}
}
