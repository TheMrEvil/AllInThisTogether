using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Realtime
{
	// Token: 0x02000003 RID: 3
	public class ConnectionHandler : MonoBehaviour
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000023C7 File Offset: 0x000005C7
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000023CF File Offset: 0x000005CF
		public LoadBalancingClient Client
		{
			[CompilerGenerated]
			get
			{
				return this.<Client>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Client>k__BackingField = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000023E0 File Offset: 0x000005E0
		public int CountSendAcksOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<CountSendAcksOnly>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CountSendAcksOnly>k__BackingField = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023E9 File Offset: 0x000005E9
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000023F1 File Offset: 0x000005F1
		public bool FallbackThreadRunning
		{
			[CompilerGenerated]
			get
			{
				return this.<FallbackThreadRunning>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FallbackThreadRunning>k__BackingField = value;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023FA File Offset: 0x000005FA
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void StaticReset()
		{
			ConnectionHandler.AppQuits = false;
			ConnectionHandler.AppPause = false;
			ConnectionHandler.AppPauseRecent = false;
			ConnectionHandler.AppOutOfFocus = false;
			ConnectionHandler.AppOutOfFocusRecent = false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000241A File Offset: 0x0000061A
		protected virtual void Awake()
		{
			if (this.ApplyDontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002430 File Offset: 0x00000630
		protected virtual void OnDisable()
		{
			this.StopFallbackSendAckThread();
			if (ConnectionHandler.AppQuits)
			{
				if (this.Client != null && this.Client.IsConnected)
				{
					this.Client.Disconnect(DisconnectCause.ApplicationQuit);
					this.Client.LoadBalancingPeer.StopThread();
				}
				SupportClass.StopAllBackgroundCalls();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002482 File Offset: 0x00000682
		public void OnApplicationQuit()
		{
			ConnectionHandler.AppQuits = true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000248A File Offset: 0x0000068A
		public void OnApplicationPause(bool pause)
		{
			ConnectionHandler.AppPause = pause;
			if (pause)
			{
				ConnectionHandler.AppPauseRecent = true;
				base.CancelInvoke("ResetAppPauseRecent");
				return;
			}
			base.Invoke("ResetAppPauseRecent", 5f);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024B7 File Offset: 0x000006B7
		private void ResetAppPauseRecent()
		{
			ConnectionHandler.AppPauseRecent = false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024BF File Offset: 0x000006BF
		public void OnApplicationFocus(bool focus)
		{
			ConnectionHandler.AppOutOfFocus = !focus;
			if (!focus)
			{
				ConnectionHandler.AppOutOfFocusRecent = true;
				base.CancelInvoke("ResetAppOutOfFocusRecent");
				return;
			}
			base.Invoke("ResetAppOutOfFocusRecent", 5f);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024EF File Offset: 0x000006EF
		private void ResetAppOutOfFocusRecent()
		{
			ConnectionHandler.AppOutOfFocusRecent = false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F7 File Offset: 0x000006F7
		public static bool IsNetworkReachableUnity()
		{
			return Application.internetReachability > NetworkReachability.NotReachable;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002501 File Offset: 0x00000701
		public void StartFallbackSendAckThread()
		{
			if (this.stateTimer != null)
			{
				return;
			}
			this.stateTimer = new Timer(new TimerCallback(this.RealtimeFallback), null, 50, 50);
			this.FallbackThreadRunning = true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000252F File Offset: 0x0000072F
		public void StopFallbackSendAckThread()
		{
			if (this.stateTimer != null)
			{
				this.stateTimer.Dispose();
				this.stateTimer = null;
			}
			this.FallbackThreadRunning = false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002552 File Offset: 0x00000752
		public void RealtimeFallbackInvoke()
		{
			this.RealtimeFallback(null);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000255C File Offset: 0x0000075C
		public void RealtimeFallback(object state = null)
		{
			if (this.Client == null)
			{
				return;
			}
			if (!this.Client.IsConnected || this.Client.LoadBalancingPeer.ConnectionTime - this.Client.LoadBalancingPeer.LastSendOutgoingTime <= 100)
			{
				if (this.backgroundStopwatch.IsRunning)
				{
					this.backgroundStopwatch.Reset();
				}
				this.didSendAcks = false;
				return;
			}
			if (!this.didSendAcks)
			{
				this.backgroundStopwatch.Reset();
				this.backgroundStopwatch.Start();
			}
			if (this.backgroundStopwatch.ElapsedMilliseconds > (long)this.KeepAliveInBackground)
			{
				if (this.DisconnectAfterKeepAlive && this.Client.State != ClientState.Disconnecting)
				{
					this.Client.Disconnect();
				}
				return;
			}
			this.didSendAcks = true;
			int countSendAcksOnly = this.CountSendAcksOnly;
			this.CountSendAcksOnly = countSendAcksOnly + 1;
			this.Client.LoadBalancingPeer.SendAcksOnly();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002644 File Offset: 0x00000844
		public ConnectionHandler()
		{
		}

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private LoadBalancingClient <Client>k__BackingField;

		// Token: 0x04000012 RID: 18
		public bool DisconnectAfterKeepAlive;

		// Token: 0x04000013 RID: 19
		public int KeepAliveInBackground = 60000;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		private int <CountSendAcksOnly>k__BackingField;

		// Token: 0x04000015 RID: 21
		[CompilerGenerated]
		private bool <FallbackThreadRunning>k__BackingField;

		// Token: 0x04000016 RID: 22
		public bool ApplyDontDestroyOnLoad = true;

		// Token: 0x04000017 RID: 23
		[NonSerialized]
		public static bool AppQuits;

		// Token: 0x04000018 RID: 24
		[NonSerialized]
		public static bool AppPause;

		// Token: 0x04000019 RID: 25
		[NonSerialized]
		public static bool AppPauseRecent;

		// Token: 0x0400001A RID: 26
		[NonSerialized]
		public static bool AppOutOfFocus;

		// Token: 0x0400001B RID: 27
		[NonSerialized]
		public static bool AppOutOfFocusRecent;

		// Token: 0x0400001C RID: 28
		private bool didSendAcks;

		// Token: 0x0400001D RID: 29
		private readonly Stopwatch backgroundStopwatch = new Stopwatch();

		// Token: 0x0400001E RID: 30
		private Timer stateTimer;
	}
}
