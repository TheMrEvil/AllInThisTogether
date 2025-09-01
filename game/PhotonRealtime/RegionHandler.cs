using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000039 RID: 57
	public class RegionHandler
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009029 File Offset: 0x00007229
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00009031 File Offset: 0x00007231
		public List<Region> EnabledRegions
		{
			[CompilerGenerated]
			get
			{
				return this.<EnabledRegions>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<EnabledRegions>k__BackingField = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000903C File Offset: 0x0000723C
		public Region BestRegion
		{
			get
			{
				if (this.EnabledRegions == null)
				{
					return null;
				}
				if (this.bestRegionCache != null)
				{
					return this.bestRegionCache;
				}
				this.EnabledRegions.Sort((Region a, Region b) => a.Ping.CompareTo(b.Ping));
				int num = (int)((float)this.EnabledRegions[0].Ping * this.pingSimilarityFactor);
				Region region = this.EnabledRegions[0];
				foreach (Region region2 in this.EnabledRegions)
				{
					if (region2.Ping <= num && region2.Code.CompareTo(region.Code) < 0)
					{
						region = region2;
					}
				}
				this.bestRegionCache = region;
				return this.bestRegionCache;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009120 File Offset: 0x00007320
		public string SummaryToCache
		{
			get
			{
				if (this.BestRegion != null && this.BestRegion.Ping < RegionPinger.MaxMillisecondsPerPing)
				{
					return string.Concat(new string[]
					{
						this.BestRegion.Code,
						";",
						this.BestRegion.Ping.ToString(),
						";",
						this.availableRegionCodes
					});
				}
				return this.availableRegionCodes;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00009198 File Offset: 0x00007398
		public string GetResults()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Region Pinging Result: {0}\n", this.BestRegion.ToString(false));
			foreach (RegionPinger regionPinger in this.pingerList)
			{
				stringBuilder.AppendLine(regionPinger.GetResults());
			}
			stringBuilder.AppendFormat("Previous summary: {0}", this.previousSummaryProvided);
			return stringBuilder.ToString();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009228 File Offset: 0x00007428
		public void SetRegions(OperationResponse opGetRegions, LoadBalancingClient loadBalancingClient = null)
		{
			if (opGetRegions.OperationCode != 220)
			{
				return;
			}
			if (opGetRegions.ReturnCode != 0)
			{
				return;
			}
			string[] array = opGetRegions[210] as string[];
			string[] array2 = opGetRegions[230] as string[];
			if (array == null || array2 == null || array.Length != array2.Length)
			{
				if (loadBalancingClient != null)
				{
					loadBalancingClient.DebugReturn(DebugLevel.ERROR, "RegionHandler.SetRegions() failed. Received regions and servers must be non null and of equal length. Could not read regions.");
				}
				return;
			}
			this.bestRegionCache = null;
			this.EnabledRegions = new List<Region>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array2[i];
				if (RegionHandler.PortToPingOverride != 0)
				{
					text = LoadBalancingClient.ReplacePortWithAlternative(array2[i], RegionHandler.PortToPingOverride);
				}
				if (loadBalancingClient != null && loadBalancingClient.AddressRewriter != null)
				{
					text = loadBalancingClient.AddressRewriter(text, ServerConnection.MasterServer);
				}
				Region region = new Region(array[i], text);
				if (!string.IsNullOrEmpty(region.Code))
				{
					this.EnabledRegions.Add(region);
				}
			}
			Array.Sort<string>(array);
			this.availableRegionCodes = string.Join(",", array);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000931E File Offset: 0x0000751E
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00009326 File Offset: 0x00007526
		public bool IsPinging
		{
			[CompilerGenerated]
			get
			{
				return this.<IsPinging>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsPinging>k__BackingField = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000932F File Offset: 0x0000752F
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00009337 File Offset: 0x00007537
		public bool Aborted
		{
			[CompilerGenerated]
			get
			{
				return this.<Aborted>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Aborted>k__BackingField = value;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009340 File Offset: 0x00007540
		public RegionHandler(ushort masterServerPortOverride = 0)
		{
			RegionHandler.PortToPingOverride = masterServerPortOverride;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009378 File Offset: 0x00007578
		public bool PingMinimumOfRegions(Action<RegionHandler> onCompleteCallback, string previousSummary)
		{
			if (this.EnabledRegions == null || this.EnabledRegions.Count == 0)
			{
				return false;
			}
			if (this.IsPinging)
			{
				return false;
			}
			this.Aborted = false;
			this.IsPinging = true;
			this.previousSummaryProvided = previousSummary;
			if (this.emptyMonoBehavior != null)
			{
				this.emptyMonoBehavior.SelfDestroy();
			}
			this.emptyMonoBehavior = MonoBehaviourEmpty.BuildInstance("RegionHandler");
			this.emptyMonoBehavior.onCompleteCall = onCompleteCallback;
			this.onCompleteCall = new Action<RegionHandler>(this.emptyMonoBehavior.CompleteOnMainThread);
			if (string.IsNullOrEmpty(previousSummary))
			{
				return this.PingEnabledRegions();
			}
			string[] array = previousSummary.Split(';', StringSplitOptions.None);
			if (array.Length < 3)
			{
				return this.PingEnabledRegions();
			}
			int num;
			if (!int.TryParse(array[1], out num))
			{
				return this.PingEnabledRegions();
			}
			string prevBestRegionCode = array[0];
			string value = array[2];
			if (string.IsNullOrEmpty(prevBestRegionCode))
			{
				return this.PingEnabledRegions();
			}
			if (string.IsNullOrEmpty(value))
			{
				return this.PingEnabledRegions();
			}
			if (!this.availableRegionCodes.Equals(value) || !this.availableRegionCodes.Contains(prevBestRegionCode))
			{
				return this.PingEnabledRegions();
			}
			if (num >= RegionPinger.PingWhenFailed)
			{
				return this.PingEnabledRegions();
			}
			this.previousPing = num;
			RegionPinger regionPinger = new RegionPinger(this.EnabledRegions.Find((Region r) => r.Code.Equals(prevBestRegionCode)), new Action<Region>(this.OnPreferredRegionPinged));
			List<RegionPinger> obj = this.pingerList;
			lock (obj)
			{
				this.pingerList.Clear();
				this.pingerList.Add(regionPinger);
			}
			regionPinger.Start();
			return true;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009534 File Offset: 0x00007734
		public void Abort()
		{
			if (this.Aborted)
			{
				return;
			}
			this.Aborted = true;
			List<RegionPinger> obj = this.pingerList;
			lock (obj)
			{
				foreach (RegionPinger regionPinger in this.pingerList)
				{
					regionPinger.Abort();
				}
			}
			if (this.emptyMonoBehavior != null)
			{
				this.emptyMonoBehavior.SelfDestroy();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000095D4 File Offset: 0x000077D4
		private void OnPreferredRegionPinged(Region preferredRegion)
		{
			if (preferredRegion.Ping > this.BestRegionSummaryPingLimit || (float)preferredRegion.Ping > (float)this.previousPing * this.rePingFactor)
			{
				this.PingEnabledRegions();
				return;
			}
			this.IsPinging = false;
			this.onCompleteCall(this);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009624 File Offset: 0x00007824
		private bool PingEnabledRegions()
		{
			if (this.EnabledRegions == null || this.EnabledRegions.Count == 0)
			{
				return false;
			}
			List<RegionPinger> obj = this.pingerList;
			lock (obj)
			{
				this.pingerList.Clear();
				foreach (Region region in this.EnabledRegions)
				{
					RegionPinger regionPinger = new RegionPinger(region, new Action<Region>(this.OnRegionDone));
					this.pingerList.Add(regionPinger);
					regionPinger.Start();
				}
			}
			return true;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000096E0 File Offset: 0x000078E0
		private void OnRegionDone(Region region)
		{
			List<RegionPinger> obj = this.pingerList;
			lock (obj)
			{
				if (!this.IsPinging)
				{
					return;
				}
				this.bestRegionCache = null;
				using (List<RegionPinger>.Enumerator enumerator = this.pingerList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Done)
						{
							return;
						}
					}
				}
				this.IsPinging = false;
			}
			if (!this.Aborted)
			{
				this.onCompleteCall(this);
			}
		}

		// Token: 0x040001CE RID: 462
		public static Type PingImplementation;

		// Token: 0x040001CF RID: 463
		[CompilerGenerated]
		private List<Region> <EnabledRegions>k__BackingField;

		// Token: 0x040001D0 RID: 464
		private string availableRegionCodes;

		// Token: 0x040001D1 RID: 465
		private Region bestRegionCache;

		// Token: 0x040001D2 RID: 466
		private readonly List<RegionPinger> pingerList = new List<RegionPinger>();

		// Token: 0x040001D3 RID: 467
		private Action<RegionHandler> onCompleteCall;

		// Token: 0x040001D4 RID: 468
		private int previousPing;

		// Token: 0x040001D5 RID: 469
		private string previousSummaryProvided;

		// Token: 0x040001D6 RID: 470
		protected internal static ushort PortToPingOverride;

		// Token: 0x040001D7 RID: 471
		private float rePingFactor = 1.2f;

		// Token: 0x040001D8 RID: 472
		private float pingSimilarityFactor = 1.2f;

		// Token: 0x040001D9 RID: 473
		public int BestRegionSummaryPingLimit = 90;

		// Token: 0x040001DA RID: 474
		[CompilerGenerated]
		private bool <IsPinging>k__BackingField;

		// Token: 0x040001DB RID: 475
		[CompilerGenerated]
		private bool <Aborted>k__BackingField;

		// Token: 0x040001DC RID: 476
		private MonoBehaviourEmpty emptyMonoBehavior;

		// Token: 0x02000046 RID: 70
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600022A RID: 554 RVA: 0x0000B689 File Offset: 0x00009889
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000B695 File Offset: 0x00009895
			public <>c()
			{
			}

			// Token: 0x0600022C RID: 556 RVA: 0x0000B6A0 File Offset: 0x000098A0
			internal int <get_BestRegion>b__8_0(Region a, Region b)
			{
				return a.Ping.CompareTo(b.Ping);
			}

			// Token: 0x04000224 RID: 548
			public static readonly RegionHandler.<>c <>9 = new RegionHandler.<>c();

			// Token: 0x04000225 RID: 549
			public static Comparison<Region> <>9__8_0;
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x0600022D RID: 557 RVA: 0x0000B6C1 File Offset: 0x000098C1
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x0600022E RID: 558 RVA: 0x0000B6C9 File Offset: 0x000098C9
			internal bool <PingMinimumOfRegions>b__0(Region r)
			{
				return r.Code.Equals(this.prevBestRegionCode);
			}

			// Token: 0x04000226 RID: 550
			public string prevBestRegionCode;
		}
	}
}
