using System;

namespace UnityEngine.Internal
{
	// Token: 0x02000398 RID: 920
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06001F01 RID: 7937 RVA: 0x00032850 File Offset: 0x00030A50
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00032864 File Offset: 0x00030A64
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0003287C File Offset: 0x00030A7C
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			bool flag = defaultValueAttribute == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.DefaultValue == null;
				if (flag2)
				{
					result = (defaultValueAttribute.Value == null);
				}
				else
				{
					result = this.DefaultValue.Equals(defaultValueAttribute.Value);
				}
			}
			return result;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000328CC File Offset: 0x00030ACC
		public override int GetHashCode()
		{
			bool flag = this.DefaultValue == null;
			int hashCode;
			if (flag)
			{
				hashCode = base.GetHashCode();
			}
			else
			{
				hashCode = this.DefaultValue.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x04000A36 RID: 2614
		private object DefaultValue;
	}
}
