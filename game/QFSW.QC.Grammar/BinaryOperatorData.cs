using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000004 RID: 4
	internal class BinaryOperatorData : IBinaryOperator
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021B8 File Offset: 0x000003B8
		public Type LArg
		{
			[CompilerGenerated]
			get
			{
				return this.<LArg>k__BackingField;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021C0 File Offset: 0x000003C0
		public Type RArg
		{
			[CompilerGenerated]
			get
			{
				return this.<RArg>k__BackingField;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021C8 File Offset: 0x000003C8
		public Type Ret
		{
			[CompilerGenerated]
			get
			{
				return this.<Ret>k__BackingField;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D0 File Offset: 0x000003D0
		public BinaryOperatorData(MethodInfo OperatorMethod)
		{
			this._method = OperatorMethod;
			this.Ret = OperatorMethod.ReturnType;
			ParameterInfo[] parameters = this._method.GetParameters();
			if (parameters.Length != 2)
			{
				throw new ArgumentException(string.Format("Cannot create a binary operator from a method with {0} parameters", parameters.Length));
			}
			this.LArg = parameters[0].ParameterType;
			this.RArg = parameters[1].ParameterType;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223C File Offset: 0x0000043C
		public object Invoke(object left, object right)
		{
			return this._method.Invoke(null, new object[]
			{
				left,
				right
			});
		}

		// Token: 0x04000003 RID: 3
		[CompilerGenerated]
		private readonly Type <LArg>k__BackingField;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		private readonly Type <RArg>k__BackingField;

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		private readonly Type <Ret>k__BackingField;

		// Token: 0x04000006 RID: 6
		private readonly MethodInfo _method;
	}
}
