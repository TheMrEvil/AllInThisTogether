using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000023 RID: 35
	public static class Aliasing
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00007986 File Offset: 0x00005B86
		public unsafe static void ExpectAliased(void* a, void* b)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007988 File Offset: 0x00005B88
		public static void ExpectAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000798A File Offset: 0x00005B8A
		public unsafe static void ExpectAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000798C File Offset: 0x00005B8C
		public unsafe static void ExpectAliased<A>(in A a, void* b) where A : struct
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000798E File Offset: 0x00005B8E
		public unsafe static void ExpectNotAliased(void* a, void* b)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007990 File Offset: 0x00005B90
		public static void ExpectNotAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007992 File Offset: 0x00005B92
		public unsafe static void ExpectNotAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007994 File Offset: 0x00005B94
		public unsafe static void ExpectNotAliased<A>(in A a, void* b) where A : struct
		{
		}
	}
}
