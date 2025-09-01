using System;
using System.Globalization;
using System.Reflection;

namespace QFSW.QC.Internal
{
	// Token: 0x02000067 RID: 103
	internal abstract class FieldMethod : MethodInfo
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000A137 File Offset: 0x00008337
		public FieldMethod(FieldInfo fieldInfo)
		{
			this._fieldInfo = fieldInfo;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A148 File Offset: 0x00008348
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] array = new object[parameters.Length + 1];
			if (base.IsStatic)
			{
				array[0] = this._fieldInfo;
			}
			else
			{
				array[0] = obj;
			}
			Array.Copy(parameters, 0, array, 1, parameters.Length);
			return this._internalDelegate.DynamicInvoke(array);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A192 File Offset: 0x00008392
		public override ParameterInfo[] GetParameters()
		{
			return this._parameters;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A19A File Offset: 0x0000839A
		public override string Name
		{
			get
			{
				return this._fieldInfo.Name;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A1A7 File Offset: 0x000083A7
		public override Type DeclaringType
		{
			get
			{
				return this._fieldInfo.DeclaringType;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public override Type ReflectedType
		{
			get
			{
				return this._fieldInfo.ReflectedType;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A1C1 File Offset: 0x000083C1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._fieldInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A1CF File Offset: 0x000083CF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._fieldInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A1DE File Offset: 0x000083DE
		public override MethodAttributes Attributes
		{
			get
			{
				return this._internalDelegate.Method.Attributes;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this._internalDelegate.Method.MethodHandle;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A202 File Offset: 0x00008402
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this._internalDelegate.Method.ReturnTypeCustomAttributes;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A214 File Offset: 0x00008414
		public override MethodInfo GetBaseDefinition()
		{
			return this._internalDelegate.Method.GetBaseDefinition();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A226 File Offset: 0x00008426
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this._internalDelegate.Method.GetMethodImplementationFlags();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A238 File Offset: 0x00008438
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._internalDelegate.Method.IsDefined(attributeType, inherit);
		}

		// Token: 0x04000146 RID: 326
		protected readonly FieldInfo _fieldInfo;

		// Token: 0x04000147 RID: 327
		protected Delegate _internalDelegate;

		// Token: 0x04000148 RID: 328
		protected ParameterInfo[] _parameters;
	}
}
