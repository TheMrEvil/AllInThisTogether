using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a type definition in an assembly as a user-defined type (UDT) in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class SqlUserDefinedTypeAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined type (UDT), used to confirm that the given type is a UDT and to indicate the storage format of the UDT.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the type.</param>
		// Token: 0x06000491 RID: 1169 RVA: 0x00010B3B File Offset: 0x0000ED3B
		public SqlUserDefinedTypeAttribute(Format format)
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

		/// <summary>The maximum size of the instance, in bytes.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the instance.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00010B68 File Offset: 0x0000ED68
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00010B70 File Offset: 0x0000ED70
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1)
				{
					throw ADP.ArgumentOutOfRange("MaxByteSize");
				}
				this.m_MaxByteSize = value;
			}
		}

		/// <summary>Indicates whether all instances of this user-defined type are the same length.</summary>
		/// <returns>
		///   <see langword="true" /> if all instances of this type are the same length; otherwise <see langword="false" />.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00010B88 File Offset: 0x0000ED88
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00010B90 File Offset: 0x0000ED90
		public bool IsFixedLength
		{
			get
			{
				return this.m_IsFixedLength;
			}
			set
			{
				this.m_IsFixedLength = value;
			}
		}

		/// <summary>Indicates whether the user-defined type is byte ordered.</summary>
		/// <returns>
		///   <see langword="true" /> if the user-defined type is byte ordered; otherwise <see langword="false" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00010B99 File Offset: 0x0000ED99
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00010BA1 File Offset: 0x0000EDA1
		public bool IsByteOrdered
		{
			get
			{
				return this.m_IsByteOrdered;
			}
			set
			{
				this.m_IsByteOrdered = value;
			}
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> value representing the serialization format.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00010BAA File Offset: 0x0000EDAA
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>The name of the method used to validate instances of the user-defined type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the method used to validate instances of the user-defined type.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00010BB2 File Offset: 0x0000EDB2
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00010BBA File Offset: 0x0000EDBA
		public string ValidationMethodName
		{
			get
			{
				return this.m_ValidationMethodName;
			}
			set
			{
				this.m_ValidationMethodName = value;
			}
		}

		/// <summary>The SQL Server name of the user-defined type.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the SQL Server name of the user-defined type.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00010BC3 File Offset: 0x0000EDC3
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00010BCB File Offset: 0x0000EDCB
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

		// Token: 0x04000562 RID: 1378
		private int m_MaxByteSize;

		// Token: 0x04000563 RID: 1379
		private bool m_IsFixedLength;

		// Token: 0x04000564 RID: 1380
		private bool m_IsByteOrdered;

		// Token: 0x04000565 RID: 1381
		private Format m_format;

		// Token: 0x04000566 RID: 1382
		private string m_fName;

		// Token: 0x04000567 RID: 1383
		internal const int YukonMaxByteSizeValue = 8000;

		// Token: 0x04000568 RID: 1384
		private string m_ValidationMethodName;
	}
}
