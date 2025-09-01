using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the set of execution contexts in which a class object will be made available for requests to construct instances.</summary>
	// Token: 0x0200074D RID: 1869
	[Flags]
	public enum RegistrationClassContext
	{
		/// <summary>Disables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04002BDF RID: 11231
		DisableActivateAsActivator = 32768,
		/// <summary>Enables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04002BE0 RID: 11232
		EnableActivateAsActivator = 65536,
		/// <summary>Allows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04002BE1 RID: 11233
		EnableCodeDownload = 8192,
		/// <summary>Begin this activation from the default context of the current apartment.</summary>
		// Token: 0x04002BE2 RID: 11234
		FromDefaultContext = 131072,
		/// <summary>The code that manages objects of this class is an in-process handler.</summary>
		// Token: 0x04002BE3 RID: 11235
		InProcessHandler = 2,
		/// <summary>Not used.</summary>
		// Token: 0x04002BE4 RID: 11236
		InProcessHandler16 = 32,
		/// <summary>The code that creates and manages objects of this class is a DLL that runs in the same process as the caller of the function specifying the class context.</summary>
		// Token: 0x04002BE5 RID: 11237
		InProcessServer = 1,
		/// <summary>Not used.</summary>
		// Token: 0x04002BE6 RID: 11238
		InProcessServer16 = 8,
		/// <summary>The EXE code that creates and manages objects of this class runs on same machine but is loaded in a separate process space.</summary>
		// Token: 0x04002BE7 RID: 11239
		LocalServer = 4,
		/// <summary>Disallows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04002BE8 RID: 11240
		NoCodeDownload = 1024,
		/// <summary>Specifies whether activation fails if it uses custom marshaling.</summary>
		// Token: 0x04002BE9 RID: 11241
		NoCustomMarshal = 4096,
		/// <summary>Overrides the logging of failures.</summary>
		// Token: 0x04002BEA RID: 11242
		NoFailureLog = 16384,
		/// <summary>A remote machine context.</summary>
		// Token: 0x04002BEB RID: 11243
		RemoteServer = 16,
		/// <summary>Not used.</summary>
		// Token: 0x04002BEC RID: 11244
		Reserved1 = 64,
		/// <summary>Not used.</summary>
		// Token: 0x04002BED RID: 11245
		Reserved2 = 128,
		/// <summary>Not used.</summary>
		// Token: 0x04002BEE RID: 11246
		Reserved3 = 256,
		/// <summary>Not used.</summary>
		// Token: 0x04002BEF RID: 11247
		Reserved4 = 512,
		/// <summary>Not used.</summary>
		// Token: 0x04002BF0 RID: 11248
		Reserved5 = 2048
	}
}
