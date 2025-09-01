using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates when a dependency is to be loaded by the referring assembly. This class cannot be inherited.</summary>
	// Token: 0x0200082B RID: 2091
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value.</summary>
		/// <param name="dependentAssemblyArgument">The dependent assembly to bind to.</param>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</param>
		// Token: 0x060046AA RID: 18090 RVA: 0x000E70DD File Offset: 0x000E52DD
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		/// <summary>Gets the value of the dependent assembly.</summary>
		/// <returns>The name of the dependent assembly.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060046AB RID: 18091 RVA: 0x000E70F3 File Offset: 0x000E52F3
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly is to load a dependency.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x000E70FB File Offset: 0x000E52FB
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002D77 RID: 11639
		private string dependentAssembly;

		// Token: 0x04002D78 RID: 11640
		private LoadHint loadHint;
	}
}
