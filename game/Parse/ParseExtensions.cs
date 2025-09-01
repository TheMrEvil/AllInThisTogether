using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x0200001F RID: 31
	public static class ParseExtensions
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000080CE File Offset: 0x000062CE
		public static Task<T> FetchAsync<T>(this T obj) where T : ParseObject
		{
			return obj.FetchAsyncInternal(CancellationToken.None).OnSuccess((Task<ParseObject> t) => (T)((object)t.Result));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008104 File Offset: 0x00006304
		public static Task<T> FetchAsync<T>(this T target, CancellationToken cancellationToken) where T : ParseObject
		{
			return target.FetchAsyncInternal(cancellationToken).OnSuccess((Task<ParseObject> task) => (T)((object)task.Result));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008136 File Offset: 0x00006336
		public static Task<T> FetchIfNeededAsync<T>(this T obj) where T : ParseObject
		{
			return obj.FetchIfNeededAsyncInternal(CancellationToken.None).OnSuccess((Task<ParseObject> t) => (T)((object)t.Result));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000816C File Offset: 0x0000636C
		public static Task<T> FetchIfNeededAsync<T>(this T obj, CancellationToken cancellationToken) where T : ParseObject
		{
			return obj.FetchIfNeededAsyncInternal(cancellationToken).OnSuccess((Task<ParseObject> t) => (T)((object)t.Result));
		}

		// Token: 0x020000D8 RID: 216
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__0<T> where T : ParseObject
		{
			// Token: 0x0600066E RID: 1646 RVA: 0x00014405 File Offset: 0x00012605
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__0()
			{
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x00014411 File Offset: 0x00012611
			public <>c__0()
			{
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x00014419 File Offset: 0x00012619
			internal T <FetchAsync>b__0_0(Task<ParseObject> t)
			{
				return (T)((object)t.Result);
			}

			// Token: 0x040001B7 RID: 439
			public static readonly ParseExtensions.<>c__0<T> <>9 = new ParseExtensions.<>c__0<T>();

			// Token: 0x040001B8 RID: 440
			public static Func<Task<ParseObject>, T> <>9__0_0;
		}

		// Token: 0x020000D9 RID: 217
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__1<T> where T : ParseObject
		{
			// Token: 0x06000671 RID: 1649 RVA: 0x00014426 File Offset: 0x00012626
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__1()
			{
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x00014432 File Offset: 0x00012632
			public <>c__1()
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0001443A File Offset: 0x0001263A
			internal T <FetchAsync>b__1_0(Task<ParseObject> task)
			{
				return (T)((object)task.Result);
			}

			// Token: 0x040001B9 RID: 441
			public static readonly ParseExtensions.<>c__1<T> <>9 = new ParseExtensions.<>c__1<T>();

			// Token: 0x040001BA RID: 442
			public static Func<Task<ParseObject>, T> <>9__1_0;
		}

		// Token: 0x020000DA RID: 218
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__2<T> where T : ParseObject
		{
			// Token: 0x06000674 RID: 1652 RVA: 0x00014447 File Offset: 0x00012647
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__2()
			{
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x00014453 File Offset: 0x00012653
			public <>c__2()
			{
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0001445B File Offset: 0x0001265B
			internal T <FetchIfNeededAsync>b__2_0(Task<ParseObject> t)
			{
				return (T)((object)t.Result);
			}

			// Token: 0x040001BB RID: 443
			public static readonly ParseExtensions.<>c__2<T> <>9 = new ParseExtensions.<>c__2<T>();

			// Token: 0x040001BC RID: 444
			public static Func<Task<ParseObject>, T> <>9__2_0;
		}

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__3<T> where T : ParseObject
		{
			// Token: 0x06000677 RID: 1655 RVA: 0x00014468 File Offset: 0x00012668
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__3()
			{
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x00014474 File Offset: 0x00012674
			public <>c__3()
			{
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x0001447C File Offset: 0x0001267C
			internal T <FetchIfNeededAsync>b__3_0(Task<ParseObject> t)
			{
				return (T)((object)t.Result);
			}

			// Token: 0x040001BD RID: 445
			public static readonly ParseExtensions.<>c__3<T> <>9 = new ParseExtensions.<>c__3<T>();

			// Token: 0x040001BE RID: 446
			public static Func<Task<ParseObject>, T> <>9__3_0;
		}
	}
}
