using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008E7 RID: 2279
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoArrayMethod : MethodInfo
	{
		// Token: 0x06004BD1 RID: 19409 RVA: 0x000F189F File Offset: 0x000EFA9F
		internal MonoArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.name = methodName;
			this.parent = arrayClass;
			this.ret = returnType;
			this.parameters = (Type[])parameterTypes.Clone();
			this.call_conv = callingConvention;
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0000270D File Offset: 0x0000090D
		[MonoTODO("Always returns this")]
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x000F18D6 File Offset: 0x000EFAD6
		public override Type ReturnType
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x0000AF5E File Offset: 0x0000915E
		[MonoTODO("Not implemented.  Always returns null")]
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x000F18DE File Offset: 0x000EFADE
		[MonoTODO("Not implemented.  Always returns an empty array")]
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x000F18E6 File Offset: 0x000EFAE6
		internal override ParameterInfo[] GetParametersInternal()
		{
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("Not implemented.  Always returns 0")]
		internal override int GetParametersCount()
		{
			return 0;
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("Not implemented")]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x000F18ED File Offset: 0x000EFAED
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodAttributes Attributes
		{
			get
			{
				return MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x000F18F5 File Offset: 0x000EFAF5
		public override Type ReflectedType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x000F18F5 File Offset: 0x000EFAF5
		public override Type DeclaringType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x000F18FD File Offset: 0x000EFAFD
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x000F1905 File Offset: 0x000EFB05
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x000F190E File Offset: 0x000EFB0E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x000F1918 File Offset: 0x000EFB18
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] array = this.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i].ParameterType.Name;
			}
			if (this.ReturnType != null)
			{
				return string.Concat(new string[]
				{
					this.ReturnType.Name,
					" ",
					this.Name,
					"(",
					text,
					")"
				});
			}
			return string.Concat(new string[]
			{
				"void ",
				this.Name,
				"(",
				text,
				")"
			});
		}

		// Token: 0x04003006 RID: 12294
		internal RuntimeMethodHandle mhandle;

		// Token: 0x04003007 RID: 12295
		internal Type parent;

		// Token: 0x04003008 RID: 12296
		internal Type ret;

		// Token: 0x04003009 RID: 12297
		internal Type[] parameters;

		// Token: 0x0400300A RID: 12298
		internal string name;

		// Token: 0x0400300B RID: 12299
		internal int table_idx;

		// Token: 0x0400300C RID: 12300
		internal CallingConventions call_conv;
	}
}
