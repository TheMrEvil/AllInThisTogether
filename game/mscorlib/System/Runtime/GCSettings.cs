using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime
{
	/// <summary>Specifies the garbage collection settings for the current process.</summary>
	// Token: 0x02000552 RID: 1362
	public static class GCSettings
	{
		/// <summary>Gets a value that indicates whether server garbage collection is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if server garbage collection is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("Always returns false")]
		public static bool IsServerGC
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the current latency mode for garbage collection.</summary>
		/// <returns>One of the enumeration values that specifies the latency mode.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Runtime.GCSettings.LatencyMode" /> property is being set to an invalid value.  
		///  -or-  
		///  The <see cref="P:System.Runtime.GCSettings.LatencyMode" /> property cannot be set to <see cref="F:System.Runtime.GCLatencyMode.NoGCRegion" />.</exception>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000040F7 File Offset: 0x000022F7
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MonoTODO("Always returns GCLatencyMode.Interactive and ignores set")]
		public static GCLatencyMode LatencyMode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return GCLatencyMode.Interactive;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether a full blocking garbage collection compacts the large object heap (LOH).</summary>
		/// <returns>One of the enumeration values that indicates whether a full blocking garbage collection compacts the LOH.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000C1FE1 File Offset: 0x000C01E1
		// (set) Token: 0x060035B7 RID: 13751 RVA: 0x000C1FE8 File Offset: 0x000C01E8
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			[CompilerGenerated]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return GCSettings.<LargeObjectHeapCompactionMode>k__BackingField;
			}
			[CompilerGenerated]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				GCSettings.<LargeObjectHeapCompactionMode>k__BackingField = value;
			}
		}

		// Token: 0x0400250E RID: 9486
		[CompilerGenerated]
		private static GCLargeObjectHeapCompactionMode <LargeObjectHeapCompactionMode>k__BackingField;
	}
}
