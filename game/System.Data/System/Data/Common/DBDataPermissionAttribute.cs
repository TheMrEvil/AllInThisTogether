using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	// Token: 0x020003D1 RID: 977
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class DBDataPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DBDataPermissionAttribute" />.</summary>
		/// <param name="action">One of the security action values representing an action that can be performed by declarative security.</param>
		// Token: 0x06002F2C RID: 12076 RVA: 0x000CAB04 File Offset: 0x000C8D04
		protected DBDataPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether a blank password is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if a blank password is allowed; otherwise <see langword="false" />.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000CAB0D File Offset: 0x000C8D0D
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000CAB15 File Offset: 0x000C8D15
		public bool AllowBlankPassword
		{
			get
			{
				return this._allowBlankPassword;
			}
			set
			{
				this._allowBlankPassword = value;
			}
		}

		/// <summary>Gets or sets a permitted connection string.</summary>
		/// <returns>A permitted connection string.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000CAB20 File Offset: 0x000C8D20
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000CAB3E File Offset: 0x000C8D3E
		public string ConnectionString
		{
			get
			{
				string connectionString = this._connectionString;
				if (connectionString == null)
				{
					return string.Empty;
				}
				return connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		/// <summary>Identifies whether the list of connection string parameters identified by the <see cref="P:System.Data.Common.DBDataPermissionAttribute.KeyRestrictions" /> property are the only connection string parameters allowed.</summary>
		/// <returns>One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000CAB47 File Offset: 0x000C8D47
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x000CAB4F File Offset: 0x000C8D4F
		public KeyRestrictionBehavior KeyRestrictionBehavior
		{
			get
			{
				return this._behavior;
			}
			set
			{
				if (value <= KeyRestrictionBehavior.PreventUsage)
				{
					this._behavior = value;
					return;
				}
				throw ADP.InvalidKeyRestrictionBehavior(value);
			}
		}

		/// <summary>Gets or sets connection string parameters that are allowed or disallowed.</summary>
		/// <returns>One or more connection string parameters that are allowed or disallowed.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000CAB64 File Offset: 0x000C8D64
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x000CAB82 File Offset: 0x000C8D82
		public string KeyRestrictions
		{
			get
			{
				string restrictions = this._restrictions;
				if (restrictions == null)
				{
					return ADP.StrEmpty;
				}
				return restrictions;
			}
			set
			{
				this._restrictions = value;
			}
		}

		/// <summary>Identifies whether the attribute should serialize the connection string.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute should serialize the connection string; otherwise <see langword="false" />.</returns>
		// Token: 0x06002F35 RID: 12085 RVA: 0x000CAB8B File Offset: 0x000C8D8B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeConnectionString()
		{
			return this._connectionString != null;
		}

		/// <summary>Identifies whether the attribute should serialize the set of key restrictions.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute should serialize the set of key restrictions; otherwise <see langword="false" />.</returns>
		// Token: 0x06002F36 RID: 12086 RVA: 0x000CAB96 File Offset: 0x000C8D96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyRestrictions()
		{
			return this._restrictions != null;
		}

		// Token: 0x04001C6F RID: 7279
		private bool _allowBlankPassword;

		// Token: 0x04001C70 RID: 7280
		private string _connectionString;

		// Token: 0x04001C71 RID: 7281
		private string _restrictions;

		// Token: 0x04001C72 RID: 7282
		private KeyRestrictionBehavior _behavior;
	}
}
