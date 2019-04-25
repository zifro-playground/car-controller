using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine.TestTools;
using ZifroPlaygroundTests;
using ZifroPlaygroundTests.PlayMode;
using PM;
using UnityEngine;

namespace CarController.Tests.PlayMode
{
	public class PlayThroughSample : PlayThroughLevelsTests
	{
		static CaseTestData[] GetActiveCases()
		{
			return PlaygroundTestHelper.GetActiveCases("car-controller");
		}

		static LevelTestData[] GetActiveLevels()
		{
			return PlaygroundTestHelper.GetActiveLevels("car-controller");
		}

		protected override string testingScenePath => "Assets/CarController.Tests.PlayMode/MainSceneForTesting.unity";

		[UnityTest]
		[Timeout(120_000)] // ms to complete whole game
		public override IEnumerator TestPlayWholeGame()
		{
			return TestPlayWholeGame(GetActiveLevels());
		}

		[UnityTest]
		public override IEnumerator TestPlayGuidesInLevel([ValueSource(nameof(GetActiveLevels))] LevelTestData data)
		{
			return base.TestPlayGuidesInLevel(data);
		}
		
		[UnityTest]
		public override IEnumerator TestPlayLevel([ValueSource(nameof(GetActiveLevels))] LevelTestData data)
		{
			return base.TestPlayLevel(data);
		}

		protected override IEnumerator PostSceneLoad()
		{
			var playerMovement = Object.FindObjectOfType<PlayerMovement>();
			playerMovement.skipChangeWait = true;
			playerMovement.playerSpeed *= 4;
			return null;
		}
	}
}
