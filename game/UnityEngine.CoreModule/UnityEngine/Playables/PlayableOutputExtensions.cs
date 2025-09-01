using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000449 RID: 1097
	public static class PlayableOutputExtensions
	{
		// Token: 0x0600269F RID: 9887 RVA: 0x00040848 File Offset: 0x0003EA48
		public static bool IsOutputNull<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsNull();
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00040870 File Offset: 0x0003EA70
		public static bool IsOutputValid<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsValid();
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x00040898 File Offset: 0x0003EA98
		public static Object GetReferenceObject<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetReferenceObject();
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000408C0 File Offset: 0x0003EAC0
		public static void SetReferenceObject<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetReferenceObject(value);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000408E8 File Offset: 0x0003EAE8
		public static Object GetUserData<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetUserData();
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00040910 File Offset: 0x0003EB10
		public static void SetUserData<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetUserData(value);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00040938 File Offset: 0x0003EB38
		public static Playable GetSourcePlayable<U>(this U output) where U : struct, IPlayableOutput
		{
			return new Playable(output.GetHandle().GetSourcePlayable());
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00040964 File Offset: 0x0003EB64
		public static void SetSourcePlayable<U, V>(this U output, V value) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), output.GetSourceOutputPort<U>());
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0004099C File Offset: 0x0003EB9C
		public static void SetSourcePlayable<U, V>(this U output, V value, int port) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), port);
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000409D0 File Offset: 0x0003EBD0
		public static int GetSourceOutputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000409F8 File Offset: 0x0003EBF8
		public static float GetWeight<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetWeight();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00040A20 File Offset: 0x0003EC20
		public static void SetWeight<U>(this U output, float value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetWeight(value);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00040A48 File Offset: 0x0003EC48
		public static void PushNotification<U>(this U output, Playable origin, INotification notification, object context = null) where U : struct, IPlayableOutput
		{
			output.GetHandle().PushNotification(origin.GetHandle(), notification, context);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00040A78 File Offset: 0x0003EC78
		public static INotificationReceiver[] GetNotificationReceivers<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetNotificationReceivers();
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x00040AA0 File Offset: 0x0003ECA0
		public static void AddNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().AddNotificationReceiver(receiver);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x00040AC8 File Offset: 0x0003ECC8
		public static void RemoveNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().RemoveNotificationReceiver(receiver);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00040AF0 File Offset: 0x0003ECF0
		[Obsolete("Method GetSourceInputPort has been renamed to GetSourceOutputPort (UnityUpgradable) -> GetSourceOutputPort<U>(*)", false)]
		public static int GetSourceInputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x00040B17 File Offset: 0x0003ED17
		[Obsolete("Method SetSourceInputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceInputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x00040B17 File Offset: 0x0003ED17
		[Obsolete("Method SetSourceOutputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceOutputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}
	}
}
