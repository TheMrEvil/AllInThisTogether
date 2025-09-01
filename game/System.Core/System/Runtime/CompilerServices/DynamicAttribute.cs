using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the use of <see cref="T:System.Object" /> on a member is meant to be treated as a dynamically dispatched type.</summary>
	// Token: 0x020002ED RID: 749
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class DynamicAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DynamicAttribute" /> class.</summary>
		// Token: 0x060016B5 RID: 5813 RVA: 0x0004C805 File Offset: 0x0004AA05
		public DynamicAttribute()
		{
			this._transformFlags = new bool[]
			{
				true
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DynamicAttribute" /> class.</summary>
		/// <param name="transformFlags">Specifies, in a prefix traversal of a type's construction, which <see cref="T:System.Object" /> occurrences are meant to be treated as a dynamically dispatched type.</param>
		// Token: 0x060016B6 RID: 5814 RVA: 0x0004C81D File Offset: 0x0004AA1D
		public DynamicAttribute(bool[] transformFlags)
		{
			if (transformFlags == null)
			{
				throw new ArgumentNullException("transformFlags");
			}
			this._transformFlags = transformFlags;
		}

		/// <summary>Specifies, in a prefix traversal of a type's construction, which <see cref="T:System.Object" /> occurrences are meant to be treated as a dynamically dispatched type.</summary>
		/// <returns>The list of <see cref="T:System.Object" /> occurrences that are meant to be treated as a dynamically dispatched type.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0004C83A File Offset: 0x0004AA3A
		public IList<bool> TransformFlags
		{
			get
			{
				return Array.AsReadOnly<bool>(this._transformFlags);
			}
		}

		// Token: 0x04000B64 RID: 2916
		private readonly bool[] _transformFlags;
	}
}
