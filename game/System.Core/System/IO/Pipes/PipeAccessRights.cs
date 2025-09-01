using System;

namespace System.IO.Pipes
{
	/// <summary>Defines the access rights to use when you create access and audit rules.</summary>
	// Token: 0x02000347 RID: 839
	[Flags]
	public enum PipeAccessRights
	{
		/// <summary>Specifies the right to read data from the pipe. This does not include the right to read file system attributes, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x04000C28 RID: 3112
		ReadData = 1,
		/// <summary>Specifies the right to write data to a pipe. This does not include the right to write file system attributes or extended file system attributes.</summary>
		// Token: 0x04000C29 RID: 3113
		WriteData = 2,
		/// <summary>Specifies the right to read file system attributes from a pipe. This does not include the right to read data, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x04000C2A RID: 3114
		ReadAttributes = 128,
		/// <summary>Specifies the right to write file system attributes to a pipe. This does not include the right to write data or extended file system attributes.</summary>
		// Token: 0x04000C2B RID: 3115
		WriteAttributes = 256,
		/// <summary>Specifies the right to read extended file system attributes from a pipe. This does not include the right to read data, file system attributes, or access and audit rules.</summary>
		// Token: 0x04000C2C RID: 3116
		ReadExtendedAttributes = 8,
		/// <summary>Specifies the right to write extended file system attributes to a pipe. This does not include the right to write file attributes or data.</summary>
		// Token: 0x04000C2D RID: 3117
		WriteExtendedAttributes = 16,
		/// <summary>Specifies the right to create a new pipe. Setting this right also sets the <see cref="F:System.IO.Pipes.PipeAccessRights.Synchronize" /> right.</summary>
		// Token: 0x04000C2E RID: 3118
		CreateNewInstance = 4,
		/// <summary>Specifies the right to delete a pipe.</summary>
		// Token: 0x04000C2F RID: 3119
		Delete = 65536,
		/// <summary>Specifies the right to read access and audit rules from the pipe. This does not include the right to read data, file system attributes, or extended file system attributes.</summary>
		// Token: 0x04000C30 RID: 3120
		ReadPermissions = 131072,
		/// <summary>Specifies the right to change the security and audit rules that are associated with a pipe.</summary>
		// Token: 0x04000C31 RID: 3121
		ChangePermissions = 262144,
		/// <summary>Specifies the right to change the owner of a pipe. Note that owners of a pipe have full access to that resource.</summary>
		// Token: 0x04000C32 RID: 3122
		TakeOwnership = 524288,
		/// <summary>Specifies whether the application can wait for a pipe handle to synchronize with the completion of an I/O operation.</summary>
		// Token: 0x04000C33 RID: 3123
		Synchronize = 1048576,
		/// <summary>Specifies the right to exert full control over a pipe, and to modify access control and audit rules. This value represents the combination of all rights in this enumeration.</summary>
		// Token: 0x04000C34 RID: 3124
		FullControl = 2032031,
		/// <summary>Specifies the right to read from the pipe. This right includes the <see cref="F:System.IO.Pipes.PipeAccessRights.ReadAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadData" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadExtendedAttributes" />, and <see cref="F:System.IO.Pipes.PipeAccessRights.ReadPermissions" /> rights.</summary>
		// Token: 0x04000C35 RID: 3125
		Read = 131209,
		/// <summary>Specifies the right to write to the pipe. This right includes the <see cref="F:System.IO.Pipes.PipeAccessRights.WriteAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.WriteData" />, and <see cref="F:System.IO.Pipes.PipeAccessRights.WriteExtendedAttributes" /> rights.</summary>
		// Token: 0x04000C36 RID: 3126
		Write = 274,
		/// <summary>Specifies the right to read and write from the pipe. This right includes the <see cref="F:System.IO.Pipes.PipeAccessRights.ReadAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadData" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadExtendedAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadPermissions" />, <see cref="F:System.IO.Pipes.PipeAccessRights.WriteAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.WriteData" />, and <see cref="F:System.IO.Pipes.PipeAccessRights.WriteExtendedAttributes" /> rights.</summary>
		// Token: 0x04000C37 RID: 3127
		ReadWrite = 131483,
		/// <summary>Specifies the right to make changes to the system access control list (SACL).</summary>
		// Token: 0x04000C38 RID: 3128
		AccessSystemSecurity = 16777216
	}
}
