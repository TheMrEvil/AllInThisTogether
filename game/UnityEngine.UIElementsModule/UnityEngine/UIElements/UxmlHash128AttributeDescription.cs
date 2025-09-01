using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E4 RID: 740
	public class UxmlHash128AttributeDescription : TypedUxmlAttributeDescription<Hash128>
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x000653B8 File Offset: 0x000635B8
		public UxmlHash128AttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = default(Hash128);
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x000653F8 File Offset: 0x000635F8
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00065420 File Offset: 0x00063620
		public override Hash128 GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00065460 File Offset: 0x00063660
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Hash128 value)
		{
			return base.TryGetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue, ref value);
		}

		// Token: 0x020002E5 RID: 741
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060018A7 RID: 6311 RVA: 0x000654A0 File Offset: 0x000636A0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060018A8 RID: 6312 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x060018A9 RID: 6313 RVA: 0x000654AC File Offset: 0x000636AC
			internal Hash128 <GetValueFromBag>b__3_0(string s, Hash128 i)
			{
				return Hash128.Parse(s);
			}

			// Token: 0x060018AA RID: 6314 RVA: 0x000654AC File Offset: 0x000636AC
			internal Hash128 <TryGetValueFromBag>b__4_0(string s, Hash128 i)
			{
				return Hash128.Parse(s);
			}

			// Token: 0x04000A95 RID: 2709
			public static readonly UxmlHash128AttributeDescription.<>c <>9 = new UxmlHash128AttributeDescription.<>c();

			// Token: 0x04000A96 RID: 2710
			public static Func<string, Hash128, Hash128> <>9__3_0;

			// Token: 0x04000A97 RID: 2711
			public static Func<string, Hash128, Hash128> <>9__4_0;
		}
	}
}
