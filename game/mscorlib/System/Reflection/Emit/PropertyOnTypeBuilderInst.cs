using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000941 RID: 2369
	[StructLayout(LayoutKind.Sequential)]
	internal class PropertyOnTypeBuilderInst : PropertyInfo
	{
		// Token: 0x06005238 RID: 21048 RVA: 0x0010260E File Offset: 0x0010080E
		internal PropertyOnTypeBuilderInst(TypeBuilderInstantiation instantiation, PropertyInfo prop)
		{
			this.instantiation = instantiation;
			this.prop = prop;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x000472C8 File Offset: 0x000454C8
		public override PropertyAttributes Attributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool CanRead
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool CanWrite
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x00102624 File Offset: 0x00100824
		public override Type PropertyType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.PropertyType);
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600523D RID: 21053 RVA: 0x0010263C File Offset: 0x0010083C
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.DeclaringType);
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600523E RID: 21054 RVA: 0x00102654 File Offset: 0x00100854
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x0010265C File Offset: 0x0010085C
		public override string Name
		{
			get
			{
				return this.prop.Name;
			}
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0010266C File Offset: 0x0010086C
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			MethodInfo getMethod = this.GetGetMethod(nonPublic);
			MethodInfo setMethod = this.GetSetMethod(nonPublic);
			int num = 0;
			if (getMethod != null)
			{
				num++;
			}
			if (setMethod != null)
			{
				num++;
			}
			MethodInfo[] array = new MethodInfo[num];
			num = 0;
			if (getMethod != null)
			{
				array[num++] = getMethod;
			}
			if (setMethod != null)
			{
				array[num] = setMethod;
			}
			return array;
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x001026D0 File Offset: 0x001008D0
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetGetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00102720 File Offset: 0x00100920
		public override ParameterInfo[] GetIndexParameters()
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetParameters();
			}
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0010274C File Offset: 0x0010094C
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetSetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0010279A File Offset: 0x0010099A
		public override string ToString()
		{
			return string.Format("{0} {1}", this.PropertyType, this.Name);
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x000472C8 File Offset: 0x000454C8
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003307 RID: 13063
		private TypeBuilderInstantiation instantiation;

		// Token: 0x04003308 RID: 13064
		private PropertyInfo prop;
	}
}
