using System;
using System.Collections.Generic;
using UnityEngine;

namespace MEC
{
	// Token: 0x020000A6 RID: 166
	public static class MECExtensionMethods1
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x0003481D File Offset: 0x00032A1D
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine)
		{
			return Timing.RunCoroutine(coroutine);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00034825 File Offset: 0x00032A25
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, GameObject gameObj)
		{
			return Timing.RunCoroutine(coroutine, gameObj);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0003482E File Offset: 0x00032A2E
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, int layer)
		{
			return Timing.RunCoroutine(coroutine, layer);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00034837 File Offset: 0x00032A37
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, string tag)
		{
			return Timing.RunCoroutine(coroutine, tag);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00034840 File Offset: 0x00032A40
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, GameObject gameObj, string tag)
		{
			return Timing.RunCoroutine(coroutine, gameObj, tag);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0003484A File Offset: 0x00032A4A
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, int layer, string tag)
		{
			return Timing.RunCoroutine(coroutine, layer, tag);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00034854 File Offset: 0x00032A54
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, Segment segment)
		{
			return Timing.RunCoroutine(coroutine, segment);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0003485D File Offset: 0x00032A5D
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, Segment segment, GameObject gameObj)
		{
			return Timing.RunCoroutine(coroutine, segment, gameObj);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00034867 File Offset: 0x00032A67
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, Segment segment, int layer)
		{
			return Timing.RunCoroutine(coroutine, segment, layer);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00034871 File Offset: 0x00032A71
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, Segment segment, string tag)
		{
			return Timing.RunCoroutine(coroutine, segment, tag);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0003487B File Offset: 0x00032A7B
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, Segment segment, GameObject gameObj, string tag)
		{
			return Timing.RunCoroutine(coroutine, segment, gameObj, tag);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00034886 File Offset: 0x00032A86
		public static CoroutineHandle RunCoroutine(this IEnumerator<float> coroutine, Segment segment, int layer, string tag)
		{
			return Timing.RunCoroutine(coroutine, segment, layer, tag);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00034891 File Offset: 0x00032A91
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, CoroutineHandle handle, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, handle, behaviorOnCollision);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0003489B File Offset: 0x00032A9B
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, GameObject gameObj, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, gameObj.GetInstanceID(), behaviorOnCollision);
			}
			return Timing.RunCoroutine(coroutine);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000348BA File Offset: 0x00032ABA
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, int layer, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, layer, behaviorOnCollision);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000348C4 File Offset: 0x00032AC4
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, string tag, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, tag, behaviorOnCollision);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000348CE File Offset: 0x00032ACE
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, GameObject gameObj, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, gameObj.GetInstanceID(), tag, behaviorOnCollision);
			}
			return Timing.RunCoroutineSingleton(coroutine, tag, behaviorOnCollision);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000348F0 File Offset: 0x00032AF0
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, int layer, string tag, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, layer, tag, behaviorOnCollision);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000348FB File Offset: 0x00032AFB
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, CoroutineHandle handle, Segment segment, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, handle, segment, behaviorOnCollision);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00034906 File Offset: 0x00032B06
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, Segment segment, GameObject gameObj, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, segment, gameObj.GetInstanceID(), behaviorOnCollision);
			}
			return Timing.RunCoroutine(coroutine, segment);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00034927 File Offset: 0x00032B27
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, Segment segment, int layer, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, segment, layer, behaviorOnCollision);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00034932 File Offset: 0x00032B32
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, Segment segment, string tag, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, segment, tag, behaviorOnCollision);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0003493D File Offset: 0x00032B3D
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, Segment segment, GameObject gameObj, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, segment, gameObj.GetInstanceID(), tag, behaviorOnCollision);
			}
			return Timing.RunCoroutineSingleton(coroutine, segment, tag, behaviorOnCollision);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00034963 File Offset: 0x00032B63
		public static CoroutineHandle RunCoroutineSingleton(this IEnumerator<float> coroutine, Segment segment, int layer, string tag, SingletonBehavior behaviorOnCollision)
		{
			return Timing.RunCoroutineSingleton(coroutine, segment, layer, tag, behaviorOnCollision);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00034970 File Offset: 0x00032B70
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine)
		{
			return Timing.WaitUntilDone(newCoroutine);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00034978 File Offset: 0x00032B78
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, string tag)
		{
			return Timing.WaitUntilDone(newCoroutine, tag);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00034981 File Offset: 0x00032B81
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, int layer)
		{
			return Timing.WaitUntilDone(newCoroutine, layer);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003498A File Offset: 0x00032B8A
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, int layer, string tag)
		{
			return Timing.WaitUntilDone(newCoroutine, layer, tag);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00034994 File Offset: 0x00032B94
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, Segment segment)
		{
			return Timing.WaitUntilDone(newCoroutine, segment);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003499D File Offset: 0x00032B9D
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, Segment segment, string tag)
		{
			return Timing.WaitUntilDone(newCoroutine, segment, tag);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000349A7 File Offset: 0x00032BA7
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, Segment segment, int layer)
		{
			return Timing.WaitUntilDone(newCoroutine, segment, layer);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000349B1 File Offset: 0x00032BB1
		public static float WaitUntilDone(this IEnumerator<float> newCoroutine, Segment segment, int layer, string tag)
		{
			return Timing.WaitUntilDone(newCoroutine, segment, layer, tag);
		}
	}
}
