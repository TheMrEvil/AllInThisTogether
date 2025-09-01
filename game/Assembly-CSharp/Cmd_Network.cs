using System;
using Photon.Pun;
using Photon.Realtime;
using QFSW.QC;

// Token: 0x02000039 RID: 57
[CommandPrefix("net.")]
public class Cmd_Network
{
	// Token: 0x060001C0 RID: 448 RVA: 0x000105E3 File Offset: 0x0000E7E3
	[Command("stats", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle through different network stats info")]
	private static string Stats()
	{
		NetStats.NextMode();
		return "Network Stats: " + NetStats.Mode.ToString();
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00010604 File Offset: 0x0000E804
	[Command("simulate", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle Network stats UI")]
	private static string Simulate()
	{
		Cmd_Network.SimEnabled = !Cmd_Network.SimEnabled;
		PhotonNetwork.NetworkingClient.LoadBalancingPeer.IsSimulationEnabled = Cmd_Network.SimEnabled;
		return "Network Simulation Mode: " + (Cmd_Network.SimEnabled ? "On" : "Off");
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00010644 File Offset: 0x0000E844
	[Command("commandeer", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Transfers ownership of the room to yourself")]
	private static string Commandeer()
	{
		if (!PhotonNetwork.InRoom || PhotonNetwork.OfflineMode)
		{
			return "Need to be in a multiplayer room";
		}
		if (PhotonNetwork.IsMasterClient)
		{
			return "Already MasterClient";
		}
		if (PlayerControl.myInstance == null)
		{
			return "Need to be fully connected";
		}
		if (!PhotonNetwork.CurrentRoom.SetMasterClient(PlayerControl.myInstance.view.Owner))
		{
			return "Failed to set self as Master Client";
		}
		return "Set Self to MasterClient ";
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x000106AB File Offset: 0x0000E8AB
	[Command("close", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Close down the current room")]
	public static string CloseRoom()
	{
		if (!PhotonNetwork.InRoom || PhotonNetwork.OfflineMode)
		{
			return "Need to be in a multiplayer room";
		}
		PhotonNetwork.CurrentRoom.IsOpen = false;
		return "Room Closed";
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000106D4 File Offset: 0x0000E8D4
	[Command("lag", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Sets a simulated latency in ms (Range: 0-1000)")]
	private static string Lag(int ms)
	{
		if (!Cmd_Network.SimEnabled)
		{
			return "Can't change latency while Simulation is disabled. Use net.simulate to enable network simulation";
		}
		if (ms < 0 || ms > 1000)
		{
			return "Out of Range: Acceptable latency is 0ms - 1000ms";
		}
		LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
		loadBalancingPeer.NetworkSimulationSettings.IncomingLag = ms;
		loadBalancingPeer.NetworkSimulationSettings.OutgoingLag = ms;
		return "Network Latency Simulation: " + ms.ToString() + "ms";
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00010738 File Offset: 0x0000E938
	[Command("jitter", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds randomness to incomming latency ms (Range: 0 - 100)")]
	private static string Jitter(int ms)
	{
		if (!Cmd_Network.SimEnabled)
		{
			return "Can't change latency while Simulation is disabled. Use net.simulate to enable network simulation";
		}
		if (ms < 0 || ms > 100)
		{
			return "Out of Range: Acceptable jitter is 0ms - 100ms";
		}
		LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
		loadBalancingPeer.NetworkSimulationSettings.IncomingJitter = ms;
		loadBalancingPeer.NetworkSimulationSettings.OutgoingJitter = ms;
		return "Network Jitter Simulation: " + ms.ToString() + "ms";
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00010798 File Offset: 0x0000E998
	[Command("loss", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Sets an incoming packet loss % (Internet Avg: 2% - Range 0-10)")]
	private static string Loss(int percent)
	{
		if (!Cmd_Network.SimEnabled)
		{
			return "Can't change latency while Simulation is disabled. Use net.simulate to enable network simulation";
		}
		if (percent < 0 || percent > 10)
		{
			return "Out of Range: Acceptable loss is 0% - 10%";
		}
		LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
		loadBalancingPeer.NetworkSimulationSettings.IncomingLossPercentage = percent;
		loadBalancingPeer.NetworkSimulationSettings.OutgoingLossPercentage = percent;
		return "Network Loss Simulation: " + percent.ToString() + "%";
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x000107F8 File Offset: 0x0000E9F8
	public Cmd_Network()
	{
	}

	// Token: 0x040001E4 RID: 484
	public static bool SimEnabled;
}
