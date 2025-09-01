using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000801 RID: 2049
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RuntimeCompatibilityAttribute" /> class.</summary>
		// Token: 0x06004614 RID: 17940 RVA: 0x00002050 File Offset: 0x00000250
		public RuntimeCompatibilityAttribute()
		{
		}

		/// <summary>Gets or sets a value that indicates whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if exceptions that do not derive from the <see cref="T:System.Exception" /> class should appear wrapped with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x000E5845 File Offset: 0x000E3A45
		// (set) Token: 0x06004616 RID: 17942 RVA: 0x000E584D File Offset: 0x000E3A4D
		public bool WrapNonExceptionThrows
		{
			[CompilerGenerated]
			get
			{
				return this.<WrapNonExceptionThrows>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WrapNonExceptionThrows>k__BackingField = value;
			}
		}

		// Token: 0x04002D3D RID: 11581
		[CompilerGenerated]
		private bool <WrapNonExceptionThrows>k__BackingField;
	}
}
