using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies access flags for the security permission object.</summary>
	// Token: 0x02000459 RID: 1113
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SecurityPermissionFlag
	{
		/// <summary>No security access.</summary>
		// Token: 0x04002099 RID: 8345
		NoFlags = 0,
		/// <summary>Ability to assert that all this code's callers have the requisite permission for the operation.</summary>
		// Token: 0x0400209A RID: 8346
		Assertion = 1,
		/// <summary>Ability to call unmanaged code.</summary>
		// Token: 0x0400209B RID: 8347
		UnmanagedCode = 2,
		/// <summary>Ability to skip verification of code in this assembly. Code that is unverifiable can be run if this permission is granted.</summary>
		// Token: 0x0400209C RID: 8348
		SkipVerification = 4,
		/// <summary>Permission for the code to run. Without this permission, managed code will not be executed.</summary>
		// Token: 0x0400209D RID: 8349
		Execution = 8,
		/// <summary>Ability to use certain advanced operations on threads.</summary>
		// Token: 0x0400209E RID: 8350
		ControlThread = 16,
		/// <summary>Ability to provide evidence, including the ability to alter the evidence provided by the common language runtime.</summary>
		// Token: 0x0400209F RID: 8351
		ControlEvidence = 32,
		/// <summary>Ability to view and modify policy.</summary>
		// Token: 0x040020A0 RID: 8352
		ControlPolicy = 64,
		/// <summary>Ability to provide serialization services. Used by serialization formatters.</summary>
		// Token: 0x040020A1 RID: 8353
		SerializationFormatter = 128,
		/// <summary>Ability to specify domain policy.</summary>
		// Token: 0x040020A2 RID: 8354
		ControlDomainPolicy = 256,
		/// <summary>Ability to manipulate the principal object.</summary>
		// Token: 0x040020A3 RID: 8355
		ControlPrincipal = 512,
		/// <summary>Ability to create and manipulate an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x040020A4 RID: 8356
		ControlAppDomain = 1024,
		/// <summary>Permission to configure Remoting types and channels.</summary>
		// Token: 0x040020A5 RID: 8357
		RemotingConfiguration = 2048,
		/// <summary>Permission to plug code into the common language runtime infrastructure, such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.</summary>
		// Token: 0x040020A6 RID: 8358
		Infrastructure = 4096,
		/// <summary>Permission to perform explicit binding redirection in the application configuration file. This includes redirection of .NET Framework assemblies that have been unified as well as other assemblies found outside the .NET Framework.</summary>
		// Token: 0x040020A7 RID: 8359
		BindingRedirects = 8192,
		/// <summary>The unrestricted state of the permission.</summary>
		// Token: 0x040020A8 RID: 8360
		AllFlags = 16383
	}
}
