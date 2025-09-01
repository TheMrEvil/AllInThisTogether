using System;

namespace System.Data
{
	// Token: 0x0200009B RID: 155
	internal sealed class Function
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A77E File Offset: 0x0002897E
		internal Function()
		{
			this._name = null;
			this._id = FunctionId.none;
			this._result = null;
			this._isValidateArguments = false;
			this._argumentCount = 0;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002A7B8 File Offset: 0x000289B8
		internal Function(string name, FunctionId id, Type result, bool IsValidateArguments, bool IsVariantArgumentList, int argumentCount, Type a1, Type a2, Type a3)
		{
			this._name = name;
			this._id = id;
			this._result = result;
			this._isValidateArguments = IsValidateArguments;
			this._isVariantArgumentList = IsVariantArgumentList;
			this._argumentCount = argumentCount;
			if (a1 != null)
			{
				this._parameters[0] = a1;
			}
			if (a2 != null)
			{
				this._parameters[1] = a2;
			}
			if (a3 != null)
			{
				this._parameters[2] = a3;
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002A840 File Offset: 0x00028A40
		// Note: this type is marked as 'beforefieldinit'.
		static Function()
		{
		}

		// Token: 0x0400073E RID: 1854
		internal readonly string _name;

		// Token: 0x0400073F RID: 1855
		internal readonly FunctionId _id;

		// Token: 0x04000740 RID: 1856
		internal readonly Type _result;

		// Token: 0x04000741 RID: 1857
		internal readonly bool _isValidateArguments;

		// Token: 0x04000742 RID: 1858
		internal readonly bool _isVariantArgumentList;

		// Token: 0x04000743 RID: 1859
		internal readonly int _argumentCount;

		// Token: 0x04000744 RID: 1860
		internal readonly Type[] _parameters = new Type[3];

		// Token: 0x04000745 RID: 1861
		internal static string[] s_functionName = new string[]
		{
			"Unknown",
			"Ascii",
			"Char",
			"CharIndex",
			"Difference",
			"Len",
			"Lower",
			"LTrim",
			"Patindex",
			"Replicate",
			"Reverse",
			"Right",
			"RTrim",
			"Soundex",
			"Space",
			"Str",
			"Stuff",
			"Substring",
			"Upper",
			"IsNull",
			"Iif",
			"Convert",
			"cInt",
			"cBool",
			"cDate",
			"cDbl",
			"cStr",
			"Abs",
			"Acos",
			"In",
			"Trim",
			"Sum",
			"Avg",
			"Min",
			"Max",
			"Count",
			"StDev",
			"Var",
			"DateTimeOffset"
		};
	}
}
