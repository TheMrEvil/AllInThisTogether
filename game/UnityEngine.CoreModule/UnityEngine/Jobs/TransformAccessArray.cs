using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x02000287 RID: 647
	[NativeType(Header = "Runtime/Transform/ScriptBindings/TransformAccess.bindings.h", CodegenOptions = CodegenOptions.Custom)]
	public struct TransformAccessArray : IDisposable
	{
		// Token: 0x06001C11 RID: 7185 RVA: 0x0002D25E File Offset: 0x0002B45E
		public TransformAccessArray(Transform[] transforms, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(transforms.Length, desiredJobCount, out this);
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0002D279 File Offset: 0x0002B479
		public TransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(capacity, desiredJobCount, out this);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0002D285 File Offset: 0x0002B485
		public static void Allocate(int capacity, int desiredJobCount, out TransformAccessArray array)
		{
			array.m_TransformArray = TransformAccessArray.Create(capacity, desiredJobCount);
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0002D298 File Offset: 0x0002B498
		public bool isCreated
		{
			get
			{
				return this.m_TransformArray != IntPtr.Zero;
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0002D2BA File Offset: 0x0002B4BA
		public void Dispose()
		{
			TransformAccessArray.DestroyTransformAccessArray(this.m_TransformArray);
			this.m_TransformArray = IntPtr.Zero;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0002D2D4 File Offset: 0x0002B4D4
		internal IntPtr GetTransformAccessArrayForSchedule()
		{
			return this.m_TransformArray;
		}

		// Token: 0x170005A4 RID: 1444
		public Transform this[int index]
		{
			get
			{
				return TransformAccessArray.GetTransform(this.m_TransformArray, index);
			}
			set
			{
				TransformAccessArray.SetTransform(this.m_TransformArray, index, value);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0002D31C File Offset: 0x0002B51C
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x0002D339 File Offset: 0x0002B539
		public int capacity
		{
			get
			{
				return TransformAccessArray.GetCapacity(this.m_TransformArray);
			}
			set
			{
				TransformAccessArray.SetCapacity(this.m_TransformArray, value);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0002D34C File Offset: 0x0002B54C
		public int length
		{
			get
			{
				return TransformAccessArray.GetLength(this.m_TransformArray);
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0002D369 File Offset: 0x0002B569
		public void Add(Transform transform)
		{
			TransformAccessArray.Add(this.m_TransformArray, transform);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0002D379 File Offset: 0x0002B579
		public void RemoveAtSwapBack(int index)
		{
			TransformAccessArray.RemoveAtSwapBack(this.m_TransformArray, index);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0002D389 File Offset: 0x0002B589
		public void SetTransforms(Transform[] transforms)
		{
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06001C1F RID: 7199
		[NativeMethod(Name = "TransformAccessArrayBindings::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(int capacity, int desiredJobCount);

		// Token: 0x06001C20 RID: 7200
		[NativeMethod(Name = "DestroyTransformAccessArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyTransformAccessArray(IntPtr transformArray);

		// Token: 0x06001C21 RID: 7201
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransforms", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransforms(IntPtr transformArrayIntPtr, Transform[] transforms);

		// Token: 0x06001C22 RID: 7202
		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransform", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Add(IntPtr transformArrayIntPtr, Transform transform);

		// Token: 0x06001C23 RID: 7203
		[NativeMethod(Name = "TransformAccessArrayBindings::RemoveAtSwapBack", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAtSwapBack(IntPtr transformArrayIntPtr, int index);

		// Token: 0x06001C24 RID: 7204
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedTransformAccess", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedTransformAccess(IntPtr transformArrayIntPtr);

		// Token: 0x06001C25 RID: 7205
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedToUserIndex", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedToUserIndex(IntPtr transformArrayIntPtr);

		// Token: 0x06001C26 RID: 7206
		[NativeMethod(Name = "TransformAccessArrayBindings::GetLength", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLength(IntPtr transformArrayIntPtr);

		// Token: 0x06001C27 RID: 7207
		[NativeMethod(Name = "TransformAccessArrayBindings::GetCapacity", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCapacity(IntPtr transformArrayIntPtr);

		// Token: 0x06001C28 RID: 7208
		[NativeMethod(Name = "TransformAccessArrayBindings::SetCapacity", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetCapacity(IntPtr transformArrayIntPtr, int capacity);

		// Token: 0x06001C29 RID: 7209
		[NativeMethod(Name = "TransformAccessArrayBindings::GetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Transform GetTransform(IntPtr transformArrayIntPtr, int index);

		// Token: 0x06001C2A RID: 7210
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTransform(IntPtr transformArrayIntPtr, int index, Transform transform);

		// Token: 0x04000928 RID: 2344
		private IntPtr m_TransformArray;
	}
}
