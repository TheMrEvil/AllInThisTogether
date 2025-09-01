using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the use of a value tuple on a member is meant to be treated as a tuple with element names.</summary>
	// Token: 0x02000808 RID: 2056
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	[CLSCompliant(false)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TupleElementNamesAttribute" /> class.</summary>
		/// <param name="transformNames">A string array that specifies, in a pre-order depth-first traversal of a type's construction, which value tuple occurrences are meant to carry element names.</param>
		// Token: 0x06004624 RID: 17956 RVA: 0x000E5932 File Offset: 0x000E3B32
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		/// <summary>Specifies, in a pre-order depth-first traversal of a type's construction, which value tuple elements are meant to carry element names.</summary>
		/// <returns>An array that indicates which value tuple elements are meant to carry element names.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004625 RID: 17957 RVA: 0x000E594F File Offset: 0x000E3B4F
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x04002D42 RID: 11586
		private readonly string[] _transformNames;
	}
}
