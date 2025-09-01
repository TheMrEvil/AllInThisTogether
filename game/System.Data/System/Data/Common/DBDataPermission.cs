using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Enables a .NET Framework data provider to help ensure that a user has a security level adequate for accessing data.</summary>
	// Token: 0x020003CF RID: 975
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class DBDataPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of a <see langword="DBDataPermission" /> class.</summary>
		// Token: 0x06002F15 RID: 12053 RVA: 0x000CA22C File Offset: 0x000C842C
		[Obsolete("DBDataPermission() has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission() : this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of a <see langword="DBDataPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06002F16 RID: 12054 RVA: 0x000CA235 File Offset: 0x000C8435
		protected DBDataPermission(PermissionState state)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (state == PermissionState.Unrestricted)
			{
				this._isUnrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this._isUnrestricted = false;
				return;
			}
			throw ADP.InvalidPermissionState(state);
		}

		/// <summary>Initializes a new instance of a <see langword="DBDataPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value, and a value indicating whether a blank password is allowed.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <param name="allowBlankPassword">
		///   <see langword="true" /> to indicate that a blank password is allowed; otherwise, <see langword="false" />.</param>
		// Token: 0x06002F17 RID: 12055 RVA: 0x000CA265 File Offset: 0x000C8465
		[Obsolete("DBDataPermission(PermissionState state,Boolean allowBlankPassword) has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			this.AllowBlankPassword = allowBlankPassword;
		}

		/// <summary>Initializes a new instance of a <see langword="DBDataPermission" /> class using an existing <see langword="DBDataPermission" />.</summary>
		/// <param name="permission">An existing <see langword="DBDataPermission" /> used to create a new <see langword="DBDataPermission" />.</param>
		// Token: 0x06002F18 RID: 12056 RVA: 0x000CA275 File Offset: 0x000C8475
		protected DBDataPermission(DBDataPermission permission)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permission == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this.CopyFrom(permission);
		}

		/// <summary>Initializes a new instance of a <see langword="DBDataPermission" /> class with the specified <see langword="DBDataPermissionAttribute" />.</summary>
		/// <param name="permissionAttribute">A security action associated with a custom security attribute.</param>
		// Token: 0x06002F19 RID: 12057 RVA: 0x000CA2A0 File Offset: 0x000C84A0
		protected DBDataPermission(DBDataPermissionAttribute permissionAttribute)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permissionAttribute == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this._isUnrestricted = permissionAttribute.Unrestricted;
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permissionAttribute.AllowBlankPassword;
				if (permissionAttribute.ShouldSerializeConnectionString() || permissionAttribute.ShouldSerializeKeyRestrictions())
				{
					this.Add(permissionAttribute.ConnectionString, permissionAttribute.KeyRestrictions, permissionAttribute.KeyRestrictionBehavior);
				}
			}
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000CA314 File Offset: 0x000C8514
		internal DBDataPermission(DbConnectionOptions connectionOptions)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (connectionOptions != null)
			{
				this._allowBlankPassword = connectionOptions.HasBlankPassword;
				this.AddPermissionEntry(new DBConnectionString(connectionOptions));
			}
		}

		/// <summary>Gets a value indicating whether a blank password is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if a blank password is allowed, otherwise, <see langword="false" />.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000CA342 File Offset: 0x000C8542
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000CA34A File Offset: 0x000C854A
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

		/// <summary>Adds access for the specified connection string to the existing state of the <see langword="DBDataPermission" />.</summary>
		/// <param name="connectionString">A permitted connection string.</param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> properties.</param>
		// Token: 0x06002F1D RID: 12061 RVA: 0x000CA354 File Offset: 0x000C8554
		public virtual void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, null, false);
			this.AddPermissionEntry(entry);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000CA374 File Offset: 0x000C8574
		internal void AddPermissionEntry(DBConnectionString entry)
		{
			if (this._keyvaluetree == null)
			{
				this._keyvaluetree = new NameValuePermission();
			}
			if (this._keyvalues == null)
			{
				this._keyvalues = new ArrayList();
			}
			NameValuePermission.AddEntry(this._keyvaluetree, this._keyvalues, entry);
			this._isUnrestricted = false;
		}

		/// <summary>Removes all permissions that were previous added using the <see cref="M:System.Data.Common.DBDataPermission.Add(System.String,System.String,System.Data.KeyRestrictionBehavior)" /> method.</summary>
		// Token: 0x06002F1F RID: 12063 RVA: 0x000CA3C0 File Offset: 0x000C85C0
		protected void Clear()
		{
			this._keyvaluetree = null;
			this._keyvalues = null;
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06002F20 RID: 12064 RVA: 0x000CA3D0 File Offset: 0x000C85D0
		public override IPermission Copy()
		{
			DBDataPermission dbdataPermission = this.CreateInstance();
			dbdataPermission.CopyFrom(this);
			return dbdataPermission;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000CA3E0 File Offset: 0x000C85E0
		private void CopyFrom(DBDataPermission permission)
		{
			this._isUnrestricted = permission.IsUnrestricted();
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permission.AllowBlankPassword;
				if (permission._keyvalues != null)
				{
					this._keyvalues = (ArrayList)permission._keyvalues.Clone();
					if (permission._keyvaluetree != null)
					{
						this._keyvaluetree = permission._keyvaluetree.CopyNameValue();
					}
				}
			}
		}

		/// <summary>Creates a new instance of the <see langword="DBDataPermission" /> class.</summary>
		/// <returns>A new <see langword="DBDataPermission" /> object.</returns>
		// Token: 0x06002F22 RID: 12066 RVA: 0x000CA444 File Offset: 0x000C8644
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected virtual DBDataPermission CreateInstance()
		{
			return Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null) as DBDataPermission;
		}

		/// <summary>Returns a new permission object representing the intersection of the current permission object and the specified permission object.</summary>
		/// <param name="target">A permission object to intersect with the current permission object. It must be of the same type as the current permission object.</param>
		/// <returns>A new permission object that represents the intersection of the current permission object and the specified permission object. This new permission object is a null reference (<see langword="Nothing" /> in Visual Basic) if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic) and is not an instance of the same class as the current permission object.</exception>
		// Token: 0x06002F23 RID: 12067 RVA: 0x000CA460 File Offset: 0x000C8660
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target;
			if (dbdataPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission2 = (DBDataPermission)dbdataPermission.Copy();
			dbdataPermission2._allowBlankPassword &= this.AllowBlankPassword;
			if (this._keyvalues != null && dbdataPermission2._keyvalues != null)
			{
				dbdataPermission2._keyvalues.Clear();
				dbdataPermission2._keyvaluetree.Intersect(dbdataPermission2._keyvalues, this._keyvaluetree);
			}
			else
			{
				dbdataPermission2._keyvalues = null;
				dbdataPermission2._keyvaluetree = null;
			}
			if (dbdataPermission2.IsEmpty())
			{
				dbdataPermission2 = null;
			}
			return dbdataPermission2;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000CA51C File Offset: 0x000C871C
		private bool IsEmpty()
		{
			ArrayList keyvalues = this._keyvalues;
			return !this.IsUnrestricted() && !this.AllowBlankPassword && (keyvalues == null || keyvalues.Count == 0);
		}

		/// <summary>Returns a value indicating whether the current permission object is a subset of the specified permission object.</summary>
		/// <param name="target">A permission object that is to be tested for the subset relationship. This object must be of the same type as the current permission object.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission object is a subset of the specified permission object, otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is an object that is not of the same type as the current permission object.</exception>
		// Token: 0x06002F25 RID: 12069 RVA: 0x000CA550 File Offset: 0x000C8750
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			DBDataPermission dbdataPermission = target as DBDataPermission;
			bool flag = dbdataPermission.IsUnrestricted();
			if (!flag && !this.IsUnrestricted() && (!this.AllowBlankPassword || dbdataPermission.AllowBlankPassword) && (this._keyvalues == null || dbdataPermission._keyvaluetree != null))
			{
				flag = true;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString parsetable = (DBConnectionString)obj;
						if (!dbdataPermission._keyvaluetree.CheckValueForKeyPermit(parsetable))
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		/// <summary>Returns a value indicating whether the permission can be represented as unrestricted without any knowledge of the permission semantics.</summary>
		/// <returns>
		///   <see langword="true" /> if the permission can be represented as unrestricted.</returns>
		// Token: 0x06002F26 RID: 12070 RVA: 0x000CA620 File Offset: 0x000C8820
		public bool IsUnrestricted()
		{
			return this._isUnrestricted;
		}

		/// <summary>Returns a new permission object that is the union of the current and specified permission objects.</summary>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object.</param>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> object is not the same type as the current permission object.</exception>
		// Token: 0x06002F27 RID: 12071 RVA: 0x000CA628 File Offset: 0x000C8828
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target.Copy();
			if (!dbdataPermission.IsUnrestricted())
			{
				dbdataPermission._allowBlankPassword |= this.AllowBlankPassword;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString entry = (DBConnectionString)obj;
						dbdataPermission.AddPermissionEntry(entry);
					}
				}
			}
			if (!dbdataPermission.IsEmpty())
			{
				return dbdataPermission;
			}
			return null;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000CA6F0 File Offset: 0x000C88F0
		private string DecodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace("&quot;", "\"");
				value = value.Replace("&apos;", "'");
				value = value.Replace("&lt;", "<");
				value = value.Replace("&gt;", ">");
				value = value.Replace("&amp;", "&");
			}
			return value;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000CA764 File Offset: 0x000C8964
		private string EncodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace('\0', ' ');
				value = value.Trim();
				value = value.Replace("&", "&amp;");
				value = value.Replace(">", "&gt;");
				value = value.Replace("<", "&lt;");
				value = value.Replace("'", "&apos;");
				value = value.Replace("\"", "&quot;");
			}
			return value;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06002F2A RID: 12074 RVA: 0x000CA7EC File Offset: 0x000C89EC
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw ADP.ArgumentNull("securityElement");
			}
			string tag = securityElement.Tag;
			if (!tag.Equals("Permission") && !tag.Equals("IPermission"))
			{
				throw ADP.NotAPermissionElement();
			}
			string text = securityElement.Attribute("version");
			if (text != null && !text.Equals("1"))
			{
				throw ADP.InvalidXMLBadVersion();
			}
			string text2 = securityElement.Attribute("Unrestricted");
			this._isUnrestricted = (text2 != null && bool.Parse(text2));
			this.Clear();
			if (!this._isUnrestricted)
			{
				string text3 = securityElement.Attribute("AllowBlankPassword");
				this._allowBlankPassword = (text3 != null && bool.Parse(text3));
				ArrayList children = securityElement.Children;
				if (children == null)
				{
					return;
				}
				using (IEnumerator enumerator = children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement2 = (SecurityElement)obj;
						tag = securityElement2.Tag;
						if ("add" == tag || (tag != null && "add" == tag.ToLower(CultureInfo.InvariantCulture)))
						{
							string text4 = securityElement2.Attribute("ConnectionString");
							string text5 = securityElement2.Attribute("KeyRestrictions");
							string text6 = securityElement2.Attribute("KeyRestrictionBehavior");
							KeyRestrictionBehavior behavior = KeyRestrictionBehavior.AllowOnly;
							if (text6 != null)
							{
								behavior = (KeyRestrictionBehavior)Enum.Parse(typeof(KeyRestrictionBehavior), text6, true);
							}
							text4 = this.DecodeXmlValue(text4);
							text5 = this.DecodeXmlValue(text5);
							this.Add(text4, text5, behavior);
						}
					}
					return;
				}
			}
			this._allowBlankPassword = false;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002F2B RID: 12075 RVA: 0x000CA9A0 File Offset: 0x000C8BA0
		public override SecurityElement ToXml()
		{
			Type type = base.GetType();
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.AssemblyQualifiedName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("AllowBlankPassword", this._allowBlankPassword.ToString(CultureInfo.InvariantCulture));
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						SecurityElement securityElement2 = new SecurityElement("add");
						string text = dbconnectionString.ConnectionString;
						text = this.EncodeXmlValue(text);
						if (!ADP.IsEmpty(text))
						{
							securityElement2.AddAttribute("ConnectionString", text);
						}
						text = dbconnectionString.Restrictions;
						text = this.EncodeXmlValue(text);
						if (text == null)
						{
							text = ADP.StrEmpty;
						}
						securityElement2.AddAttribute("KeyRestrictions", text);
						text = dbconnectionString.Behavior.ToString();
						securityElement2.AddAttribute("KeyRestrictionBehavior", text);
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x04001C5F RID: 7263
		private bool _isUnrestricted;

		// Token: 0x04001C60 RID: 7264
		private bool _allowBlankPassword;

		// Token: 0x04001C61 RID: 7265
		private NameValuePermission _keyvaluetree;

		// Token: 0x04001C62 RID: 7266
		private ArrayList _keyvalues;

		// Token: 0x020003D0 RID: 976
		private static class XmlStr
		{
			// Token: 0x04001C63 RID: 7267
			internal const string _class = "class";

			// Token: 0x04001C64 RID: 7268
			internal const string _IPermission = "IPermission";

			// Token: 0x04001C65 RID: 7269
			internal const string _Permission = "Permission";

			// Token: 0x04001C66 RID: 7270
			internal const string _Unrestricted = "Unrestricted";

			// Token: 0x04001C67 RID: 7271
			internal const string _AllowBlankPassword = "AllowBlankPassword";

			// Token: 0x04001C68 RID: 7272
			internal const string _true = "true";

			// Token: 0x04001C69 RID: 7273
			internal const string _Version = "version";

			// Token: 0x04001C6A RID: 7274
			internal const string _VersionNumber = "1";

			// Token: 0x04001C6B RID: 7275
			internal const string _add = "add";

			// Token: 0x04001C6C RID: 7276
			internal const string _ConnectionString = "ConnectionString";

			// Token: 0x04001C6D RID: 7277
			internal const string _KeyRestrictions = "KeyRestrictions";

			// Token: 0x04001C6E RID: 7278
			internal const string _KeyRestrictionBehavior = "KeyRestrictionBehavior";
		}
	}
}
