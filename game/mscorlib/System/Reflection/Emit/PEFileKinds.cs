using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies the type of the portable executable (PE) file.</summary>
	// Token: 0x0200093D RID: 2365
	[ComVisible(true)]
	[Serializable]
	public enum PEFileKinds
	{
		/// <summary>The portable executable (PE) file is a DLL.</summary>
		// Token: 0x040032EB RID: 13035
		Dll = 1,
		/// <summary>The application is a console (not a Windows-based) application.</summary>
		// Token: 0x040032EC RID: 13036
		ConsoleApplication,
		/// <summary>The application is a Windows-based application.</summary>
		// Token: 0x040032ED RID: 13037
		WindowApplication
	}
}
