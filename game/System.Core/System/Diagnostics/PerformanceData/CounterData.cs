using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Contains the raw data for a counter.</summary>
	// Token: 0x0200038D RID: 909
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterData
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0000235B File Offset: 0x0000055B
		internal CounterData()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets or gets the raw counter data.</summary>
		/// <returns>The raw counter data.</returns>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0005A304 File Offset: 0x00058504
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0000235B File Offset: 0x0000055B
		public long RawValue
		{
			[SecurityCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			[SecurityCritical]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Sets or gets the counter data.</summary>
		/// <returns>The counter data.</returns>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0005A320 File Offset: 0x00058520
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0000235B File Offset: 0x0000055B
		public long Value
		{
			[SecurityCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			[SecurityCritical]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Decrements the counter value by 1.</summary>
		// Token: 0x06001B40 RID: 6976 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void Decrement()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the counter value by 1.</summary>
		// Token: 0x06001B41 RID: 6977 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void Increment()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the counter value by the specified amount.</summary>
		/// <param name="value">The amount by which to increment the counter value. The increment value can be positive or negative.</param>
		// Token: 0x06001B42 RID: 6978 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void IncrementBy(long value)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
