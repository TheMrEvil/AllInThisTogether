using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Sql
{
	/// <summary>Provides a mechanism for enumerating all available instances of SQL Server within the local network.</summary>
	// Token: 0x02000177 RID: 375
	public sealed class SqlDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x060013E7 RID: 5095 RVA: 0x0005AEFB File Offset: 0x000590FB
		private SqlDataSourceEnumerator()
		{
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />, which can be used to retrieve information about available SQL Server instances.</summary>
		/// <returns>An instance of the <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> used to retrieve information about available SQL Server instances.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0005AF03 File Offset: 0x00059103
		public static SqlDataSourceEnumerator Instance
		{
			get
			{
				return SqlDataSourceEnumerator.SingletonInstance;
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Data.DataTable" /> containing information about all visible SQL Server 2000 or SQL Server 2005 instances.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> containing information about the visible SQL Server instances.</returns>
		// Token: 0x060013E9 RID: 5097 RVA: 0x0005AF0A File Offset: 0x0005910A
		public override DataTable GetDataSources()
		{
			this.timeoutTime = 0L;
			throw new NotImplementedException();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0005AF1C File Offset: 0x0005911C
		private static DataTable ParseServerEnumString(string serverInstances)
		{
			DataTable dataTable = new DataTable("SqlDataSources");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ServerName", typeof(string));
			dataTable.Columns.Add("InstanceName", typeof(string));
			dataTable.Columns.Add("IsClustered", typeof(string));
			dataTable.Columns.Add("Version", typeof(string));
			string text = null;
			string text2 = null;
			string text3 = null;
			string value = null;
			string[] array = serverInstances.Split('\0', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text4 = array[i].Trim('\0');
				if (text4.Length != 0)
				{
					foreach (string text5 in text4.Split(';', StringSplitOptions.None))
					{
						if (text == null)
						{
							foreach (string text6 in text5.Split('\\', StringSplitOptions.None))
							{
								if (text == null)
								{
									text = text6;
								}
								else
								{
									text2 = text6;
								}
							}
						}
						else if (text3 == null)
						{
							text3 = text5.Substring(SqlDataSourceEnumerator._clusterLength);
						}
						else
						{
							value = text5.Substring(SqlDataSourceEnumerator._versionLength);
						}
					}
					string text7 = "ServerName='" + text + "'";
					if (!ADP.IsEmpty(text2))
					{
						text7 = text7 + " AND InstanceName='" + text2 + "'";
					}
					if (dataTable.Select(text7).Length == 0)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = text;
						dataRow[1] = text2;
						dataRow[2] = text3;
						dataRow[3] = value;
						dataTable.Rows.Add(dataRow);
					}
					text = null;
					text2 = null;
					text3 = null;
					value = null;
				}
			}
			foreach (object obj in dataTable.Columns)
			{
				((DataColumn)obj).ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0005B13C File Offset: 0x0005933C
		// Note: this type is marked as 'beforefieldinit'.
		static SqlDataSourceEnumerator()
		{
		}

		// Token: 0x04000C2A RID: 3114
		private static readonly SqlDataSourceEnumerator SingletonInstance = new SqlDataSourceEnumerator();

		// Token: 0x04000C2B RID: 3115
		internal const string ServerName = "ServerName";

		// Token: 0x04000C2C RID: 3116
		internal const string InstanceName = "InstanceName";

		// Token: 0x04000C2D RID: 3117
		internal const string IsClustered = "IsClustered";

		// Token: 0x04000C2E RID: 3118
		internal const string Version = "Version";

		// Token: 0x04000C2F RID: 3119
		private long timeoutTime;

		// Token: 0x04000C30 RID: 3120
		private static string _Version = "Version:";

		// Token: 0x04000C31 RID: 3121
		private static string _Cluster = "Clustered:";

		// Token: 0x04000C32 RID: 3122
		private static int _clusterLength = SqlDataSourceEnumerator._Cluster.Length;

		// Token: 0x04000C33 RID: 3123
		private static int _versionLength = SqlDataSourceEnumerator._Version.Length;
	}
}
