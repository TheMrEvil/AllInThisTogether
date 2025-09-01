using System;

namespace System.IO.Pipes
{
	/// <summary>Specifies the direction of the pipe.</summary>
	// Token: 0x0200034C RID: 844
	public enum PipeDirection
	{
		/// <summary>Specifies that the pipe direction is in.</summary>
		// Token: 0x04000C48 RID: 3144
		In = 1,
		/// <summary>Specifies that the pipe direction is out.</summary>
		// Token: 0x04000C49 RID: 3145
		Out,
		/// <summary>Specifies that the pipe direction is two-way.</summary>
		// Token: 0x04000C4A RID: 3146
		InOut
	}
}
