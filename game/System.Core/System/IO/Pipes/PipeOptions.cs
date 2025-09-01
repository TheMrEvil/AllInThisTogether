using System;

namespace System.IO.Pipes
{
	/// <summary>Provides options for creating a <see cref="T:System.IO.Pipes.PipeStream" /> object. This enumeration has a <see cref="T:System.FlagsAttribute" /> attribute that allows a bitwise combination of its member values.</summary>
	// Token: 0x0200034D RID: 845
	[Flags]
	public enum PipeOptions
	{
		/// <summary>Indicates that there are no additional parameters.</summary>
		// Token: 0x04000C4C RID: 3148
		None = 0,
		/// <summary>Indicates that the system should write through any intermediate cache and go directly to the pipe.</summary>
		// Token: 0x04000C4D RID: 3149
		WriteThrough = -2147483648,
		/// <summary>Indicates that the pipe can be used for asynchronous reading and writing.</summary>
		// Token: 0x04000C4E RID: 3150
		Asynchronous = 1073741824,
		// Token: 0x04000C4F RID: 3151
		CurrentUserOnly = 536870912
	}
}
