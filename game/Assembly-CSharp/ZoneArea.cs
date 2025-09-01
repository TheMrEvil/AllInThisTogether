using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class ZoneArea : Indicatable, IPunObservable
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060009AF RID: 2479 RVA: 0x00040840 File Offset: 0x0003EA40
	public override Transform Root
	{
		get
		{
			return this.IndicatorLocation;
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00040848 File Offset: 0x0003EA48
	public int PlayersInZone
	{
		get
		{
			return this.PlayersInside.Count;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00040855 File Offset: 0x0003EA55
	public int EnemiesInZone
	{
		get
		{
			return this.EnemiesInside.Count;
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00040864 File Offset: 0x0003EA64
	private void Awake()
	{
		ZoneManager.Zones.Add(this);
		this.view = base.GetComponent<PhotonView>();
		this.CaptureTime = 30f;
		this.projMat = new Material(this.ProgressFill.material.shader);
		this.projMat.CopyPropertiesFromMaterial(this.ProgressFill.material);
		this.ProgressFill.material = this.projMat;
		this.ProgressFill.orthographicSize = 0.01f;
		this.zoneMat = this.Cylinder.GetComponent<MeshRenderer>().material;
		this.zoneMat.SetFloat("_DissolveAmount", 1f);
		WorldIndicators.Indicate(this);
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00040916 File Offset: 0x0003EB16
	public void Setup(ZoneProperties props)
	{
		this.Radius = props.Radius;
		this.CaptureTime = props.CaptureTime;
		this.Progress = 0f;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0004093C File Offset: 0x0003EB3C
	private void Update()
	{
		this.Cylinder.localScale = new Vector3(this.Radius, this.Height, this.Radius);
		if (this.fadeIn < 1f)
		{
			this.fadeIn += Time.deltaTime;
			this.zoneMat.SetFloat("_DissolveAmount", 1f - this.fadeIn);
		}
		if (this.IsCaptured)
		{
			this.DissolveAway();
			return;
		}
		this.CheckInside();
		if (this.PlayersInZone > 0)
		{
			this.Progress += Time.deltaTime / this.CaptureTime;
		}
		this.Progress = Mathf.Clamp(this.Progress, 0f, 1f);
		this.ProgressFill.orthographicSize = Mathf.Lerp(this.ProgressFill.orthographicSize, Mathf.Max(0.01f, this.Radius * this.Progress * this.projFudge), Time.deltaTime * 4f);
		if (this.Progress >= 1f && !this.IsCaptured)
		{
			this.TryCapture(false);
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00040A58 File Offset: 0x0003EC58
	public void TryCapture(bool immediate)
	{
		if (!PhotonNetwork.IsMasterClient || this.IsCaptured)
		{
			return;
		}
		this.view.RPC("CaptureNetwork", RpcTarget.All, new object[]
		{
			immediate
		});
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00040A8A File Offset: 0x0003EC8A
	[PunRPC]
	private void CaptureNetwork(bool immediate)
	{
		this.IsCaptured = true;
		WorldIndicators.ReleaseIndicator(this);
		if (!immediate)
		{
			AudioManager.PlaySFX2D(this.ZoneCapturedClips.GetRandomClip(-1), 1f, 0.1f);
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00040AB8 File Offset: 0x0003ECB8
	private void CheckInside()
	{
		if (PlayerControl.AllPlayers == null)
		{
			return;
		}
		this.PlayersInside.Clear();
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!playerControl.IsDead && (this.PointInside(playerControl.display.CenterOfMass.position) || this.PointInside(playerControl.display.GetLocation(ActionLocation.Floor).position)))
			{
				this.PlayersInside.Add(playerControl);
			}
		}
		this.EnemiesInside.Clear();
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.IsDead && (this.PointInside(entityControl.display.CenterOfMass.position) || this.PointInside(entityControl.display.GetLocation(ActionLocation.Floor).position)))
			{
				this.EnemiesInside.Add(entityControl);
			}
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00040BE8 File Offset: 0x0003EDE8
	private bool PointInside(Vector3 point)
	{
		Vector3 cylTop = base.transform.position - base.transform.up * 0.25f;
		Vector3 cylBottom = base.transform.position + base.transform.up * this.Height;
		return AreaOfEffect.PointInCylinder(point, cylTop, cylBottom, this.Radius);
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00040C50 File Offset: 0x0003EE50
	private void DissolveAway()
	{
		this.DissolveAmount += Time.deltaTime;
		this.DissolveAmount = Mathf.Clamp(this.DissolveAmount, 0f, 1f);
		this.projMat.SetFloat("_DissolveAmount", this.DissolveAmount);
		this.zoneMat.SetFloat("_DissolveAmount", this.DissolveAmount);
		if (this.DissolveAmount >= 1f)
		{
			this.RemoveZone();
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00040CC9 File Offset: 0x0003EEC9
	public void RemoveZone()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (this.removed)
		{
			return;
		}
		this.removed = true;
		PhotonNetwork.Destroy(this.view);
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00040CEE File Offset: 0x0003EEEE
	private void OnDestroy()
	{
		ZoneManager.Zones.Remove(this);
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00040D04 File Offset: 0x0003EF04
	public override bool ShouldIndicate()
	{
		return !(PlayerControl.myInstance == null) && !this.IsCaptured && Vector3.Distance(PlayerControl.myInstance.display.CenterOfMass.position, base.transform.position) > this.Radius + 10f;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00040D5C File Offset: 0x0003EF5C
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(this.Radius);
			stream.SendNext(this.CaptureTime);
			stream.SendNext(this.Progress);
			return;
		}
		base.transform.position = (Vector3)stream.ReceiveNext();
		this.Radius = (float)stream.ReceiveNext();
		this.CaptureTime = (float)stream.ReceiveNext();
		this.Progress = (float)stream.ReceiveNext();
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00040E04 File Offset: 0x0003F004
	public ZoneArea()
	{
	}

	// Token: 0x0400080A RID: 2058
	private PhotonView view;

	// Token: 0x0400080B RID: 2059
	[Space(15f)]
	public Transform Cylinder;

	// Token: 0x0400080C RID: 2060
	public Transform IndicatorLocation;

	// Token: 0x0400080D RID: 2061
	public Projector ProgressFill;

	// Token: 0x0400080E RID: 2062
	public List<AudioClip> ZoneCapturedClips;

	// Token: 0x0400080F RID: 2063
	[Header("Runtime")]
	public float Progress;

	// Token: 0x04000810 RID: 2064
	public float Radius = 5f;

	// Token: 0x04000811 RID: 2065
	public float CaptureTime = 45f;

	// Token: 0x04000812 RID: 2066
	public bool IsCaptured;

	// Token: 0x04000813 RID: 2067
	private List<PlayerControl> PlayersInside = new List<PlayerControl>();

	// Token: 0x04000814 RID: 2068
	private List<EntityControl> EnemiesInside = new List<EntityControl>();

	// Token: 0x04000815 RID: 2069
	private float Height = 8f;

	// Token: 0x04000816 RID: 2070
	private float projFudge = 1.04f;

	// Token: 0x04000817 RID: 2071
	private Material zoneMat;

	// Token: 0x04000818 RID: 2072
	private Material projMat;

	// Token: 0x04000819 RID: 2073
	private float fadeIn;

	// Token: 0x0400081A RID: 2074
	private float DissolveAmount;

	// Token: 0x0400081B RID: 2075
	private bool removed;
}
