using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Specifies that a string contains a name of an event log or the file system path to an event log file.</summary>
	// Token: 0x0200039D RID: 925
	public enum PathType
	{
		/// <summary>A path parameter contains the file system path to an event log file.</summary>
		// Token: 0x04000D48 RID: 3400
		FilePath = 2,
		/// <summary>A path parameter contains the name of the event log.</summary>
		// Token: 0x04000D49 RID: 3401
		LogName = 1
	}
}
