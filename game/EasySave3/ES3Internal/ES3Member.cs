using System;

namespace ES3Internal
{
	// Token: 0x020000E4 RID: 228
	public class ES3Member
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0001E5A5 File Offset: 0x0001C7A5
		public ES3Member(string name, Type type, bool isProperty)
		{
			this.name = name;
			this.type = type;
			this.isProperty = isProperty;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001E5C2 File Offset: 0x0001C7C2
		public ES3Member(ES3Reflection.ES3ReflectedMember reflectedMember)
		{
			this.reflectedMember = reflectedMember;
			this.name = reflectedMember.Name;
			this.type = reflectedMember.MemberType;
			this.isProperty = reflectedMember.isProperty;
			this.useReflection = true;
		}

		// Token: 0x0400015C RID: 348
		public string name;

		// Token: 0x0400015D RID: 349
		public Type type;

		// Token: 0x0400015E RID: 350
		public bool isProperty;

		// Token: 0x0400015F RID: 351
		public ES3Reflection.ES3ReflectedMember reflectedMember;

		// Token: 0x04000160 RID: 352
		public bool useReflection;
	}
}
