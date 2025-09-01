using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000925 RID: 2341
	[StructLayout(LayoutKind.Sequential)]
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x0600502E RID: 20526 RVA: 0x000FAE2C File Offset: 0x000F902C
		public FieldOnTypeBuilderInst(TypeBuilderInstantiation instantiation, FieldInfo fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x000FAE42 File Offset: 0x000F9042
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x000FAE4A File Offset: 0x000F904A
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x000FAE42 File Offset: 0x000F9042
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x000FAE57 File Offset: 0x000F9057
		public override string ToString()
		{
			return this.fb.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x000FAE79 File Offset: 0x000F9079
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x000472C8 File Offset: 0x000454C8
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06005038 RID: 20536 RVA: 0x00084B75 File Offset: 0x00082D75
		public override int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x000472C8 File Offset: 0x000454C8
		public override Type FieldType
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x000472C8 File Offset: 0x000454C8
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x000FAE86 File Offset: 0x000F9086
		internal FieldInfo RuntimeResolve()
		{
			return this.instantiation.RuntimeResolve().GetField(this.fb);
		}

		// Token: 0x04003169 RID: 12649
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x0400316A RID: 12650
		internal FieldInfo fb;
	}
}
