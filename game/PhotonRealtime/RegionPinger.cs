using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Realtime
{
	// Token: 0x0200003A RID: 58
	public class RegionPinger
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000978C File Offset: 0x0000798C
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00009794 File Offset: 0x00007994
		public bool Done
		{
			[CompilerGenerated]
			get
			{
				return this.<Done>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Done>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000979D File Offset: 0x0000799D
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000097A5 File Offset: 0x000079A5
		public bool Aborted
		{
			[CompilerGenerated]
			get
			{
				return this.<Aborted>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Aborted>k__BackingField = value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000097AE File Offset: 0x000079AE
		public RegionPinger(Region region, Action<Region> onDoneCallback)
		{
			this.region = region;
			this.region.Ping = RegionPinger.PingWhenFailed;
			this.Done = false;
			this.onDoneCall = onDoneCallback;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000097DC File Offset: 0x000079DC
		private PhotonPing GetPingImplementation()
		{
			PhotonPing photonPing = null;
			if (RegionHandler.PingImplementation == null || RegionHandler.PingImplementation == typeof(PingMono))
			{
				photonPing = new PingMono();
			}
			if (photonPing == null && RegionHandler.PingImplementation != null)
			{
				photonPing = (PhotonPing)Activator.CreateInstance(RegionHandler.PingImplementation);
			}
			return photonPing;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009838 File Offset: 0x00007A38
		public bool Start()
		{
			this.ping = this.GetPingImplementation();
			this.Done = false;
			this.CurrentAttempt = 0;
			this.rttResults = new List<int>(RegionPinger.Attempts);
			if (this.Aborted)
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					this.RegionPingThreaded();
				});
			}
			catch
			{
				flag = false;
			}
			if (!flag)
			{
				SupportClass.StartBackgroundCalls(new Func<bool>(this.RegionPingThreaded), 0, "RegionPing_" + this.region.Code + "_" + this.region.Cluster);
			}
			return true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000098E0 File Offset: 0x00007AE0
		protected internal void Abort()
		{
			this.Aborted = true;
			if (this.ping != null)
			{
				this.ping.Dispose();
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000098FC File Offset: 0x00007AFC
		protected internal bool RegionPingThreaded()
		{
			this.region.Ping = RegionPinger.PingWhenFailed;
			int num = 0;
			int num2 = 0;
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				string text = this.region.HostAndPort;
				int num3 = text.LastIndexOf(':');
				if (num3 > 1)
				{
					text = text.Substring(0, num3);
				}
				stopwatch.Start();
				this.regionAddress = RegionPinger.ResolveHost(text);
				stopwatch.Stop();
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			}
			catch (Exception)
			{
				this.Aborted = true;
			}
			this.CurrentAttempt = 0;
			while (this.CurrentAttempt < RegionPinger.Attempts && !this.Aborted)
			{
				stopwatch.Reset();
				stopwatch.Start();
				try
				{
					this.ping.StartPing(this.regionAddress);
					goto IL_BF;
				}
				catch (Exception)
				{
					break;
				}
				goto IL_AB;
				IL_CC:
				stopwatch.Stop();
				int num4 = this.ping.Successful ? ((int)stopwatch.ElapsedMilliseconds) : RegionPinger.MaxMillisecondsPerPing;
				this.rttResults.Add(num4);
				num += num4;
				num2++;
				this.region.Ping = num / num2;
				int num5 = 4;
				while (!this.ping.Done() && num5 > 0)
				{
					num5--;
					Thread.Sleep(100);
				}
				Thread.Sleep(10);
				this.CurrentAttempt++;
				continue;
				IL_AB:
				if (stopwatch.ElapsedMilliseconds >= (long)RegionPinger.MaxMillisecondsPerPing)
				{
					goto IL_CC;
				}
				Thread.Sleep(1);
				IL_BF:
				if (this.ping.Done())
				{
					goto IL_CC;
				}
				goto IL_AB;
			}
			this.Done = true;
			this.ping.Dispose();
			if (this.rttResults.Count > 1 && num2 > 0)
			{
				int num6 = this.rttResults.Min();
				int num7 = this.rttResults.Max();
				int num8 = num - num7 + num6;
				this.region.Ping = num8 / num2;
			}
			this.onDoneCall(this.region);
			return false;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009AEC File Offset: 0x00007CEC
		protected internal IEnumerator RegionPingCoroutine()
		{
			this.region.Ping = RegionPinger.PingWhenFailed;
			int rttSum = 0;
			int replyCount = 0;
			Stopwatch sw = new Stopwatch();
			try
			{
				string text = this.region.HostAndPort;
				int num = text.LastIndexOf(':');
				if (num > 1)
				{
					text = text.Substring(0, num);
				}
				sw.Start();
				this.regionAddress = RegionPinger.ResolveHost(text);
				sw.Stop();
				if (sw.ElapsedMilliseconds > 100L)
				{
					UnityEngine.Debug.Log(string.Format("RegionPingCoroutine.ResolveHost() took: {0}ms", sw.ElapsedMilliseconds));
				}
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log(string.Format("RegionPingCoroutine ResolveHost failed for {0}. Caught: {1}", this.region, arg));
				this.Aborted = true;
			}
			this.CurrentAttempt = 0;
			while (this.CurrentAttempt < RegionPinger.Attempts)
			{
				if (this.Aborted)
				{
					yield return null;
				}
				sw.Reset();
				sw.Start();
				try
				{
					this.ping.StartPing(this.regionAddress);
					goto IL_1D4;
				}
				catch (Exception ex)
				{
					string[] array = new string[6];
					array[0] = "RegionPinger.RegionPingCoroutine() caught exception for ping.StartPing(). Exception: ";
					int num2 = 1;
					Exception ex2 = ex;
					array[num2] = ((ex2 != null) ? ex2.ToString() : null);
					array[2] = " Source: ";
					array[3] = ex.Source;
					array[4] = " Message: ";
					array[5] = ex.Message;
					UnityEngine.Debug.Log(string.Concat(array));
					break;
				}
				goto IL_1A1;
				IL_1E1:
				sw.Stop();
				int num3 = this.ping.Successful ? ((int)sw.ElapsedMilliseconds) : RegionPinger.MaxMillisecondsPerPing;
				this.rttResults.Add(num3);
				rttSum += num3;
				int num4 = replyCount;
				replyCount = num4 + 1;
				this.region.Ping = rttSum / replyCount;
				int i = 4;
				while (!this.ping.Done() && i > 0)
				{
					num4 = i;
					i = num4 - 1;
					yield return new WaitForSeconds(0.1f);
				}
				yield return new WaitForSeconds(0.1f);
				this.CurrentAttempt++;
				continue;
				IL_1A1:
				if (sw.ElapsedMilliseconds >= (long)RegionPinger.MaxMillisecondsPerPing)
				{
					goto IL_1E1;
				}
				yield return new WaitForSecondsRealtime(0.01f);
				IL_1D4:
				if (this.ping.Done())
				{
					goto IL_1E1;
				}
				goto IL_1A1;
			}
			this.Done = true;
			this.ping.Dispose();
			if (this.rttResults.Count > 1 && replyCount > 0)
			{
				int num5 = this.rttResults.Min();
				int num6 = this.rttResults.Max();
				int num7 = rttSum - num6 + num5;
				this.region.Ping = num7 / replyCount;
			}
			this.onDoneCall(this.region);
			yield return null;
			yield break;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009AFB File Offset: 0x00007CFB
		public string GetResults()
		{
			return string.Format("{0}: {1} ({2})", this.region.Code, this.region.Ping, this.rttResults.ToStringFull<int>());
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009B30 File Offset: 0x00007D30
		public static string ResolveHost(string hostName)
		{
			if (hostName.StartsWith("wss://"))
			{
				hostName = hostName.Substring(6);
			}
			if (hostName.StartsWith("ws://"))
			{
				hostName = hostName.Substring(5);
			}
			string text = string.Empty;
			try
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
				if (hostAddresses.Length == 1)
				{
					return hostAddresses[0].ToString();
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					if (ipaddress != null)
					{
						if (ipaddress.ToString().Contains(":"))
						{
							return ipaddress.ToString();
						}
						if (string.IsNullOrEmpty(text))
						{
							text = hostAddresses.ToString();
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return text;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009BE4 File Offset: 0x00007DE4
		// Note: this type is marked as 'beforefieldinit'.
		static RegionPinger()
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009C06 File Offset: 0x00007E06
		[CompilerGenerated]
		private void <Start>b__19_0(object o)
		{
			this.RegionPingThreaded();
		}

		// Token: 0x040001DD RID: 477
		public static int Attempts = 5;

		// Token: 0x040001DE RID: 478
		public static int MaxMillisecondsPerPing = 800;

		// Token: 0x040001DF RID: 479
		public static int PingWhenFailed = RegionPinger.Attempts * RegionPinger.MaxMillisecondsPerPing;

		// Token: 0x040001E0 RID: 480
		public int CurrentAttempt;

		// Token: 0x040001E1 RID: 481
		[CompilerGenerated]
		private bool <Done>k__BackingField;

		// Token: 0x040001E2 RID: 482
		[CompilerGenerated]
		private bool <Aborted>k__BackingField;

		// Token: 0x040001E3 RID: 483
		private Action<Region> onDoneCall;

		// Token: 0x040001E4 RID: 484
		private PhotonPing ping;

		// Token: 0x040001E5 RID: 485
		private List<int> rttResults;

		// Token: 0x040001E6 RID: 486
		private Region region;

		// Token: 0x040001E7 RID: 487
		private string regionAddress;

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		private sealed class <RegionPingCoroutine>d__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600022F RID: 559 RVA: 0x0000B6DC File Offset: 0x000098DC
			[DebuggerHidden]
			public <RegionPingCoroutine>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000230 RID: 560 RVA: 0x0000B6EB File Offset: 0x000098EB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000B6F0 File Offset: 0x000098F0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				RegionPinger regionPinger = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					regionPinger.region.Ping = RegionPinger.PingWhenFailed;
					rttSum = 0;
					replyCount = 0;
					sw = new Stopwatch();
					try
					{
						string text = regionPinger.region.HostAndPort;
						int num2 = text.LastIndexOf(':');
						if (num2 > 1)
						{
							text = text.Substring(0, num2);
						}
						sw.Start();
						regionPinger.regionAddress = RegionPinger.ResolveHost(text);
						sw.Stop();
						if (sw.ElapsedMilliseconds > 100L)
						{
							UnityEngine.Debug.Log(string.Format("RegionPingCoroutine.ResolveHost() took: {0}ms", sw.ElapsedMilliseconds));
						}
					}
					catch (Exception arg)
					{
						UnityEngine.Debug.Log(string.Format("RegionPingCoroutine ResolveHost failed for {0}. Caught: {1}", regionPinger.region, arg));
						regionPinger.Aborted = true;
					}
					regionPinger.CurrentAttempt = 0;
					goto IL_2D3;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_1D4;
				case 3:
					this.<>1__state = -1;
					goto IL_28F;
				case 4:
					this.<>1__state = -1;
					regionPinger.CurrentAttempt++;
					goto IL_2D3;
				case 5:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				IL_120:
				sw.Reset();
				sw.Start();
				try
				{
					regionPinger.ping.StartPing(regionPinger.regionAddress);
					goto IL_1D4;
				}
				catch (Exception ex)
				{
					string[] array = new string[6];
					array[0] = "RegionPinger.RegionPingCoroutine() caught exception for ping.StartPing(). Exception: ";
					int num3 = 1;
					Exception ex2 = ex;
					array[num3] = ((ex2 != null) ? ex2.ToString() : null);
					array[2] = " Source: ";
					array[3] = ex.Source;
					array[4] = " Message: ";
					array[5] = ex.Message;
					UnityEngine.Debug.Log(string.Concat(array));
					goto IL_2E3;
				}
				IL_1A1:
				if (sw.ElapsedMilliseconds < (long)RegionPinger.MaxMillisecondsPerPing)
				{
					this.<>2__current = new WaitForSecondsRealtime(0.01f);
					this.<>1__state = 2;
					return true;
				}
				goto IL_1E1;
				IL_1D4:
				if (!regionPinger.ping.Done())
				{
					goto IL_1A1;
				}
				IL_1E1:
				sw.Stop();
				int num4 = regionPinger.ping.Successful ? ((int)sw.ElapsedMilliseconds) : RegionPinger.MaxMillisecondsPerPing;
				regionPinger.rttResults.Add(num4);
				rttSum += num4;
				int num5 = replyCount;
				replyCount = num5 + 1;
				regionPinger.region.Ping = rttSum / replyCount;
				i = 4;
				IL_28F:
				if (regionPinger.ping.Done() || i <= 0)
				{
					this.<>2__current = new WaitForSeconds(0.1f);
					this.<>1__state = 4;
					return true;
				}
				num5 = i;
				i = num5 - 1;
				this.<>2__current = new WaitForSeconds(0.1f);
				this.<>1__state = 3;
				return true;
				IL_2D3:
				if (regionPinger.CurrentAttempt < RegionPinger.Attempts)
				{
					if (regionPinger.Aborted)
					{
						this.<>2__current = null;
						this.<>1__state = 1;
						return true;
					}
					goto IL_120;
				}
				IL_2E3:
				regionPinger.Done = true;
				regionPinger.ping.Dispose();
				if (regionPinger.rttResults.Count > 1 && replyCount > 0)
				{
					int num6 = regionPinger.rttResults.Min();
					int num7 = regionPinger.rttResults.Max();
					int num8 = rttSum - num7 + num6;
					regionPinger.region.Ping = num8 / replyCount;
				}
				regionPinger.onDoneCall(regionPinger.region);
				this.<>2__current = null;
				this.<>1__state = 5;
				return true;
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000BA8C File Offset: 0x00009C8C
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000233 RID: 563 RVA: 0x0000BA94 File Offset: 0x00009C94
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000234 RID: 564 RVA: 0x0000BA9B File Offset: 0x00009C9B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000227 RID: 551
			private int <>1__state;

			// Token: 0x04000228 RID: 552
			private object <>2__current;

			// Token: 0x04000229 RID: 553
			public RegionPinger <>4__this;

			// Token: 0x0400022A RID: 554
			private int <rttSum>5__2;

			// Token: 0x0400022B RID: 555
			private int <replyCount>5__3;

			// Token: 0x0400022C RID: 556
			private Stopwatch <sw>5__4;

			// Token: 0x0400022D RID: 557
			private int <i>5__5;
		}
	}
}
