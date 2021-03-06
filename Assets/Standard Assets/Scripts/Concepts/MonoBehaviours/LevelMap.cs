﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Extensions;

namespace AmbitiousSnake
{
	public class LevelMap : SingletonMonoBehaviour<LevelMap>
	{
		public static string previousLevelName;
		public static Rect boundsRect;
		public Transform trs;
		public Camera cam;
		public Transform rotationViewersParent;
		
		public override void Awake ()
		{
			base.Awake ();
			if (string.IsNullOrEmpty(previousLevelName))
				previousLevelName = SceneManager.GetSceneByBuildIndex(LevelSelect.firstLevelBuildIndex).name;
		}

		public virtual void Make (string levelName)
		{
			StartCoroutine(MakeRoutine (levelName));
		}
		
		public virtual IEnumerator MakeRoutine (string levelName)
		{
			while (rotationViewersParent.childCount > 0)
				DestroyImmediate(rotationViewersParent.GetChild(0).gameObject);
			if (!string.IsNullOrEmpty(previousLevelName))
				GameManager.Instance.UnloadLevelAsync (previousLevelName);
			GameManager.Instance.SetPaused (true);
			GameManager.Instance.SetTimeScale (1);
			AsyncOperation loadLevel = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
			while (!loadLevel.isDone)
				yield return new WaitForEndOfFrame();
			if (!string.IsNullOrEmpty(previousLevelName) && previousLevelName != levelName)
				GameManager.Instance.UnloadLevelAsync (previousLevelName);
			previousLevelName = levelName;
			Bounds levelBounds = GetBounds();
			float previousZPos = trs.position.z;
			trs.position = levelBounds.center.SetZ(previousZPos);
			cam.orthographicSize = Mathf.Max(levelBounds.size.x, levelBounds.size.y) / 2;
		}
		
		public virtual void Make (Renderer[] renderers)
		{
			while (rotationViewersParent.childCount > 0)
				DestroyImmediate(rotationViewersParent.GetChild(0).gameObject);
			float previousZPos = trs.position.z;
			Bounds levelBounds = GetBounds();
			trs.position = (Vector2) levelBounds.center;
			trs.position += Vector3.forward * previousZPos;
			cam.orthographicSize = Mathf.Max(levelBounds.size.x, levelBounds.size.y) / 2;
		}
		
		public static Bounds GetBounds ()
		{
			List<Bounds> levelBoundsInstances = new List<Bounds>();
			foreach (Renderer renderer in FindObjectsOfType<Renderer>())
			{
				if (renderer.GetComponent<OmitFromLevelMap>() == null)
					levelBoundsInstances.Add(renderer.bounds);
			}
			return BoundsExtensions.Combine(levelBoundsInstances.ToArray());
		}
		
		public static Rect GetBoundsRect ()
		{
			List<Rect> levelBoundRectsInstances = new List<Rect>();
			foreach (Renderer renderer in FindObjectsOfType<Renderer>())
			{
				if (renderer.GetComponent<OmitFromLevelMap>() == null)
					levelBoundRectsInstances.Add(renderer.bounds.ToRect());
			}
			return RectExtensions.Combine(levelBoundRectsInstances.ToArray());
		}
	}
}