using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class NetStats : MonoBehaviour
{
	// Token: 0x06001637 RID: 5687 RVA: 0x0008C4C8 File Offset: 0x0008A6C8
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		NetStats.Mode = NetStats.StatsMode.Off;
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x0008C4DC File Offset: 0x0008A6DC
	public static void NextMode()
	{
		int num = (int)NetStats.Mode;
		num++;
		if (num > 3)
		{
			num = 0;
		}
		NetStats.Mode = (NetStats.StatsMode)num;
		if (NetStats.Mode == NetStats.StatsMode.Core)
		{
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsReset();
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsEnabled = true;
		}
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x0008C528 File Offset: 0x0008A728
	private void Update()
	{
		this.canvasGroup.UpdateOpacity(NetStats.Mode > NetStats.StatsMode.Off, 2f, false);
		if (this.canvasGroup.alpha <= 0f)
		{
			return;
		}
		TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsGameLevel;
		long num = PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsElapsedMs / 1000L;
		if (num == 0L)
		{
			num = 1L;
		}
		string text = "";
		switch (NetStats.Mode)
		{
		case NetStats.StatsMode.Core:
		{
			string text2 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount, trafficStatsGameLevel.TotalIncomingMessageCount, trafficStatsGameLevel.TotalMessageCount);
			string text3 = string.Format("{0}sec average:", num);
			string text4 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (long)trafficStatsGameLevel.TotalOutgoingMessageCount / num, (long)trafficStatsGameLevel.TotalIncomingMessageCount / num, (long)trafficStatsGameLevel.TotalMessageCount / num);
			text = string.Concat(new string[]
			{
				text2,
				"\n",
				text3,
				"\n",
				text4
			});
			break;
		}
		case NetStats.StatsMode.Traffic:
		{
			string str = "Incoming: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsIncoming.ToString();
			string str2 = "Outgoing: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsOutgoing.ToString();
			text = str + "\n" + str2;
			break;
		}
		case NetStats.StatsMode.Health:
			text = string.Format("ping: {6}[+/-{7}]ms resent:{8} \n\nmax ms between\nsend: {0,4} \ndispatch: {1,4} \n\nlongest dispatch for: \nev({3}):{2,3}ms \nop({5}):{4,3}ms", new object[]
			{
				trafficStatsGameLevel.LongestDeltaBetweenSending,
				trafficStatsGameLevel.LongestDeltaBetweenDispatching,
				trafficStatsGameLevel.LongestEventCallback,
				trafficStatsGameLevel.LongestEventCallbackCode,
				trafficStatsGameLevel.LongestOpResponseCallback,
				trafficStatsGameLevel.LongestOpResponseCallbackOpCode,
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.RoundTripTime,
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.RoundTripTimeVariance,
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.ResentReliableCommands
			});
			break;
		}
		this.m_Text.text = text;
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x0008C75F File Offset: 0x0008A95F
	public NetStats()
	{
	}

	// Token: 0x040015D3 RID: 5587
	private CanvasGroup canvasGroup;

	// Token: 0x040015D4 RID: 5588
	public static NetStats.StatsMode Mode;

	// Token: 0x040015D5 RID: 5589
	public TextMeshProUGUI m_Text;

	// Token: 0x020005F3 RID: 1523
	public enum StatsMode
	{
		// Token: 0x0400294C RID: 10572
		Off,
		// Token: 0x0400294D RID: 10573
		Core,
		// Token: 0x0400294E RID: 10574
		Traffic,
		// Token: 0x0400294F RID: 10575
		Health
	}
}
