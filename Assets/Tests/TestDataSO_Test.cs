using UnityEngine;
using NUnit.Framework;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

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
            //Assert.AreEqual(100, testData.health);
            Assert.AreEqual(50f, testData.moveSpeed);
            Assert.IsTrue(testData.isPlayer);
            Assert.AreEqual(new Vector3(1, 1, 1), testData.initialPosition);
            Assert.AreEqual(new Color(1f, 0f, 0f, 0f), testData.characterColor);
            //Assert.IsNull(testData.prefab);
            Assert.That(testData.abilities, Has.Count.EqualTo(2));


            //Assert.Greater(testData.health, 50);          // health > 50
            Assert.Less(testData.moveSpeed, 60.0f);       // moveSpeed < 10
            //Assert.GreaterOrEqual(testData.health, 100);  // health >= 100
            //Assert.Positive(testData.health);             // health > 0
            Assert.Zero(0);                               // 0 == 0

            Assert.IsNotEmpty(testData.abilities);        // Список не пуст
            Assert.IsEmpty(new List<string>());           // Пустой список
            Assert.Contains("Fireball", testData.abilities);  // Содержит "Dash"
            Assert.That(testData.inventoryIDs, Is.EquivalentTo(new[] { 1, 2, 3 }));
            Assert.That(testData.abilities, Has.Count.EqualTo(2));


            Assert.AreEqual(CharacterClass.Warrior, testData.characterClass);
            Assert.IsTrue(Enum.IsDefined(typeof(CharacterClass), testData.characterClass));


            // Синхронное исключение
            Assert.Throws<ArgumentException>(() =>
            {
                if (testData.health < 0)
                {
                    throw new Exception("Invalid health!");
                    // throw new ArgumentException("Invalid health!");
                }                    
            });

            // Асинхронное исключение
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await Task.Delay(10);
                throw new Exception("Async error!");
            });

            // Ловит Exception и его подклассы
            Assert.Catch<Exception>(() => throw new InvalidOperationException());

            Assert.IsInstanceOf<GameObject>(testData.prefab);
            //Assert.IsAssignableFrom<ScriptableObject>(testData);
            Assert.IsNotInstanceOf<MonoBehaviour>(testData);
            

            // Pass/Fail
            if (testData.isPlayer)
                Assert.Pass("Персонаж является игроком");
            else
                Assert.Fail("Персонаж не игрок");
        }
    }
}
