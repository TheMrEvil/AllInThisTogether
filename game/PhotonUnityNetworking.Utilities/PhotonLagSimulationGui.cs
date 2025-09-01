using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Pun.UtilityScripts
{
	// Token: 0x02000002 RID: 2
	public class PhotonLagSimulationGui : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public PhotonPeer Peer
		{
			[CompilerGenerated]
			get
			{
				return this.<Peer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Peer>k__BackingField = value;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public void Start()
		{
			this.Peer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public void OnGUI()
		{
			if (!this.Visible)
			{
				return;
			}
			if (this.Peer == null)
			{
				this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction(this.NetSimHasNoPeerWindow), "Netw. Sim.", Array.Empty<GUILayoutOption>());
				return;
			}
			this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction(this.NetSimWindow), "Netw. Sim.", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020ED File Offset: 0x000002ED
		private void NetSimHasNoPeerWindow(int windowId)
		{
			GUILayout.Label("No peer to communicate with. ", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		private void NetSimWindow(int windowId)
		{
			GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", this.Peer.RoundTripTime, this.Peer.RoundTripTimeVariance), Array.Empty<GUILayoutOption>());
			bool isSimulationEnabled = this.Peer.IsSimulationEnabled;
			bool flag = GUILayout.Toggle(isSimulationEnabled, "Simulate", Array.Empty<GUILayoutOption>());
			if (flag != isSimulationEnabled)
			{
				this.Peer.IsSimulationEnabled = flag;
			}
			float num = (float)this.Peer.NetworkSimulationSettings.IncomingLag;
			GUILayout.Label("Lag " + num.ToString(), Array.Empty<GUILayoutOption>());
			num = GUILayout.HorizontalSlider(num, 0f, 500f, Array.Empty<GUILayoutOption>());
			this.Peer.NetworkSimulationSettings.IncomingLag = (int)num;
			this.Peer.NetworkSimulationSettings.OutgoingLag = (int)num;
			float num2 = (float)this.Peer.NetworkSimulationSettings.IncomingJitter;
			GUILayout.Label("Jit " + num2.ToString(), Array.Empty<GUILayoutOption>());
			num2 = GUILayout.HorizontalSlider(num2, 0f, 100f, Array.Empty<GUILayoutOption>());
			this.Peer.NetworkSimulationSettings.IncomingJitter = (int)num2;
			this.Peer.NetworkSimulationSettings.OutgoingJitter = (int)num2;
			float num3 = (float)this.Peer.NetworkSimulationSettings.IncomingLossPercentage;
			GUILayout.Label("Loss " + num3.ToString(), Array.Empty<GUILayoutOption>());
			num3 = GUILayout.HorizontalSlider(num3, 0f, 10f, Array.Empty<GUILayoutOption>());
			this.Peer.NetworkSimulationSettings.IncomingLossPercentage = (int)num3;
			this.Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int)num3;
			if (GUI.changed)
			{
				this.WindowRect.height = 100f;
			}
			GUI.DragWindow();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022C4 File Offset: 0x000004C4
		public PhotonLagSimulationGui()
		{
		}

		// Token: 0x04000001 RID: 1
		public Rect WindowRect = new Rect(0f, 100f, 120f, 100f);

		// Token: 0x04000002 RID: 2
		public int WindowId = 101;

		// Token: 0x04000003 RID: 3
		public bool Visible = true;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		private PhotonPeer <Peer>k__BackingField;
	}
}
