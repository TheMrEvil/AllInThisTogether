using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Push;

namespace Parse
{
	// Token: 0x02000020 RID: 32
	public static class PushServiceExtensions
	{
		// Token: 0x060001BE RID: 446 RVA: 0x0000819E File Offset: 0x0000639E
		public static Task SendPushAlertAsync(this IServiceHub serviceHub, string alert)
		{
			return new ParsePush(serviceHub)
			{
				Alert = alert
			}.SendAsync();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000081B2 File Offset: 0x000063B2
		public static Task SendPushAlertAsync(this IServiceHub serviceHub, string alert, string channel)
		{
			return new ParsePush(serviceHub)
			{
				Channels = new List<string>
				{
					channel
				},
				Alert = alert
			}.SendAsync();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000081D8 File Offset: 0x000063D8
		public static Task SendPushAlertAsync(this IServiceHub serviceHub, string alert, IEnumerable<string> channels)
		{
			return new ParsePush(serviceHub)
			{
				Channels = channels,
				Alert = alert
			}.SendAsync();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000081F3 File Offset: 0x000063F3
		public static Task SendPushAlertAsync(this IServiceHub serviceHub, string alert, ParseQuery<ParseInstallation> query)
		{
			return new ParsePush(serviceHub)
			{
				Query = query,
				Alert = alert
			}.SendAsync();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000820E File Offset: 0x0000640E
		public static Task SendPushDataAsync(this IServiceHub serviceHub, IDictionary<string, object> data)
		{
			return new ParsePush(serviceHub)
			{
				Data = data
			}.SendAsync();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008222 File Offset: 0x00006422
		public static Task SendPushDataAsync(this IServiceHub serviceHub, IDictionary<string, object> data, string channel)
		{
			return new ParsePush(serviceHub)
			{
				Channels = new List<string>
				{
					channel
				},
				Data = data
			}.SendAsync();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008248 File Offset: 0x00006448
		public static Task SendPushDataAsync(this IServiceHub serviceHub, IDictionary<string, object> data, IEnumerable<string> channels)
		{
			return new ParsePush(serviceHub)
			{
				Channels = channels,
				Data = data
			}.SendAsync();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008263 File Offset: 0x00006463
		public static Task SendPushDataAsync(this IServiceHub serviceHub, IDictionary<string, object> data, ParseQuery<ParseInstallation> query)
		{
			return new ParsePush(serviceHub)
			{
				Query = query,
				Data = data
			}.SendAsync();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001C6 RID: 454 RVA: 0x0000827E File Offset: 0x0000647E
		// (remove) Token: 0x060001C7 RID: 455 RVA: 0x0000828B File Offset: 0x0000648B
		public static event EventHandler<ParsePushNotificationEvent> ParsePushNotificationReceived
		{
			add
			{
				PushServiceExtensions.parsePushNotificationReceived.Add(value);
			}
			remove
			{
				PushServiceExtensions.parsePushNotificationReceived.Remove(value);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008298 File Offset: 0x00006498
		public static Task SubscribeToPushChannelAsync(this IServiceHub serviceHub, string channel, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.SubscribeToPushChannelsAsync(new List<string>
			{
				channel
			}, cancellationToken);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000082AD File Offset: 0x000064AD
		public static Task SubscribeToPushChannelsAsync(this IServiceHub serviceHub, IEnumerable<string> channels, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.PushChannelsController.SubscribeAsync(channels, serviceHub, cancellationToken);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000082BD File Offset: 0x000064BD
		public static Task UnsubscribeToPushChannelAsync(this IServiceHub serviceHub, string channel, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.UnsubscribeToPushChannelsAsync(new List<string>
			{
				channel
			}, cancellationToken);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000082D2 File Offset: 0x000064D2
		public static Task UnsubscribeToPushChannelsAsync(this IServiceHub serviceHub, IEnumerable<string> channels, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.PushChannelsController.UnsubscribeAsync(channels, serviceHub, cancellationToken);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000082E2 File Offset: 0x000064E2
		// Note: this type is marked as 'beforefieldinit'.
		static PushServiceExtensions()
		{
		}

		// Token: 0x0400003A RID: 58
		internal static readonly SynchronizedEventHandler<ParsePushNotificationEvent> parsePushNotificationReceived = new SynchronizedEventHandler<ParsePushNotificationEvent>();
	}
}
