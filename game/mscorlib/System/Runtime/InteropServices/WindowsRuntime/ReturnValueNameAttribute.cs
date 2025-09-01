using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the name of the return value of a method in a Windows Runtime component.</summary>
	// Token: 0x02000787 RID: 1927
	[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public sealed class ReturnValueNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute" /> class, and specifies the name of the return value.</summary>
		/// <param name="name">The name of the return value.</param>
		// Token: 0x06004486 RID: 17542 RVA: 0x000E3B22 File Offset: 0x000E1D22
		public ReturnValueNameAttribute(string name)
		{
			this.m_Name = name;
		}

		/// <summary>Gets the name that was specified for the return value of a method in a Windows Runtime component.</summary>
		/// <returns>The name of the method's return value.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x000E3B31 File Offset: 0x000E1D31
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x04002C23 RID: 11299
		private string m_Name;
	}
}
