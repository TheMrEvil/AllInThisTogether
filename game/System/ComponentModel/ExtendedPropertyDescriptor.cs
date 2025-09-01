using System;
using System.Collections.Generic;

namespace System.ComponentModel
{
	// Token: 0x020003AA RID: 938
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06001EBB RID: 7867 RVA: 0x0006C940 File Offset: 0x0006AB40
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes) : base(extenderInfo, attributes)
		{
			List<Attribute> list = new List<Attribute>(this.AttributeArray);
			list.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				list.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[list.Count];
			list.CopyTo(array, 0);
			this.AttributeArray = array;
			this._extenderInfo = extenderInfo;
			this._provider = provider;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0006C9AC File Offset: 0x0006ABAC
		public ExtendedPropertyDescriptor(PropertyDescriptor extender, Attribute[] attributes) : base(extender, attributes)
		{
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = extender.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			ReflectPropertyDescriptor extenderInfo = extenderProvidedPropertyAttribute.ExtenderProperty as ReflectPropertyDescriptor;
			this._extenderInfo = extenderInfo;
			this._provider = extenderProvidedPropertyAttribute.Provider;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0006C9FB File Offset: 0x0006ABFB
		public override bool CanResetValue(object comp)
		{
			return this._extenderInfo.ExtenderCanResetValue(this._provider, comp);
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0006CA0F File Offset: 0x0006AC0F
		public override Type ComponentType
		{
			get
			{
				return this._extenderInfo.ComponentType;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0006CA1C File Offset: 0x0006AC1C
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0006CA3D File Offset: 0x0006AC3D
		public override Type PropertyType
		{
			get
			{
				return this._extenderInfo.ExtenderGetType(this._provider);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0006CA50 File Offset: 0x0006AC50
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this._provider);
					string text2 = (site != null) ? site.Name : null;
					if (text2 != null && text2.Length > 0)
					{
						text = string.Format("{0} on {1}", text, text2);
					}
				}
				return text;
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0006CABC File Offset: 0x0006ACBC
		public override object GetValue(object comp)
		{
			return this._extenderInfo.ExtenderGetValue(this._provider, comp);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0006CAD0 File Offset: 0x0006ACD0
		public override void ResetValue(object comp)
		{
			this._extenderInfo.ExtenderResetValue(this._provider, comp, this);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0006CAE5 File Offset: 0x0006ACE5
		public override void SetValue(object component, object value)
		{
			this._extenderInfo.ExtenderSetValue(this._provider, component, value, this);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0006CAFB File Offset: 0x0006ACFB
		public override bool ShouldSerializeValue(object comp)
		{
			return this._extenderInfo.ExtenderShouldSerializeValue(this._provider, comp);
		}

		// Token: 0x04000F44 RID: 3908
		private readonly ReflectPropertyDescriptor _extenderInfo;

		// Token: 0x04000F45 RID: 3909
		private readonly IExtenderProvider _provider;
	}
}
