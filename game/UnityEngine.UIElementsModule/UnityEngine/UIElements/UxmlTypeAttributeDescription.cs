using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E1 RID: 737
	public class UxmlTypeAttributeDescription<TBase> : TypedUxmlAttributeDescription<Type>
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x00065059 File Offset: 0x00063259
		public UxmlTypeAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = null;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00065084 File Offset: 0x00063284
		public override string defaultValueAsString
		{
			get
			{
				return (base.defaultValue == null) ? "null" : base.defaultValue.FullName;
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000650B0 File Offset: 0x000632B0
		public override Type GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000650DC File Offset: 0x000632DC
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Type value)
		{
			return base.TryGetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue, ref value);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0006510C File Offset: 0x0006330C
		private Type ConvertValueToType(string v, Type defaultValue)
		{
			bool flag = string.IsNullOrEmpty(v);
			Type result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				try
				{
					Type type = Type.GetType(v, true);
					bool flag2 = !typeof(TBase).IsAssignableFrom(type);
					if (!flag2)
					{
						return type;
					}
					Debug.LogError(string.Concat(new string[]
					{
						"Type: Invalid type \"",
						v,
						"\". Type must derive from ",
						typeof(TBase).FullName,
						"."
					}));
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000651B4 File Offset: 0x000633B4
		[CompilerGenerated]
		private Type <GetValueFromBag>b__3_0(string s, Type type1)
		{
			return this.ConvertValueToType(s, type1);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000651B4 File Offset: 0x000633B4
		[CompilerGenerated]
		private Type <TryGetValueFromBag>b__4_0(string s, Type type1)
		{
			return this.ConvertValueToType(s, type1);
		}
	}
}
