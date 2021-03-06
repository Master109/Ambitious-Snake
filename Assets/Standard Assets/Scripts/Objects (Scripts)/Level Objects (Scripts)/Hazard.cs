﻿using UnityEngine;

namespace AmbitiousSnake
{
	public class Hazard : NotPartOfLevelEditor
	{
		public void OnCollisionEnter2D (Collision2D coll)
		{
			if ((coll == null || coll.transform.root == Snake.instance.trs) && !GameManager.isInSceneTransition)
				Level.instance.Restart ();
		}
		
		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.transform.root == Snake.instance.trs && !GameManager.isInSceneTransition)
				Level.instance.Restart ();
		}
	}
}