using System;

namespace System.IO
{
	/// <summary>Defines constants for drive types, including CDRom, Fixed, Network, NoRootDirectory, Ram, Removable, and Unknown.</summary>
	// Token: 0x02000B2D RID: 2861
	public enum DriveType
	{
		/// <summary>The type of drive is unknown.</summary>
		// Token: 0x04003C1F RID: 15391
		Unknown,
		/// <summary>The drive does not have a root directory.</summary>
		// Token: 0x04003C20 RID: 15392
		NoRootDirectory,
		/// <summary>The drive is a removable storage device, such as a floppy disk drive or a USB flash drive.</summary>
		// Token: 0x04003C21 RID: 15393
		Removable,
		/// <summary>The drive is a fixed disk.</summary>
		// Token: 0x04003C22 RID: 15394
		Fixed,
		/// <summary>The drive is a network drive.</summary>
		// Token: 0x04003C23 RID: 15395
		Network,
		/// <summary>The drive is an optical disc device, such as a CD or DVD-ROM.</summary>
		// Token: 0x04003C24 RID: 15396
		CDRom,
		/// <summary>The drive is a RAM disk.</summary>
		// Token: 0x04003C25 RID: 15397
		Ram
	}
}
