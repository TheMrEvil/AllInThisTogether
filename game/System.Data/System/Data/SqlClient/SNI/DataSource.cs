using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200029C RID: 668
	internal class DataSource
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000913FB File Offset: 0x0008F5FB
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x00091403 File Offset: 0x0008F603
		internal string ServerName
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ServerName>k__BackingField = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0009140C File Offset: 0x0008F60C
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x00091414 File Offset: 0x0008F614
		internal int Port
		{
			[CompilerGenerated]
			get
			{
				return this.<Port>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Port>k__BackingField = value;
			}
		} = -1;

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0009141D File Offset: 0x0008F61D
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x00091425 File Offset: 0x0008F625
		public string InstanceName
		{
			[CompilerGenerated]
			get
			{
				return this.<InstanceName>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<InstanceName>k__BackingField = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0009142E File Offset: 0x0008F62E
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x00091436 File Offset: 0x0008F636
		public string PipeName
		{
			[CompilerGenerated]
			get
			{
				return this.<PipeName>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PipeName>k__BackingField = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0009143F File Offset: 0x0008F63F
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x00091447 File Offset: 0x0008F647
		public string PipeHostName
		{
			[CompilerGenerated]
			get
			{
				return this.<PipeHostName>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PipeHostName>k__BackingField = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x00091450 File Offset: 0x0008F650
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x00091458 File Offset: 0x0008F658
		internal bool IsBadDataSource
		{
			[CompilerGenerated]
			get
			{
				return this.<IsBadDataSource>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsBadDataSource>k__BackingField = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00091461 File Offset: 0x0008F661
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x00091469 File Offset: 0x0008F669
		internal bool IsSsrpRequired
		{
			[CompilerGenerated]
			get
			{
				return this.<IsSsrpRequired>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsSsrpRequired>k__BackingField = value;
			}
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00091474 File Offset: 0x0008F674
		private DataSource(string dataSource)
		{
			this._workingDataSource = dataSource.Trim().ToLowerInvariant();
			int num = this._workingDataSource.IndexOf(':');
			this.PopulateProtocol();
			this._dataSourceAfterTrimmingProtocol = ((num > -1 && this.ConnectionProtocol != DataSource.Protocol.None) ? this._workingDataSource.Substring(num + 1).Trim() : this._workingDataSource);
			if (this._dataSourceAfterTrimmingProtocol.Contains("/"))
			{
				if (this.ConnectionProtocol == DataSource.Protocol.None)
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.NP)
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.TCP)
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
				}
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0009152C File Offset: 0x0008F72C
		private void PopulateProtocol()
		{
			string[] array = this._workingDataSource.Split(':', StringSplitOptions.None);
			if (array.Length <= 1)
			{
				this.ConnectionProtocol = DataSource.Protocol.None;
				return;
			}
			string a = array[0].Trim();
			if (a == "tcp")
			{
				this.ConnectionProtocol = DataSource.Protocol.TCP;
				return;
			}
			if (a == "np")
			{
				this.ConnectionProtocol = DataSource.Protocol.NP;
				return;
			}
			if (!(a == "admin"))
			{
				this.ConnectionProtocol = DataSource.Protocol.None;
				return;
			}
			this.ConnectionProtocol = DataSource.Protocol.Admin;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000915A8 File Offset: 0x0008F7A8
		public static string GetLocalDBInstance(string dataSource, out bool error)
		{
			string result = null;
			string[] array = dataSource.ToLowerInvariant().Split('\\', StringSplitOptions.None);
			error = false;
			if (array.Length == 2 && "(localdb)".Equals(array[0].TrimStart()))
			{
				if (string.IsNullOrWhiteSpace(array[1]))
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 51U, string.Empty);
					error = true;
					return null;
				}
				result = array[1].Trim();
			}
			return result;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00091618 File Offset: 0x0008F818
		public static DataSource ParseServerName(string dataSource)
		{
			DataSource dataSource2 = new DataSource(dataSource);
			if (dataSource2.IsBadDataSource)
			{
				return null;
			}
			if (dataSource2.InferNamedPipesInformation())
			{
				return dataSource2;
			}
			if (dataSource2.IsBadDataSource)
			{
				return null;
			}
			if (dataSource2.InferConnectionDetails())
			{
				return dataSource2;
			}
			return null;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00091655 File Offset: 0x0008F855
		private void InferLocalServerName()
		{
			if (string.IsNullOrEmpty(this.ServerName) || DataSource.IsLocalHost(this.ServerName))
			{
				this.ServerName = ((this.ConnectionProtocol == DataSource.Protocol.Admin) ? Environment.MachineName : "localhost");
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0009168C File Offset: 0x0008F88C
		private bool InferConnectionDetails()
		{
			string[] array = this._dataSourceAfterTrimmingProtocol.Split(new char[]
			{
				'\\',
				','
			});
			this.ServerName = array[0].Trim();
			int num = this._dataSourceAfterTrimmingProtocol.IndexOf(',');
			int num2 = this._dataSourceAfterTrimmingProtocol.IndexOf('\\');
			if (num > -1)
			{
				string text = (num2 > -1) ? ((num > num2) ? array[2].Trim() : array[1].Trim()) : array[1].Trim();
				if (string.IsNullOrEmpty(text))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.None)
				{
					this.ConnectionProtocol = DataSource.Protocol.TCP;
				}
				else if (this.ConnectionProtocol != DataSource.Protocol.TCP)
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				int num3;
				if (!int.TryParse(text, out num3))
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
					return false;
				}
				if (num3 < 1)
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
					return false;
				}
				this.Port = num3;
			}
			else if (num2 > -1)
			{
				this.InstanceName = array[1].Trim();
				if (string.IsNullOrWhiteSpace(this.InstanceName))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				if ("mssqlserver".Equals(this.InstanceName))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				this.IsSsrpRequired = true;
			}
			this.InferLocalServerName();
			return true;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000917BF File Offset: 0x0008F9BF
		private void ReportSNIError(SNIProviders provider)
		{
			SNILoadHandle.SingletonInstance.LastError = new SNIError(provider, 0U, 25U, string.Empty);
			this.IsBadDataSource = true;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000917E0 File Offset: 0x0008F9E0
		private bool InferNamedPipesInformation()
		{
			if (!this._dataSourceAfterTrimmingProtocol.StartsWith("\\\\") && this.ConnectionProtocol != DataSource.Protocol.NP)
			{
				return false;
			}
			if (!this._dataSourceAfterTrimmingProtocol.Contains('\\'))
			{
				this.PipeHostName = (this.ServerName = this._dataSourceAfterTrimmingProtocol);
				this.InferLocalServerName();
				this.PipeName = "sql\\query";
				return true;
			}
			try
			{
				string[] array = this._dataSourceAfterTrimmingProtocol.Split('\\', StringSplitOptions.None);
				if (array.Length < 6)
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				string text = array[2];
				if (string.IsNullOrEmpty(text))
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				if (!"pipe".Equals(array[3]))
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				if (array[4].StartsWith("mssql$"))
				{
					this.InstanceName = array[4].Substring("mssql$".Length);
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 4; i < array.Length - 1; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(array[array.Length - 1]);
				this.PipeName = stringBuilder.ToString();
				if (string.IsNullOrWhiteSpace(this.InstanceName) && !"sql\\query".Equals(this.PipeName))
				{
					this.InstanceName = "pipe" + this.PipeName;
				}
				this.ServerName = (DataSource.IsLocalHost(text) ? Environment.MachineName : text);
				this.PipeHostName = text;
			}
			catch (UriFormatException)
			{
				this.ReportSNIError(SNIProviders.NP_PROV);
				return false;
			}
			if (this.ConnectionProtocol == DataSource.Protocol.None)
			{
				this.ConnectionProtocol = DataSource.Protocol.NP;
			}
			else if (this.ConnectionProtocol != DataSource.Protocol.NP)
			{
				this.ReportSNIError(SNIProviders.NP_PROV);
				return false;
			}
			return true;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000919BC File Offset: 0x0008FBBC
		private static bool IsLocalHost(string serverName)
		{
			return ".".Equals(serverName) || "(local)".Equals(serverName) || "localhost".Equals(serverName);
		}

		// Token: 0x0400154D RID: 5453
		private const char CommaSeparator = ',';

		// Token: 0x0400154E RID: 5454
		private const char BackSlashSeparator = '\\';

		// Token: 0x0400154F RID: 5455
		private const string DefaultHostName = "localhost";

		// Token: 0x04001550 RID: 5456
		private const string DefaultSqlServerInstanceName = "mssqlserver";

		// Token: 0x04001551 RID: 5457
		private const string PipeBeginning = "\\\\";

		// Token: 0x04001552 RID: 5458
		private const string PipeToken = "pipe";

		// Token: 0x04001553 RID: 5459
		private const string LocalDbHost = "(localdb)";

		// Token: 0x04001554 RID: 5460
		private const string NamedPipeInstanceNameHeader = "mssql$";

		// Token: 0x04001555 RID: 5461
		private const string DefaultPipeName = "sql\\query";

		// Token: 0x04001556 RID: 5462
		internal DataSource.Protocol ConnectionProtocol = DataSource.Protocol.None;

		// Token: 0x04001557 RID: 5463
		[CompilerGenerated]
		private string <ServerName>k__BackingField;

		// Token: 0x04001558 RID: 5464
		[CompilerGenerated]
		private int <Port>k__BackingField;

		// Token: 0x04001559 RID: 5465
		[CompilerGenerated]
		private string <InstanceName>k__BackingField;

		// Token: 0x0400155A RID: 5466
		[CompilerGenerated]
		private string <PipeName>k__BackingField;

		// Token: 0x0400155B RID: 5467
		[CompilerGenerated]
		private string <PipeHostName>k__BackingField;

		// Token: 0x0400155C RID: 5468
		private string _workingDataSource;

		// Token: 0x0400155D RID: 5469
		private string _dataSourceAfterTrimmingProtocol;

		// Token: 0x0400155E RID: 5470
		[CompilerGenerated]
		private bool <IsBadDataSource>k__BackingField;

		// Token: 0x0400155F RID: 5471
		[CompilerGenerated]
		private bool <IsSsrpRequired>k__BackingField;

		// Token: 0x0200029D RID: 669
		internal enum Protocol
		{
			// Token: 0x04001561 RID: 5473
			TCP,
			// Token: 0x04001562 RID: 5474
			NP,
			// Token: 0x04001563 RID: 5475
			None,
			// Token: 0x04001564 RID: 5476
			Admin
		}
	}
}
