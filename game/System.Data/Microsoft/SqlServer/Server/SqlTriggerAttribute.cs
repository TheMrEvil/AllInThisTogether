using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a trigger in SQL Server. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlTriggerAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to mark the method as a trigger in SQL Server.</summary>
		// Token: 0x0600047C RID: 1148 RVA: 0x00010A22 File Offset: 0x0000EC22
		public SqlTriggerAttribute()
		{
			this.m_fName = null;
			this.m_fTarget = null;
			this.m_fEvent = null;
		}

		/// <summary>The name of the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the trigger.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00010A3F File Offset: 0x0000EC3F
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00010A47 File Offset: 0x0000EC47
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		/// <summary>The table to which the trigger applies.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table name.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00010A50 File Offset: 0x0000EC50
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00010A58 File Offset: 0x0000EC58
		public string Target
		{
			get
			{
				return this.m_fTarget;
			}
			set
			{
				this.m_fTarget = value;
			}
		}

		/// <summary>The type of trigger and what data manipulation language (DML) action activates the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the type of trigger and what data manipulation language (DML) action activates the trigger.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00010A61 File Offset: 0x0000EC61
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00010A69 File Offset: 0x0000EC69
		public string Event
		{
			get
			{
				return this.m_fEvent;
			}
			set
			{
				this.m_fEvent = value;
			}
		}

		// Token: 0x04000553 RID: 1363
		private string m_fName;

		// Token: 0x04000554 RID: 1364
		private string m_fTarget;

		// Token: 0x04000555 RID: 1365
		private string m_fEvent;
	}
}
