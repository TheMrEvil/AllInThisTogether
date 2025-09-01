using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.OleDb.OleDbConnection" /> class.</summary>
	// Token: 0x02000160 RID: 352
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbConnectionStringBuilder : DbConnectionStringBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> class.</summary>
		// Token: 0x060012F7 RID: 4855 RVA: 0x0005AC85 File Offset: 0x00058E85
		public OleDbConnectionStringBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into key/value pairs.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		// Token: 0x060012F8 RID: 4856 RVA: 0x0005AC85 File Offset: 0x00058E85
		public OleDbConnectionStringBuilder(string connectionString)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets the name of the data source to connect to.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.DataSource" /> property, or <see langword="String.Empty" /> if none has been supplied.</returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x00007EED File Offset: 0x000060ED
		public string DataSource
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the Universal Data Link (UDL) file for connecting to the data source.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.FileName" /> property, or <see langword="String.Empty" /> if none has been supplied.</returns>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x00007EED File Offset: 0x000060ED
		public string FileName
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00007EED File Offset: 0x000060ED
		public object Item
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00007EED File Offset: 0x000060ED
		public override ICollection Keys
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value to be passed for the OLE DB Services key within the connection string.</summary>
		/// <returns>The value corresponding to the OLE DB Services key within the connection string. By default, the value is -13.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00007EED File Offset: 0x000060ED
		public int OleDbServices
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether security-sensitive information, such as the password, is returned as part of the connection if the connection is open or has ever been in an open state.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.PersistSecurityInfo" /> property, or <see langword="false" /> if none has been supplied.</returns>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00007EED File Offset: 0x000060ED
		public bool PersistSecurityInfo
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a string that contains the name of the data provider associated with the internal connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.Provider" /> property, or <see langword="String.Empty" /> if none has been supplied.</returns>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00007EED File Offset: 0x000060ED
		public string Provider
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> instance.</summary>
		// Token: 0x06001307 RID: 4871 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Clear()
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> contains a specific key.</summary>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x06001308 RID: 4872 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool ContainsKey(string keyword)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> instance.</summary>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the key existed within the connection string and was removed, <see langword="false" /> if the key did not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x0600130A RID: 4874 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool Remove(string keyword)
		{
			throw ADP.OleDb();
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public bool TryGetValue(string keyword, object value)
		{
			throw ADP.OleDb();
		}
	}
}
