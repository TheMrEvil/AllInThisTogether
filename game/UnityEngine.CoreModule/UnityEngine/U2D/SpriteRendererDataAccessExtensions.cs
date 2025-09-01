using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000275 RID: 629
	[NativeHeader("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	public static class SpriteRendererDataAccessExtensions
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x0002BD08 File Offset: 0x00029F08
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<byte> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("spriteRenderer does not have a valid sprite set.", new object[0]));
			}
			bool flag2 = src.Length != SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite);
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("custom sprite vertex data size must match sprite asset's vertex data size {0} {1}", src.Length, SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite)));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<byte>(), src.Length);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0002BD98 File Offset: 0x00029F98
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<Vector3> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new InvalidOperationException("spriteRenderer does not have a valid sprite set.");
			}
			bool flag2 = src.Length != spriteRenderer.sprite.GetVertexCount();
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The src length {0} must match the vertex count of source Sprite {1}.", src.Length, spriteRenderer.sprite.GetVertexCount()));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<Vector3>(), src.Length);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0002BE1C File Offset: 0x0002A01C
		internal static void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, NativeArray<IntPtr> buffers, NativeArray<int> bufferSizes, NativeArray<Bounds> bounds)
		{
			int num = spriteRenderers.Length;
			bool flag = num != buffers.Length || num != bufferSizes.Length || num != bounds.Length;
			if (flag)
			{
				throw new ArgumentException("Input array sizes are not the same.");
			}
			SpriteRendererDataAccessExtensions.SetBatchDeformableBufferAndLocalAABBArray(spriteRenderers, buffers.GetUnsafeReadOnlyPtr<IntPtr>(), bufferSizes.GetUnsafeReadOnlyPtr<int>(), bounds.GetUnsafeReadOnlyPtr<Bounds>(), num);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0002BE7C File Offset: 0x0002A07C
		internal unsafe static bool IsUsingDeformableBuffer(this SpriteRenderer spriteRenderer, IntPtr buffer)
		{
			return SpriteRendererDataAccessExtensions.IsUsingDeformableBuffer(spriteRenderer, (void*)buffer);
		}

		// Token: 0x06001B56 RID: 6998
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeactivateDeformableBuffer([NotNull("ArgumentNullException")] this SpriteRenderer renderer);

		// Token: 0x06001B57 RID: 6999 RVA: 0x0002BE9A File Offset: 0x0002A09A
		internal static void SetLocalAABB([NotNull("ArgumentNullException")] this SpriteRenderer renderer, Bounds aabb)
		{
			SpriteRendererDataAccessExtensions.SetLocalAABB_Injected(renderer, ref aabb);
		}

		// Token: 0x06001B58 RID: 7000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetDeformableBuffer([NotNull("ArgumentNullException")] SpriteRenderer spriteRenderer, void* src, int count);

		// Token: 0x06001B59 RID: 7001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, void* buffers, void* bufferSizes, void* bounds, int count);

		// Token: 0x06001B5A RID: 7002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool IsUsingDeformableBuffer([NotNull("ArgumentNullException")] SpriteRenderer spriteRenderer, void* buffer);

		// Token: 0x06001B5B RID: 7003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalAABB_Injected(SpriteRenderer renderer, ref Bounds aabb);
	}
}
