using UnityEngine;
using NUnit.Framework;
using UnityEditor;

namespace TestTaskProj
{
    public class TestDataSO_Test
    {
        [Test]
        public void TestDataSO_CheckingValuesForCorrect()
        {
            var testData = AssetDatabase.LoadAssetAtPath<TestDataSO>("Assets/Tests/TestDataSO.asset");

            Assert.IsNotNull(testData);
            Assert.AreEqual("John", testData.characterName);
            Assert.AreEqual(100, testData.health);
            Assert.AreEqual(50f, testData.moveSpeed);
            Assert.IsFalse(testData.isPlayer);
            Assert.AreEqual(new Vector3(1,1,1), testData.initialPosition);
            Assert.AreEqual(new Color(1f, 0f, 0f, 0f), testData.characterColor);
        }
    }
}
