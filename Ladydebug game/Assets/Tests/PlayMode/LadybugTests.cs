using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LadybugTests
    {
        [UnityTest]
        public IEnumerator MoveUp()
        {
            GameObject gameObject = new GameObject();
            var ladybug = gameObject.AddComponent<Ladybug>();

            ladybug.MoveUp();
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(new Vector2(0, 1), (Vector2)gameObject.transform.position);
        }
    }
}
