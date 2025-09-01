using System;

namespace System.Globalization
{
	// Token: 0x0200099E RID: 2462
	internal interface ISimpleCollator
	{
		// Token: 0x060058A3 RID: 22691
		SortKey GetSortKey(string source, CompareOptions options);

		// Token: 0x060058A4 RID: 22692
		int Compare(string s1, string s2);

		// Token: 0x060058A5 RID: 22693
		int Compare(string s1, int idx1, int len1, string s2, int idx2, int len2, CompareOptions options);

		// Token: 0x060058A6 RID: 22694
		bool IsPrefix(string src, string target, CompareOptions opt);

		// Token: 0x060058A7 RID: 22695
		bool IsSuffix(string src, string target, CompareOptions opt);

		// Token: 0x060058A8 RID: 22696
		int IndexOf(string s, string target, int start, int length, CompareOptions opt);

		// Token: 0x060058A9 RID: 22697
		int IndexOf(string s, char target, int start, int length, CompareOptions opt);

		// Token: 0x060058AA RID: 22698
		int LastIndexOf(string s, string target, CompareOptions opt);

		// Token: 0x060058AB RID: 22699
		int LastIndexOf(string s, string target, int start, int length, CompareOptions opt);

		// Token: 0x060058AC RID: 22700
		int LastIndexOf(string s, char target, CompareOptions opt);

		// Token: 0x060058AD RID: 22701
		int LastIndexOf(string s, char target, int start, int length, CompareOptions opt);
	}
}
