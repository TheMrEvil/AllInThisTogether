using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class LibrarySecretRoom : MonoBehaviour
{
	// Token: 0x06000D28 RID: 3368 RVA: 0x000547EC File Offset: 0x000529EC
	private void Awake()
	{
		LibrarySecretRoom.instance = this;
		NetworkManager.OnPlayerJoinedGame = (Action<Player>)Delegate.Combine(NetworkManager.OnPlayerJoinedGame, new Action<Player>(this.OnPlayerJoinedGame));
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x00054814 File Offset: 0x00052A14
	private void Start()
	{
		LibraryManager libraryManager = LibraryManager.instance;
		libraryManager.OnLibraryEntered = (Action)Delegate.Combine(libraryManager.OnLibraryEntered, new Action(this.Reset));
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0005483C File Offset: 0x00052A3C
	private void Update()
	{
		if (PlayerControl.myInstance != null)
		{
			this.LookTarget.position = PlayerControl.myInstance.Display.CenterOfMass.position;
		}
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0005486A File Offset: 0x00052A6A
	public static bool CanMoveNextState(LibrarySecretRoom.PuzzleState state)
	{
		return !(LibrarySecretRoom.instance == null) && LibrarySecretRoom.instance.CurrentState != LibrarySecretRoom.PuzzleState.RoomOpened && LibrarySecretRoom.instance.CurrentState == state;
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x00054897 File Offset: 0x00052A97
	public void TryNextStage()
	{
		MapManager.instance.RequestNextSecretRoomStage(this.CurrentState);
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x000548AC File Offset: 0x00052AAC
	public void UpdateStage(LibrarySecretRoom.PuzzleState state, bool doTransition)
	{
		base.StopAllCoroutines();
		this.CurrentState = state;
		this.ActivateBird(state);
		if (state == LibrarySecretRoom.PuzzleState.RoomOpened)
		{
			this.CreateRewardScroll();
			if (!doTransition)
			{
				this.SecretDoor.localEulerAngles = new Vector3(0f, this.DoorRotation.Evaluate(1f), 0f);
			}
		}
		else
		{
			this.SecretDoor.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.ClearRewardScroll();
		}
		if (doTransition)
		{
			this.DoTransition(state);
		}
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x00054938 File Offset: 0x00052B38
	public void DoTransition(LibrarySecretRoom.PuzzleState state)
	{
		if (state == LibrarySecretRoom.PuzzleState.RoomOpened)
		{
			base.StartCoroutine(this.OpenDoor());
		}
		int num = state - LibrarySecretRoom.PuzzleState.Bird_1;
		if (num + 1 >= this.Birds.Count)
		{
			return;
		}
		this.BirdMove(this.Birds[num].transform, this.Birds[num + 1].transform);
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x00054998 File Offset: 0x00052B98
	private void ActivateBird(LibrarySecretRoom.PuzzleState state)
	{
		for (int i = 0; i < this.Birds.Count; i++)
		{
			if (i == (int)state)
			{
				GameObject bird = this.Birds[i];
				base.StartCoroutine(this.EnableBird(bird));
			}
			else
			{
				this.Birds[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x000549EE File Offset: 0x00052BEE
	private IEnumerator EnableBird(GameObject bird)
	{
		yield return new WaitForSeconds(2f);
		bird.SetActive(true);
		yield break;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x00054A00 File Offset: 0x00052C00
	private void BirdMove(Transform start, Transform end)
	{
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.IsWorld = true;
		effectProperties.SourceType = ActionSource.Vignette;
		EffectProperties effectProperties2 = effectProperties;
		PlayerControl myInstance = PlayerControl.myInstance;
		effectProperties2.AllyTarget = ((myInstance != null) ? myInstance.gameObject : null);
		effectProperties.StartLoc = global::Pose.WorldPoint(start.position, Vector3.up);
		effectProperties.OutLoc = global::Pose.WorldPoint(start.position, Vector3.up);
		effectProperties.SaveLocation("TargetPt", end.position);
		this.BirdMoveTree.Root.Apply(effectProperties);
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x00054A86 File Offset: 0x00052C86
	private IEnumerator OpenDoor()
	{
		this.SecretDoor.localEulerAngles = new Vector3(0f, 0f, 0f);
		yield return new WaitForSeconds(0.25f);
		AudioManager.PlaySFX2D(this.OpenedSFX, 1f, 0.1f);
		yield return new WaitForSeconds(0.5f);
		float t = 0f;
		this.StartFX.Play();
		this.OpenFX.Play();
		this.DoorSFX.Play();
		while (t < 1f)
		{
			t += Time.deltaTime / this.DoorOpenDuration;
			this.SecretDoor.localEulerAngles = new Vector3(0f, this.DoorRotation.Evaluate(t), 0f);
			yield return true;
		}
		this.CompleteFX.Play();
		yield break;
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x00054A98 File Offset: 0x00052C98
	private void CreateRewardScroll()
	{
		if (PlayerControl.myInstance != null && PlayerControl.myInstance.HasAugment(this.RewardScroll.ID, true))
		{
			return;
		}
		this.ClearRewardScroll();
		this.spawnedScroll = GoalManager.instance.CreateScroll(this.RewardScroll, this.RewardSpawnPt.position).gameObject;
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x00054AF7 File Offset: 0x00052CF7
	private void ClearRewardScroll()
	{
		if (this.spawnedScroll != null)
		{
			UnityEngine.Object.Destroy(this.spawnedScroll);
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x00054B12 File Offset: 0x00052D12
	public void Reset()
	{
		MapManager.instance.ResetSecretRoomState();
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x00054B1E File Offset: 0x00052D1E
	private void OnPlayerJoinedGame(Player plr)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			MapManager.instance.SyncSecretRoomState(plr, this.CurrentState);
		}
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x00054B38 File Offset: 0x00052D38
	private void OnDestroy()
	{
		NetworkManager.OnPlayerJoinedGame = (Action<Player>)Delegate.Remove(NetworkManager.OnPlayerJoinedGame, new Action<Player>(this.OnPlayerJoinedGame));
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x00054B5A File Offset: 0x00052D5A
	public LibrarySecretRoom()
	{
	}

	// Token: 0x04000AAE RID: 2734
	public static LibrarySecretRoom instance;

	// Token: 0x04000AAF RID: 2735
	public LibrarySecretRoom.PuzzleState CurrentState;

	// Token: 0x04000AB0 RID: 2736
	public Transform LookTarget;

	// Token: 0x04000AB1 RID: 2737
	public ActionTree BirdMoveTree;

	// Token: 0x04000AB2 RID: 2738
	public List<GameObject> Birds;

	// Token: 0x04000AB3 RID: 2739
	public AnimationCurve DoorRotation;

	// Token: 0x04000AB4 RID: 2740
	public float DoorOpenDuration = 1f;

	// Token: 0x04000AB5 RID: 2741
	public Transform SecretDoor;

	// Token: 0x04000AB6 RID: 2742
	public AudioClip OpenedSFX;

	// Token: 0x04000AB7 RID: 2743
	public AudioSource DoorSFX;

	// Token: 0x04000AB8 RID: 2744
	public ParticleSystem StartFX;

	// Token: 0x04000AB9 RID: 2745
	public ParticleSystem OpenFX;

	// Token: 0x04000ABA RID: 2746
	public ParticleSystem CompleteFX;

	// Token: 0x04000ABB RID: 2747
	public AugmentTree RewardScroll;

	// Token: 0x04000ABC RID: 2748
	public Transform RewardSpawnPt;

	// Token: 0x04000ABD RID: 2749
	private GameObject spawnedScroll;

	// Token: 0x0200051F RID: 1311
	public enum PuzzleState
	{
		// Token: 0x040025EC RID: 9708
		Start,
		// Token: 0x040025ED RID: 9709
		Bird_1,
		// Token: 0x040025EE RID: 9710
		Bird_2,
		// Token: 0x040025EF RID: 9711
		Bird_3,
		// Token: 0x040025F0 RID: 9712
		Bird_4,
		// Token: 0x040025F1 RID: 9713
		Bird_5,
		// Token: 0x040025F2 RID: 9714
		RoomOpened
	}

	// Token: 0x02000520 RID: 1312
	[CompilerGenerated]
	private sealed class <EnableBird>d__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023DA RID: 9178 RVA: 0x000CC231 File Offset: 0x000CA431
		[DebuggerHidden]
		public <EnableBird>d__24(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000CC240 File Offset: 0x000CA440
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000CC244 File Offset: 0x000CA444
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(2f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			bird.SetActive(true);
			return false;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000CC295 File Offset: 0x000CA495
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000CC29D File Offset: 0x000CA49D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000CC2A4 File Offset: 0x000CA4A4
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025F3 RID: 9715
		private int <>1__state;

		// Token: 0x040025F4 RID: 9716
		private object <>2__current;

		// Token: 0x040025F5 RID: 9717
		public GameObject bird;
	}

	// Token: 0x02000521 RID: 1313
	[CompilerGenerated]
	private sealed class <OpenDoor>d__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023E0 RID: 9184 RVA: 0x000CC2AC File Offset: 0x000CA4AC
		[DebuggerHidden]
		public <OpenDoor>d__26(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000CC2BB File Offset: 0x000CA4BB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000CC2C0 File Offset: 0x000CA4C0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibrarySecretRoom librarySecretRoom = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				librarySecretRoom.SecretDoor.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				AudioManager.PlaySFX2D(librarySecretRoom.OpenedSFX, 1f, 0.1f);
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				t = 0f;
				librarySecretRoom.StartFX.Play();
				librarySecretRoom.OpenFX.Play();
				librarySecretRoom.DoorSFX.Play();
				break;
			case 3:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				librarySecretRoom.CompleteFX.Play();
				return false;
			}
			t += Time.deltaTime / librarySecretRoom.DoorOpenDuration;
			librarySecretRoom.SecretDoor.localEulerAngles = new Vector3(0f, librarySecretRoom.DoorRotation.Evaluate(t), 0f);
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000CC415 File Offset: 0x000CA615
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000CC41D File Offset: 0x000CA61D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000CC424 File Offset: 0x000CA624
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025F6 RID: 9718
		private int <>1__state;

		// Token: 0x040025F7 RID: 9719
		private object <>2__current;

		// Token: 0x040025F8 RID: 9720
		public LibrarySecretRoom <>4__this;

		// Token: 0x040025F9 RID: 9721
		private float <t>5__2;
	}
}
