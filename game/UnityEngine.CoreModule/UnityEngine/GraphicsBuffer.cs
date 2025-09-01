using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	[UsedByNativeCode]
	public sealed class GraphicsBuffer : IDisposable
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00012458 File Offset: 0x00010658
		~GraphicsBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0001248C File Offset: 0x0001068C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x000124A0 File Offset: 0x000106A0
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GraphicsBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of GraphicsBuffer. Please use GraphicsBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000124EC File Offset: 0x000106EC
		private static bool RequiresCompute(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target target2 = GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.Raw | GraphicsBuffer.Target.Append | GraphicsBuffer.Target.Counter | GraphicsBuffer.Target.IndirectArguments;
			return (target & target2) > (GraphicsBuffer.Target)0;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001250C File Offset: 0x0001070C
		private static bool IsVertexIndexOrCopyOnly(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target target2 = GraphicsBuffer.Target.Vertex | GraphicsBuffer.Target.Index | GraphicsBuffer.Target.CopySource | GraphicsBuffer.Target.CopyDestination;
			return (target & target2) == target;
		}

		// Token: 0x06000DE6 RID: 3558
		[FreeFunction("GraphicsBuffer_Bindings::InitBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer(GraphicsBuffer.Target target, int count, int stride);

		// Token: 0x06000DE7 RID: 3559
		[FreeFunction("GraphicsBuffer_Bindings::DestroyBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer(GraphicsBuffer buf);

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00012528 File Offset: 0x00010728
		public GraphicsBuffer(GraphicsBuffer.Target target, int count, int stride)
		{
			bool flag = GraphicsBuffer.RequiresCompute(target) && !SystemInfo.supportsComputeShaders;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a graphics buffer that requires compute shader support, but compute shaders are not supported on this platform. Target: " + target.ToString());
			}
			bool flag2 = count <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a zero length graphics buffer", "count");
			}
			bool flag3 = stride <= 0;
			if (flag3)
			{
				throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", "stride");
			}
			bool flag4 = (target & GraphicsBuffer.Target.Index) != (GraphicsBuffer.Target)0 && stride != 2 && stride != 4;
			if (flag4)
			{
				throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride.ToString(), "stride");
			}
			bool flag5 = !GraphicsBuffer.IsVertexIndexOrCopyOnly(target) && stride % 4 != 0;
			if (flag5)
			{
				throw new ArgumentException("Stride must be a multiple of 4 unless the buffer is only used as a vertex buffer and/or index buffer ", "stride");
			}
			long num = (long)count * (long)stride;
			long maxGraphicsBufferSize = SystemInfo.maxGraphicsBufferSize;
			bool flag6 = num > maxGraphicsBufferSize;
			if (flag6)
			{
				throw new ArgumentException(string.Format("The total size of the graphics buffer ({0} bytes) exceeds the maximum buffer size. Maximum supported buffer size: {1} bytes.", num, maxGraphicsBufferSize));
			}
			this.m_Ptr = GraphicsBuffer.InitBuffer(target, count, stride);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001264B File Offset: 0x0001084B
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06000DEA RID: 3562
		[FreeFunction("GraphicsBuffer_Bindings::IsValidBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidBuffer(GraphicsBuffer buf);

		// Token: 0x06000DEB RID: 3563 RVA: 0x00012658 File Offset: 0x00010858
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero && GraphicsBuffer.IsValidBuffer(this);
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000DEC RID: 3564
		public extern int count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000DED RID: 3565
		public extern int stride { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000DEE RID: 3566
		public extern GraphicsBuffer.Target target { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000DEF RID: 3567 RVA: 0x00012688 File Offset: 0x00010888
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000126F0 File Offset: 0x000108F0
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00012761 File Offset: 0x00010961
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00012784 File Offset: 0x00010984
		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00012828 File Offset: 0x00010A28
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000128D8 File Offset: 0x00010AD8
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000DF5 RID: 3573
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06000DF6 RID: 3574
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00012948 File Offset: 0x00010B48
		[SecurityCritical]
		public void GetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x000129B0 File Offset: 0x00010BB0
		[SecurityCritical]
		public void GetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DF9 RID: 3577
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06000DFA RID: 3578
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetNativeBufferPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeBufferPtr();

		// Token: 0x170002CD RID: 717
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00012A54 File Offset: 0x00010C54
		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x06000DFC RID: 3580
		[FreeFunction(Name = "GraphicsBuffer_Bindings::SetName", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetName(string name);

		// Token: 0x06000DFD RID: 3581
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCounterValue(uint counterValue);

		// Token: 0x06000DFE RID: 3582
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountCC(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x06000DFF RID: 3583
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountGC(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x06000E00 RID: 3584
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountCG(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes);

		// Token: 0x06000E01 RID: 3585
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountGG(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes);

		// Token: 0x06000E02 RID: 3586 RVA: 0x00012A5E File Offset: 0x00010C5E
		public static void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00012A6A File Offset: 0x00010C6A
		public static void CopyCount(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00012A76 File Offset: 0x00010C76
		public static void CopyCount(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00012A82 File Offset: 0x00010C82
		public static void CopyCount(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x04000417 RID: 1047
		internal IntPtr m_Ptr;

		// Token: 0x0200014C RID: 332
		[Flags]
		public enum Target
		{
			// Token: 0x04000419 RID: 1049
			Vertex = 1,
			// Token: 0x0400041A RID: 1050
			Index = 2,
			// Token: 0x0400041B RID: 1051
			CopySource = 4,
			// Token: 0x0400041C RID: 1052
			CopyDestination = 8,
			// Token: 0x0400041D RID: 1053
			Structured = 16,
			// Token: 0x0400041E RID: 1054
			Raw = 32,
			// Token: 0x0400041F RID: 1055
			Append = 64,
			// Token: 0x04000420 RID: 1056
			Counter = 128,
			// Token: 0x04000421 RID: 1057
			IndirectArguments = 256,
			// Token: 0x04000422 RID: 1058
			Constant = 512
		}

		// Token: 0x0200014D RID: 333
		public struct IndirectDrawArgs
		{
			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00012A8E File Offset: 0x00010C8E
			// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00012A96 File Offset: 0x00010C96
			public uint vertexCountPerInstance
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<vertexCountPerInstance>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<vertexCountPerInstance>k__BackingField = value;
				}
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00012A9F File Offset: 0x00010C9F
			// (set) Token: 0x06000E09 RID: 3593 RVA: 0x00012AA7 File Offset: 0x00010CA7
			public uint instanceCount
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<instanceCount>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<instanceCount>k__BackingField = value;
				}
			}

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00012AB0 File Offset: 0x00010CB0
			// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00012AB8 File Offset: 0x00010CB8
			public uint startVertex
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<startVertex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<startVertex>k__BackingField = value;
				}
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00012AC1 File Offset: 0x00010CC1
			// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00012AC9 File Offset: 0x00010CC9
			public uint startInstance
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<startInstance>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<startInstance>k__BackingField = value;
				}
			}

			// Token: 0x04000423 RID: 1059
			public const int size = 16;

			// Token: 0x04000424 RID: 1060
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <vertexCountPerInstance>k__BackingField;

			// Token: 0x04000425 RID: 1061
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <instanceCount>k__BackingField;

			// Token: 0x04000426 RID: 1062
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private uint <startVertex>k__BackingField;

			// Token: 0x04000427 RID: 1063
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <startInstance>k__BackingField;
		}

		// Token: 0x0200014E RID: 334
		public struct IndirectDrawIndexedArgs
		{
			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00012AD2 File Offset: 0x00010CD2
			// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00012ADA File Offset: 0x00010CDA
			public uint indexCountPerInstance
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<indexCountPerInstance>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<indexCountPerInstance>k__BackingField = value;
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00012AE3 File Offset: 0x00010CE3
			// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00012AEB File Offset: 0x00010CEB
			public uint instanceCount
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<instanceCount>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<instanceCount>k__BackingField = value;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00012AF4 File Offset: 0x00010CF4
			// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00012AFC File Offset: 0x00010CFC
			public uint startIndex
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<startIndex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<startIndex>k__BackingField = value;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00012B05 File Offset: 0x00010D05
			// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00012B0D File Offset: 0x00010D0D
			public uint baseVertexIndex
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<baseVertexIndex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<baseVertexIndex>k__BackingField = value;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00012B16 File Offset: 0x00010D16
			// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00012B1E File Offset: 0x00010D1E
			public uint startInstance
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<startInstance>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<startInstance>k__BackingField = value;
				}
			}

			// Token: 0x04000428 RID: 1064
			public const int size = 20;

			// Token: 0x04000429 RID: 1065
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <indexCountPerInstance>k__BackingField;

			// Token: 0x0400042A RID: 1066
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <instanceCount>k__BackingField;

			// Token: 0x0400042B RID: 1067
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private uint <startIndex>k__BackingField;

			// Token: 0x0400042C RID: 1068
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <baseVertexIndex>k__BackingField;

			// Token: 0x0400042D RID: 1069
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private uint <startInstance>k__BackingField;
		}
	}
}
