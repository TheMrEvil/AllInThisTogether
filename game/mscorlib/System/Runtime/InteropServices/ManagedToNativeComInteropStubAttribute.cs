using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides support for user customization of interop stubs in managed-to-COM interop scenarios.</summary>
	// Token: 0x02000713 RID: 1811
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class ManagedToNativeComInteropStubAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute" /> class with the specified class type and method name.</summary>
		/// <param name="classType">The class that contains the required stub method.</param>
		/// <param name="methodName">The name of the stub method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodName" /> cannot be found.  
		/// -or-  
		/// The method is not static or non-generic.  
		/// -or-  
		/// The method's parameter list does not match the expected parameter list for the stub.</exception>
		/// <exception cref="T:System.MethodAccessException">The interface that contains the managed interop method has no access to the stub method, because the stub method has private or protected accessibility, or because of a security issue.</exception>
		// Token: 0x060040C6 RID: 16582 RVA: 0x000E1636 File Offset: 0x000DF836
		public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}

		/// <summary>Gets the class that contains the required stub method.</summary>
		/// <returns>The class that contains the customized interop stub.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x000E164C File Offset: 0x000DF84C
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		/// <summary>Gets the name of the stub method.</summary>
		/// <returns>The name of a customized interop stub.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x000E1654 File Offset: 0x000DF854
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04002AF0 RID: 10992
		internal Type _classType;

		// Token: 0x04002AF1 RID: 10993
		internal string _methodName;
	}
}
