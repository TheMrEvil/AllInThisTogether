using System;

namespace System.Reflection
{
	/// <summary>Retrieves the mapping of an interface into the actual methods on a class that implements that interface.</summary>
	// Token: 0x020008A5 RID: 2213
	public struct InterfaceMapping
	{
		/// <summary>Represents the type that was used to create the interface mapping.</summary>
		// Token: 0x04002EB3 RID: 11955
		public Type TargetType;

		/// <summary>Shows the type that represents the interface.</summary>
		// Token: 0x04002EB4 RID: 11956
		public Type InterfaceType;

		/// <summary>Shows the methods that implement the interface.</summary>
		// Token: 0x04002EB5 RID: 11957
		public MethodInfo[] TargetMethods;

		/// <summary>Shows the methods that are defined on the interface.</summary>
		// Token: 0x04002EB6 RID: 11958
		public MethodInfo[] InterfaceMethods;
	}
}
