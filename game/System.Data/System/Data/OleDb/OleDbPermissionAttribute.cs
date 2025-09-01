using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	// Token: 0x0200015B RID: 347
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OleDbPermissionAttribute : DBDataPermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values representing an action that can be performed by using declarative security.</param>
		// Token: 0x0600129A RID: 4762 RVA: 0x0005ABA6 File Offset: 0x00058DA6
		public OleDbPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets a comma-delimited string that contains a list of supported providers.</summary>
		/// <returns>A comma-delimited list of providers allowed by the security policy.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0005ABB0 File Offset: 0x00058DB0
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x0005ABCE File Offset: 0x00058DCE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public string Provider
		{
			get
			{
				string providers = this._providers;
				if (providers == null)
				{
					return ADP.StrEmpty;
				}
				return providers;
			}
			set
			{
				this._providers = value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Data.OleDb.OleDbPermission" /> object that is configured according to the attribute properties.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbPermission" /> object.</returns>
		// Token: 0x0600129D RID: 4765 RVA: 0x0005ABD7 File Offset: 0x00058DD7
		public override IPermission CreatePermission()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000BAA RID: 2986
		private string _providers;
	}
}
