using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	/// <summary>Allows declarative printing permission checks.</summary>
	// Token: 0x020000CA RID: 202
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	[Serializable]
	public sealed class PrintingPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06000ACF RID: 2767 RVA: 0x00018788 File Offset: 0x00016988
		public PrintingPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the type of printing allowed.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not one of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</exception>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00018791 File Offset: 0x00016991
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00018799 File Offset: 0x00016999
		public PrintingPermissionLevel Level
		{
			[CompilerGenerated]
			get
			{
				return this.<Level>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Level>k__BackingField = value;
			}
		}

		/// <summary>Creates the permission based on the requested access levels, which are set through the <see cref="P:System.Drawing.Printing.PrintingPermissionAttribute.Level" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x00018785 File Offset: 0x00016985
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x04000713 RID: 1811
		[CompilerGenerated]
		private PrintingPermissionLevel <Level>k__BackingField;
	}
}
