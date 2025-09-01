using System;
using System.Globalization;

namespace IKVM.Reflection
{
	// Token: 0x02000008 RID: 8
	public abstract class Binder
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Binder()
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual object ChangeType(object value, Type type, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual void ReorderArgumentArray(ref object[] args, object state)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000070 RID: 112
		public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06000071 RID: 113
		public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);
	}
}
