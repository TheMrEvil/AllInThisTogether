using System;

namespace System.Collections
{
	/// <summary>Provides objects for performing a structural comparison of two collection objects.</summary>
	// Token: 0x02000A38 RID: 2616
	public static class StructuralComparisons
	{
		/// <summary>Gets a predefined object that performs a structural comparison of two objects.</summary>
		/// <returns>A predefined object that is used to perform a structural comparison of two collection objects.</returns>
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x00138D10 File Offset: 0x00136F10
		public static IComparer StructuralComparer
		{
			get
			{
				IComparer comparer = StructuralComparisons.s_StructuralComparer;
				if (comparer == null)
				{
					comparer = new StructuralComparer();
					StructuralComparisons.s_StructuralComparer = comparer;
				}
				return comparer;
			}
		}

		/// <summary>Gets a predefined object that compares two objects for structural equality.</summary>
		/// <returns>A predefined object that is used to compare two collection objects for structural equality.</returns>
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06005CFB RID: 23803 RVA: 0x00138D38 File Offset: 0x00136F38
		public static IEqualityComparer StructuralEqualityComparer
		{
			get
			{
				IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
				if (equalityComparer == null)
				{
					equalityComparer = new StructuralEqualityComparer();
					StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x040038CE RID: 14542
		private static volatile IComparer s_StructuralComparer;

		// Token: 0x040038CF RID: 14543
		private static volatile IEqualityComparer s_StructuralEqualityComparer;
	}
}
