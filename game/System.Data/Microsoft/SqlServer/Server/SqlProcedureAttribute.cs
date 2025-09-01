using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a stored procedure. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x0200005B RID: 91
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlProcedureAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to indicate that the given method should be registered as a stored procedure in SQL Server.</summary>
		// Token: 0x06000479 RID: 1145 RVA: 0x00010A02 File Offset: 0x0000EC02
		public SqlProcedureAttribute()
		{
			this.m_fName = null;
		}

		/// <summary>The name of the stored procedure.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the stored procedure.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00010A11 File Offset: 0x0000EC11
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00010A19 File Offset: 0x0000EC19
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

		// Token: 0x04000552 RID: 1362
		private string m_fName;
	}
}
