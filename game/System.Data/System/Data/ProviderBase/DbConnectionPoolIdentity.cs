using System;
using System.Data.SqlClient;
using System.Security.Principal;

namespace System.Data.ProviderBase
{
	// Token: 0x02000361 RID: 865
	[Serializable]
	internal sealed class DbConnectionPoolIdentity
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x000B0521 File Offset: 0x000AE721
		internal static DbConnectionPoolIdentity GetCurrent()
		{
			if (!TdsParserStateObjectFactory.UseManagedSNI)
			{
				return DbConnectionPoolIdentity.GetCurrentNative();
			}
			return DbConnectionPoolIdentity.GetCurrentManaged();
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000B0538 File Offset: 0x000AE738
		private static DbConnectionPoolIdentity GetCurrentNative()
		{
			DbConnectionPoolIdentity result;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				IntPtr token = current.AccessToken.DangerousGetHandle();
				bool flag = current.User.IsWellKnown(WellKnownSidType.NetworkSid);
				string value = current.User.Value;
				bool flag2 = Win32NativeMethods.IsTokenRestrictedWrapper(token);
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.s_lastIdentity;
				if (dbConnectionPoolIdentity != null && dbConnectionPoolIdentity._sidString == value && dbConnectionPoolIdentity._isRestricted == flag2 && dbConnectionPoolIdentity._isNetwork == flag)
				{
					result = dbConnectionPoolIdentity;
				}
				else
				{
					result = new DbConnectionPoolIdentity(value, flag2, flag);
				}
			}
			DbConnectionPoolIdentity.s_lastIdentity = result;
			return result;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000B05D8 File Offset: 0x000AE7D8
		private DbConnectionPoolIdentity(string sidString, bool isRestricted, bool isNetwork)
		{
			this._sidString = sidString;
			this._isRestricted = isRestricted;
			this._isNetwork = isNetwork;
			this._hashCode = ((sidString == null) ? 0 : sidString.GetHashCode());
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x000B0607 File Offset: 0x000AE807
		internal bool IsRestricted
		{
			get
			{
				return this._isRestricted;
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000B0610 File Offset: 0x000AE810
		public override bool Equals(object value)
		{
			bool flag = this == DbConnectionPoolIdentity.NoIdentity || this == value;
			if (!flag && value != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = (DbConnectionPoolIdentity)value;
				flag = (this._sidString == dbConnectionPoolIdentity._sidString && this._isRestricted == dbConnectionPoolIdentity._isRestricted && this._isNetwork == dbConnectionPoolIdentity._isNetwork);
			}
			return flag;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000B066E File Offset: 0x000AE86E
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000B0678 File Offset: 0x000AE878
		internal static DbConnectionPoolIdentity GetCurrentManaged()
		{
			string sidString = ((!string.IsNullOrWhiteSpace(Environment.UserDomainName)) ? (Environment.UserDomainName + "\\") : "") + Environment.UserName;
			bool isNetwork = false;
			bool isRestricted = false;
			return new DbConnectionPoolIdentity(sidString, isRestricted, isNetwork);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000B06BC File Offset: 0x000AE8BC
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionPoolIdentity()
		{
		}

		// Token: 0x040019C5 RID: 6597
		private static DbConnectionPoolIdentity s_lastIdentity = null;

		// Token: 0x040019C6 RID: 6598
		public static readonly DbConnectionPoolIdentity NoIdentity = new DbConnectionPoolIdentity(string.Empty, false, true);

		// Token: 0x040019C7 RID: 6599
		private readonly string _sidString;

		// Token: 0x040019C8 RID: 6600
		private readonly bool _isRestricted;

		// Token: 0x040019C9 RID: 6601
		private readonly bool _isNetwork;

		// Token: 0x040019CA RID: 6602
		private readonly int _hashCode;
	}
}
