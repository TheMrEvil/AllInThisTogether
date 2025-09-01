using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that an importing compiler must fully understand the semantics of a type definition, or refuse to use it.  This class cannot be inherited.</summary>
	// Token: 0x02000847 RID: 2119
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> class.</summary>
		/// <param name="requiredContract">A type that an importing compiler must fully understand.  
		///  This parameter is not supported in the .NET Framework version 2.0 and later.</param>
		// Token: 0x060046C5 RID: 18117 RVA: 0x000E719D File Offset: 0x000E539D
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		/// <summary>Gets a type that an importing compiler must fully understand.</summary>
		/// <returns>A type that an importing compiler must fully understand.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x000E71AC File Offset: 0x000E53AC
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04002D8F RID: 11663
		private Type requiredContract;
	}
}
