using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support creating error configuration records.</summary>
	// Token: 0x0200007B RID: 123
	public interface IConfigErrorInfo
	{
		/// <summary>Gets a string specifying the file name related to the configuration details.</summary>
		/// <returns>A string specifying a filename.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000427 RID: 1063
		string Filename { get; }

		/// <summary>Gets an integer specifying the line number related to the configuration details.</summary>
		/// <returns>An integer specifying a line number.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000428 RID: 1064
		int LineNumber { get; }
	}
}
