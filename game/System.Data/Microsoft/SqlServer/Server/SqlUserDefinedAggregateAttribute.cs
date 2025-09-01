using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates that the type should be registered as a user-defined aggregate. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SqlUserDefinedAggregateAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined aggregate, used to indicate that the given type is a user-defined aggregate and the storage format of the user-defined aggregate.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the aggregate.</param>
		// Token: 0x06000483 RID: 1155 RVA: 0x00010A72 File Offset: 0x0000EC72
		public SqlUserDefinedAggregateAttribute(Format format)
		{
			if (format == Format.Unknown)
			{
				throw ADP.NotSupportedUserDefinedTypeSerializationFormat(format, "format");
			}
			if (format - Format.Native > 1)
			{
				throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
			}
			this.m_format = format;
		}

		/// <summary>The maximum size, in bytes, of the aggregate instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the aggregate instance.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00010AA6 File Offset: 0x0000ECA6
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00010AAE File Offset: 0x0000ECAE
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1 || value > 8000)
				{
					throw ADP.ArgumentOutOfRange(Res.GetString("range: 0-8000"), "MaxByteSize", value);
				}
				this.m_MaxByteSize = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to duplicates.</summary>
		/// <returns>
		///   <see langword="true" /> if the aggregate is invariant to duplicates; otherwise <see langword="false" />.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00010ADE File Offset: 0x0000ECDE
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00010AE6 File Offset: 0x0000ECE6
		public bool IsInvariantToDuplicates
		{
			get
			{
				return this.m_fInvariantToDup;
			}
			set
			{
				this.m_fInvariantToDup = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to nulls.</summary>
		/// <returns>
		///   <see langword="true" /> if the aggregate is invariant to nulls; otherwise <see langword="false" />.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00010AEF File Offset: 0x0000ECEF
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00010AF7 File Offset: 0x0000ECF7
		public bool IsInvariantToNulls
		{
			get
			{
				return this.m_fInvariantToNulls;
			}
			set
			{
				this.m_fInvariantToNulls = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to order.</summary>
		/// <returns>
		///   <see langword="true" /> if the aggregate is invariant to order; otherwise <see langword="false" />.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00010B00 File Offset: 0x0000ED00
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00010B08 File Offset: 0x0000ED08
		public bool IsInvariantToOrder
		{
			get
			{
				return this.m_fInvariantToOrder;
			}
			set
			{
				this.m_fInvariantToOrder = value;
			}
		}

		/// <summary>Indicates whether the aggregate returns <see langword="null" /> if no values have been accumulated.</summary>
		/// <returns>
		///   <see langword="true" /> if the aggregate returns <see langword="null" /> if no values have been accumulated; otherwise <see langword="false" />.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00010B11 File Offset: 0x0000ED11
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00010B19 File Offset: 0x0000ED19
		public bool IsNullIfEmpty
		{
			get
			{
				return this.m_fNullIfEmpty;
			}
			set
			{
				this.m_fNullIfEmpty = value;
			}
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> representing the serialization format.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00010B22 File Offset: 0x0000ED22
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>The name of the aggregate.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the aggregate.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00010B2A File Offset: 0x0000ED2A
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00010B32 File Offset: 0x0000ED32
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

		// Token: 0x04000556 RID: 1366
		private int m_MaxByteSize;

		// Token: 0x04000557 RID: 1367
		private bool m_fInvariantToDup;

		// Token: 0x04000558 RID: 1368
		private bool m_fInvariantToNulls;

		// Token: 0x04000559 RID: 1369
		private bool m_fInvariantToOrder = true;

		// Token: 0x0400055A RID: 1370
		private bool m_fNullIfEmpty;

		// Token: 0x0400055B RID: 1371
		private Format m_format;

		// Token: 0x0400055C RID: 1372
		private string m_fName;

		/// <summary>The maximum size, in bytes, required to store the state of this aggregate instance during computation.</summary>
		// Token: 0x0400055D RID: 1373
		public const int MaxByteSizeValue = 8000;
	}
}
