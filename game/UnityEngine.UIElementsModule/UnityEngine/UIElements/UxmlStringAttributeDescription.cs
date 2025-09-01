using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D3 RID: 723
	public class UxmlStringAttributeDescription : TypedUxmlAttributeDescription<string>
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x000648AF File Offset: 0x00062AAF
		public UxmlStringAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = "";
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x000648E0 File Offset: 0x00062AE0
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue;
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000648F8 File Offset: 0x00062AF8
		public override string GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00064938 File Offset: 0x00062B38
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref string value)
		{
			return base.TryGetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue, ref value);
		}

		// Token: 0x020002D4 RID: 724
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001859 RID: 6233 RVA: 0x00064978 File Offset: 0x00062B78
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600185A RID: 6234 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x0600185B RID: 6235 RVA: 0x0000A501 File Offset: 0x00008701
			internal string <GetValueFromBag>b__3_0(string s, string t)
			{
				return s;
			}

			// Token: 0x0600185C RID: 6236 RVA: 0x0000A501 File Offset: 0x00008701
			internal string <TryGetValueFromBag>b__4_0(string s, string t)
			{
				return s;
			}

			// Token: 0x04000A7D RID: 2685
			public static readonly UxmlStringAttributeDescription.<>c <>9 = new UxmlStringAttributeDescription.<>c();

			// Token: 0x04000A7E RID: 2686
			public static Func<string, string, string> <>9__3_0;

			// Token: 0x04000A7F RID: 2687
			public static Func<string, string, string> <>9__4_0;
		}
	}
}
