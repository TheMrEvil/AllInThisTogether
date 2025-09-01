using System;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Determines the permission flags that apply to a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x0200087C RID: 2172
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class TypeDescriptorPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060044C3 RID: 17603 RVA: 0x00003917 File Offset: 0x00001B17
		public TypeDescriptorPermissionAttribute(SecurityAction action)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x000ED20C File Offset: 0x000EB40C
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x00003917 File Offset: 0x00001B17
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return TypeDescriptorPermissionFlags.NoFlags;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the type descriptor can be accessed from partial trust.</summary>
		/// <returns>
		///   <see langword="true" /> if the type descriptor can be accessed from partial trust; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x000ED228 File Offset: 0x000EB428
		// (set) Token: 0x060044C7 RID: 17607 RVA: 0x00003917 File Offset: 0x00001B17
		public bool RestrictedRegistrationAccess
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x060044C8 RID: 17608 RVA: 0x00032884 File Offset: 0x00030A84
		public override IPermission CreatePermission()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
