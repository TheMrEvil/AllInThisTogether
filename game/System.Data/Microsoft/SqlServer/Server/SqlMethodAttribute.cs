using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates the determinism and data access properties of a method or property on a user-defined type (UDT). The properties on the attribute reflect the physical characteristics that are used when the type is registered with SQL Server.</summary>
	// Token: 0x0200005A RID: 90
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlMethodAttribute : SqlFunctionAttribute
	{
		/// <summary>An attribute on a user-defined type (UDT), used to indicate the determinism and data access properties of a method or a property on a UDT.</summary>
		// Token: 0x06000472 RID: 1138 RVA: 0x000109B2 File Offset: 0x0000EBB2
		public SqlMethodAttribute()
		{
			this.m_fCallOnNullInputs = true;
			this.m_fMutator = false;
			this.m_fInvokeIfReceiverIsNull = false;
		}

		/// <summary>Indicates whether the method on a user-defined type (UDT) is called when <see langword="null" /> input arguments are specified in the method invocation.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is called when <see langword="null" /> input arguments are specified in the method invocation; <see langword="false" /> if the method returns a <see langword="null" /> value when any of its input parameters are <see langword="null" />. If the method cannot be invoked (because of an attribute on the method), the SQL Server <see langword="DbNull" /> is returned.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000109CF File Offset: 0x0000EBCF
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000109D7 File Offset: 0x0000EBD7
		public bool OnNullCall
		{
			get
			{
				return this.m_fCallOnNullInputs;
			}
			set
			{
				this.m_fCallOnNullInputs = value;
			}
		}

		/// <summary>Indicates whether a method on a user-defined type (UDT) is a mutator.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is a mutator; otherwise <see langword="false" />.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000109E0 File Offset: 0x0000EBE0
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000109E8 File Offset: 0x0000EBE8
		public bool IsMutator
		{
			get
			{
				return this.m_fMutator;
			}
			set
			{
				this.m_fMutator = value;
			}
		}

		/// <summary>Indicates whether SQL Server should invoke the method on null instances.</summary>
		/// <returns>
		///   <see langword="true" /> if SQL Server should invoke the method on null instances; otherwise, <see langword="false" />. If the method cannot be invoked (because of an attribute on the method), the SQL Server <see langword="DbNull" /> is returned.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000109F1 File Offset: 0x0000EBF1
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000109F9 File Offset: 0x0000EBF9
		public bool InvokeIfReceiverIsNull
		{
			get
			{
				return this.m_fInvokeIfReceiverIsNull;
			}
			set
			{
				this.m_fInvokeIfReceiverIsNull = value;
			}
		}

		// Token: 0x0400054F RID: 1359
		private bool m_fCallOnNullInputs;

		// Token: 0x04000550 RID: 1360
		private bool m_fMutator;

		// Token: 0x04000551 RID: 1361
		private bool m_fInvokeIfReceiverIsNull;
	}
}
