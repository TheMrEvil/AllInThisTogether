using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/UnityAnalytics/ContinuousEvent/Manager.h")]
	[ExcludeFromDocs]
	public class ContinuousEvent
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public static AnalyticsResult RegisterCollector<T>(string metricName, Func<T> del) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(metricName);
			if (flag)
			{
				throw new ArgumentException("Cannot set metric name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalRegisterCollector(typeof(T).ToString(), metricName, del);
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B24 File Offset: 0x00000D24
		public static AnalyticsResult SetEventHistogramThresholds<T>(string eventName, int count, T[] data, int ver = 1, string prefix = "") where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalSetEventHistogramThresholds(typeof(T).ToString(), eventName, count, data, ver, prefix);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B78 File Offset: 0x00000D78
		public static AnalyticsResult SetCustomEventHistogramThresholds<T>(string eventName, int count, T[] data) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalSetCustomEventHistogramThresholds(typeof(T).ToString(), eventName, count, data);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static AnalyticsResult ConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled = true)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalConfigureCustomEvent(customEventName, metricName, interval, period, enabled);
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static AnalyticsResult ConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled = true, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalConfigureEvent(eventName, metricName, interval, period, enabled, ver, prefix);
			}
			return result;
		}

		// Token: 0x06000067 RID: 103
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalRegisterCollector(string type, string metricName, object collector);

		// Token: 0x06000068 RID: 104
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalSetEventHistogramThresholds(string type, string eventName, int count, object data, int ver, string prefix);

		// Token: 0x06000069 RID: 105
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalSetCustomEventHistogramThresholds(string type, string eventName, int count, object data);

		// Token: 0x0600006A RID: 106
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled);

		// Token: 0x0600006B RID: 107
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled, int ver, string prefix);

		// Token: 0x0600006C RID: 108 RVA: 0x00002C54 File Offset: 0x00000E54
		internal static bool IsInitialized()
		{
			return Analytics.IsInitialized();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C6B File Offset: 0x00000E6B
		public ContinuousEvent()
		{
		}
	}
}
