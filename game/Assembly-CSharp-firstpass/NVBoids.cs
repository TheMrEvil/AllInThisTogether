using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000003 RID: 3
[HelpURL("https://nvjob.github.io/unity/nvjob-boids")]
public class NVBoids : MonoBehaviour
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002058 File Offset: 0x00000258
	private void OnEnable()
	{
		this.thisTransform = base.transform;
		this.CreateFlock();
		this.CreateBird();
		base.StartCoroutine(this.BehavioralChange());
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000207F File Offset: 0x0000027F
	private void LateUpdate()
	{
		this.FlocksMove();
		this.BirdsMove();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002090 File Offset: 0x00000290
	private void FlocksMove()
	{
		for (int i = 0; i < this.flockNum; i++)
		{
			this.flocksTransform[i].localPosition = Vector3.SmoothDamp(this.flocksTransform[i].localPosition, this.flockPos[i], ref this.velFlocks[i], this.smoothChFrequency);
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000020EC File Offset: 0x000002EC
	private void BirdsMove()
	{
		float deltaTime = Time.deltaTime;
		this.timeTime += deltaTime;
		Vector3 a = Vector3.forward * this.birdSpeed * deltaTime;
		Vector3 b = Vector3.up * (this.verticalWawe * 0.5f - Mathf.PingPong(this.timeTime * 0.5f, this.verticalWawe));
		float maxRadiansDelta = this.soaring * deltaTime;
		for (int i = 0; i < this.BirdRefs.Length; i++)
		{
			for (int j = 0; j < this.birdLocations[i].Length; j++)
			{
				if (this.birdsSpeedCur[i][j] != this.birdsSpeed[i][j])
				{
					this.birdsSpeedCur[i][j] = Mathf.SmoothDamp(this.birdsSpeedCur[i][j], this.birdsSpeed[i][j], ref this.spVelocity[i][j], 0.5f);
				}
				ValueTuple<Vector3, Quaternion, float> valueTuple = this.birdLocations[i][j];
				Vector3 vector = valueTuple.Item1 + valueTuple.Item2 * (a * this.birdsSpeed[i][j]);
				Vector3 target = this.flocksTransform[this.curentFlock[i][j]].position + this.rdTargetPos[i][j] + b - vector;
				Quaternion quaternion = Quaternion.LookRotation(Vector3.RotateTowards(valueTuple.Item2 * Vector3.forward, target, maxRadiansDelta, 0f));
				this.birdLocations[i][j] = new ValueTuple<Vector3, Quaternion, float>(vector, quaternion, valueTuple.Item3);
				this.birdMatrix[i][j] = Matrix4x4.TRS(vector, quaternion, Vector3.one * valueTuple.Item3);
			}
		}
		for (int k = 0; k < this.BirdRefs.Length; k++)
		{
			Graphics.DrawMeshInstanced(this.BirdRefs[k].mesh, 0, this.BirdRefs[k].mat, this.birdMatrix[k]);
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002324 File Offset: 0x00000524
	private IEnumerator BehavioralChange()
	{
		for (;;)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(this.behavioralCh.x, this.behavioralCh.y));
			for (int i = 0; i < this.flockNum; i++)
			{
				if (UnityEngine.Random.value < this.posChangeFrequency)
				{
					Vector3 vector = UnityEngine.Random.insideUnitSphere * (float)this.fragmentedFlock;
					this.flockPos[i] = new Vector3(vector.x, Mathf.Abs(vector.y * this.fragmentedFlockYLimit), vector.z);
				}
			}
			for (int j = 0; j < this.BirdRefs.Length; j++)
			{
				for (int k = 0; k < this.BirdRefs[j].Count; k++)
				{
					this.birdsSpeed[j][k] = UnityEngine.Random.Range(3f, 7f);
					Vector3 vector2 = UnityEngine.Random.insideUnitSphere * (float)this.fragmentedBirds;
					this.rdTargetPos[j][k] = new Vector3(vector2.x, vector2.y * this.fragmentedBirdsYLimit, vector2.z);
					if (UnityEngine.Random.value < this.migrationFrequency)
					{
						this.curentFlock[j][k] = UnityEngine.Random.Range(0, this.flockNum);
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002334 File Offset: 0x00000534
	private void CreateFlock()
	{
		this.flocksTransform = new Transform[this.flockNum];
		this.flockPos = new Vector3[this.flockNum];
		this.velFlocks = new Vector3[this.flockNum];
		this.curentFlock = new int[this.BirdRefs.Length][];
		for (int i = 0; i < this.flockNum; i++)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject.SetActive(this.debug);
			this.flocksTransform[i] = gameObject.transform;
			Vector3 vector = UnityEngine.Random.onUnitSphere * (float)this.fragmentedFlock;
			this.flocksTransform[i].position = this.thisTransform.position;
			this.flockPos[i] = new Vector3(vector.x, Mathf.Abs(vector.y * this.fragmentedFlockYLimit), vector.z);
			this.flocksTransform[i].parent = this.thisTransform;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000242C File Offset: 0x0000062C
	private void CreateBird()
	{
		this.birdLocations = new ValueTuple<Vector3, Quaternion, float>[this.BirdRefs.Length][];
		this.birdMatrix = new Matrix4x4[this.BirdRefs.Length][];
		this.birdsSpeed = new float[this.BirdRefs.Length][];
		this.birdsSpeedCur = new float[this.BirdRefs.Length][];
		this.rdTargetPos = new Vector3[this.BirdRefs.Length][];
		this.spVelocity = new float[this.BirdRefs.Length][];
		for (int i = 0; i < this.BirdRefs.Length; i++)
		{
			this.birdLocations[i] = new ValueTuple<Vector3, Quaternion, float>[this.BirdRefs[i].Count];
			this.rdTargetPos[i] = new Vector3[this.BirdRefs[i].Count];
			this.birdsSpeed[i] = new float[this.BirdRefs[i].Count];
			this.birdsSpeedCur[i] = new float[this.BirdRefs[i].Count];
			this.curentFlock[i] = new int[this.BirdRefs[i].Count];
			this.spVelocity[i] = new float[this.BirdRefs[i].Count];
			this.birdMatrix[i] = new Matrix4x4[this.BirdRefs[i].Count];
			for (int j = 0; j < this.birdLocations[i].Length; j++)
			{
				this.birdLocations[i][j] = new ValueTuple<Vector3, Quaternion, float>(Vector3.one, Quaternion.identity, 1f);
				Vector3 vector = UnityEngine.Random.insideUnitSphere * (float)this.fragmentedBirds;
				this.birdLocations[i][j].Item1 = (this.rdTargetPos[i][j] = new Vector3(vector.x, vector.y * this.fragmentedBirdsYLimit, vector.z) + base.transform.localPosition);
				this.birdLocations[i][j].Item3 = UnityEngine.Random.Range(this.scaleRandom.x, this.scaleRandom.y);
				this.birdLocations[i][j].Item2 = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f);
				this.curentFlock[i][j] = UnityEngine.Random.Range(0, this.flockNum);
				this.birdsSpeed[i][j] = UnityEngine.Random.Range(3f, 7f);
			}
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000026C4 File Offset: 0x000008C4
	public NVBoids()
	{
	}

	// Token: 0x04000001 RID: 1
	[Header("General Settings")]
	public Vector2 behavioralCh = new Vector2(2f, 6f);

	// Token: 0x04000002 RID: 2
	public bool debug;

	// Token: 0x04000003 RID: 3
	[Header("Flock Settings")]
	[Range(1f, 150f)]
	public int flockNum = 2;

	// Token: 0x04000004 RID: 4
	[Range(0f, 5000f)]
	public int fragmentedFlock = 30;

	// Token: 0x04000005 RID: 5
	[Range(0f, 1f)]
	public float fragmentedFlockYLimit = 0.5f;

	// Token: 0x04000006 RID: 6
	[Range(0f, 1f)]
	public float migrationFrequency = 0.1f;

	// Token: 0x04000007 RID: 7
	[Range(0f, 1f)]
	public float posChangeFrequency = 0.5f;

	// Token: 0x04000008 RID: 8
	[Range(0f, 100f)]
	public float smoothChFrequency = 0.5f;

	// Token: 0x04000009 RID: 9
	[Header("Bird Settings")]
	public NVBoids.BirdRender[] BirdRefs;

	// Token: 0x0400000A RID: 10
	[Range(0f, 150f)]
	public float birdSpeed = 1f;

	// Token: 0x0400000B RID: 11
	[Range(0f, 100f)]
	public int fragmentedBirds = 10;

	// Token: 0x0400000C RID: 12
	[Range(0f, 1f)]
	public float fragmentedBirdsYLimit = 1f;

	// Token: 0x0400000D RID: 13
	[Range(0f, 10f)]
	public float soaring = 0.5f;

	// Token: 0x0400000E RID: 14
	[Range(0.01f, 500f)]
	public float verticalWawe = 20f;

	// Token: 0x0400000F RID: 15
	public Vector2 scaleRandom = new Vector2(1f, 1.5f);

	// Token: 0x04000010 RID: 16
	[Header("Information")]
	public string HelpURL = "nvjob.github.io/unity/nvjob-boids";

	// Token: 0x04000011 RID: 17
	public string ReportAProblem = "nvjob.github.io/support";

	// Token: 0x04000012 RID: 18
	public string Patrons = "nvjob.github.io/patrons";

	// Token: 0x04000013 RID: 19
	private Transform thisTransform;

	// Token: 0x04000014 RID: 20
	[TupleElementNames(new string[]
	{
		"pos",
		"rot",
		"scale"
	})]
	private ValueTuple<Vector3, Quaternion, float>[][] birdLocations;

	// Token: 0x04000015 RID: 21
	private Matrix4x4[][] birdMatrix;

	// Token: 0x04000016 RID: 22
	private Transform[] flocksTransform;

	// Token: 0x04000017 RID: 23
	private Vector3[] flockPos;

	// Token: 0x04000018 RID: 24
	private Vector3[] velFlocks;

	// Token: 0x04000019 RID: 25
	private Vector3[][] rdTargetPos;

	// Token: 0x0400001A RID: 26
	private float[][] birdsSpeed;

	// Token: 0x0400001B RID: 27
	private float[][] birdsSpeedCur;

	// Token: 0x0400001C RID: 28
	private float[][] spVelocity;

	// Token: 0x0400001D RID: 29
	private int[][] curentFlock;

	// Token: 0x0400001E RID: 30
	private float timeTime;

	// Token: 0x0400001F RID: 31
	private static WaitForSeconds delay0;

	// Token: 0x02000175 RID: 373
	[Serializable]
	public struct BirdRender
	{
		// Token: 0x04000BE8 RID: 3048
		public Mesh mesh;

		// Token: 0x04000BE9 RID: 3049
		public Material mat;

		// Token: 0x04000BEA RID: 3050
		[Range(0f, 1023f)]
		public int Count;
	}

	// Token: 0x02000176 RID: 374
	[CompilerGenerated]
	private sealed class <BehavioralChange>d__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x0005E306 File Offset: 0x0005C506
		[DebuggerHidden]
		public <BehavioralChange>d__35(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0005E315 File Offset: 0x0005C515
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0005E318 File Offset: 0x0005C518
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			NVBoids nvboids = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				for (int i = 0; i < nvboids.flockNum; i++)
				{
					if (UnityEngine.Random.value < nvboids.posChangeFrequency)
					{
						Vector3 vector = UnityEngine.Random.insideUnitSphere * (float)nvboids.fragmentedFlock;
						nvboids.flockPos[i] = new Vector3(vector.x, Mathf.Abs(vector.y * nvboids.fragmentedFlockYLimit), vector.z);
					}
				}
				for (int j = 0; j < nvboids.BirdRefs.Length; j++)
				{
					for (int k = 0; k < nvboids.BirdRefs[j].Count; k++)
					{
						nvboids.birdsSpeed[j][k] = UnityEngine.Random.Range(3f, 7f);
						Vector3 vector2 = UnityEngine.Random.insideUnitSphere * (float)nvboids.fragmentedBirds;
						nvboids.rdTargetPos[j][k] = new Vector3(vector2.x, vector2.y * nvboids.fragmentedBirdsYLimit, vector2.z);
						if (UnityEngine.Random.value < nvboids.migrationFrequency)
						{
							nvboids.curentFlock[j][k] = UnityEngine.Random.Range(0, nvboids.flockNum);
						}
					}
				}
			}
			else
			{
				this.<>1__state = -1;
			}
			this.<>2__current = new WaitForSeconds(UnityEngine.Random.Range(nvboids.behavioralCh.x, nvboids.behavioralCh.y));
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0005E4A4 File Offset: 0x0005C6A4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0005E4AC File Offset: 0x0005C6AC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0005E4B3 File Offset: 0x0005C6B3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000BEB RID: 3051
		private int <>1__state;

		// Token: 0x04000BEC RID: 3052
		private object <>2__current;

		// Token: 0x04000BED RID: 3053
		public NVBoids <>4__this;
	}
}
