using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/UIElementsNative/TextNative.bindings.h")]
	internal static class TextNative
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00003C80 File Offset: 0x00001E80
		public static Vector2 GetCursorPosition(TextNativeSettings settings, Rect rect, int cursorIndex)
		{
			bool flag = settings.font == null;
			Vector2 result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = Vector2.zero;
			}
			else
			{
				result = TextNative.DoGetCursorPosition(settings, rect, cursorIndex);
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public static float ComputeTextWidth(TextNativeSettings settings)
		{
			bool flag = settings.font == null;
			float result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = 0f;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(settings.text);
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = TextNative.DoComputeTextWidth(settings);
				}
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003D14 File Offset: 0x00001F14
		public static float ComputeTextHeight(TextNativeSettings settings)
		{
			bool flag = settings.font == null;
			float result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = 0f;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(settings.text);
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = TextNative.DoComputeTextHeight(settings);
				}
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003D68 File Offset: 0x00001F68
		public static NativeArray<TextVertex> GetVertices(TextNativeSettings settings)
		{
			int num = 0;
			TextNative.GetVertices(settings, IntPtr.Zero, UnsafeUtility.SizeOf<TextVertex>(), ref num);
			NativeArray<TextVertex> nativeArray = new NativeArray<TextVertex>(num, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			bool flag = num > 0;
			if (flag)
			{
				TextNative.GetVertices(settings, (IntPtr)nativeArray.GetUnsafePtr<TextVertex>(), UnsafeUtility.SizeOf<TextVertex>(), ref num);
				Debug.Assert(num == nativeArray.Length);
			}
			return nativeArray;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public static Vector2 GetOffset(TextNativeSettings settings, Rect screenRect)
		{
			bool flag = settings.font == null;
			Vector2 result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = new Vector2(0f, 0f);
			}
			else
			{
				settings.text = (settings.text ?? "");
				result = TextNative.DoGetOffset(settings, screenRect);
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003E30 File Offset: 0x00002030
		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003E8D File Offset: 0x0000208D
		[FreeFunction(Name = "TextNative::ComputeTextWidth")]
		private static float DoComputeTextWidth(TextNativeSettings settings)
		{
			return TextNative.DoComputeTextWidth_Injected(ref settings);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003E96 File Offset: 0x00002096
		[FreeFunction(Name = "TextNative::ComputeTextHeight")]
		private static float DoComputeTextHeight(TextNativeSettings settings)
		{
			return TextNative.DoComputeTextHeight_Injected(ref settings);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003EA0 File Offset: 0x000020A0
		[FreeFunction(Name = "TextNative::GetCursorPosition")]
		private static Vector2 DoGetCursorPosition(TextNativeSettings settings, Rect rect, int cursorPosition)
		{
			Vector2 result;
			TextNative.DoGetCursorPosition_Injected(ref settings, ref rect, cursorPosition, out result);
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003EBA File Offset: 0x000020BA
		[FreeFunction(Name = "TextNative::GetVertices")]
		private static void GetVertices(TextNativeSettings settings, IntPtr buffer, int vertexSize, ref int vertexCount)
		{
			TextNative.GetVertices_Injected(ref settings, buffer, vertexSize, ref vertexCount);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003EC8 File Offset: 0x000020C8
		[FreeFunction(Name = "TextNative::GetOffset")]
		private static Vector2 DoGetOffset(TextNativeSettings settings, Rect rect)
		{
			Vector2 result;
			TextNative.DoGetOffset_Injected(ref settings, ref rect, out result);
			return result;
		}

		// Token: 0x0600016A RID: 362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float DoComputeTextWidth_Injected(ref TextNativeSettings settings);

		// Token: 0x0600016B RID: 363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float DoComputeTextHeight_Injected(ref TextNativeSettings settings);

		// Token: 0x0600016C RID: 364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DoGetCursorPosition_Injected(ref TextNativeSettings settings, ref Rect rect, int cursorPosition, out Vector2 ret);

		// Token: 0x0600016D RID: 365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVertices_Injected(ref TextNativeSettings settings, IntPtr buffer, int vertexSize, ref int vertexCount);

		// Token: 0x0600016E RID: 366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DoGetOffset_Injected(ref TextNativeSettings settings, ref Rect rect, out Vector2 ret);
	}
}
