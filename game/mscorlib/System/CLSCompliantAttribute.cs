using System;

namespace System
{
	/// <summary>Indicates whether a program element is compliant with the Common Language Specification (CLS). This class cannot be inherited.</summary>
	// Token: 0x02000104 RID: 260
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.CLSCompliantAttribute" /> class with a Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <param name="isCompliant">
		///   <see langword="true" /> if CLS-compliant; otherwise, <see langword="false" />.</param>
		// Token: 0x060007DB RID: 2011 RVA: 0x0002253D File Offset: 0x0002073D
		public CLSCompliantAttribute(bool isCompliant)
		{
			this._compliant = isCompliant;
		}

		/// <summary>Gets the Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <returns>
		///   <see langword="true" /> if the program element is CLS-compliant; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002254C File Offset: 0x0002074C
		public bool IsCompliant
		{
			get
			{
				return this._compliant;
			}
		}

		// Token: 0x04001077 RID: 4215
		private bool _compliant;
	}
}
