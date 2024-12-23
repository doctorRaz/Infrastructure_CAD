using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using NUnit.Framework;

namespace Infrastructure.Tests
{
    public class IntExtensionsTest
    {
        [Test]
        public void DeclensionGeneratorTest()
        {
            int value = 1;
            string res = value.DeclensionGenerator("день", "дня", "дней");
            Assert.AreEqual(res, "день");
            value = 11;
            res = value.DeclensionGenerator("день", "дня", "дней");
            Assert.AreEqual(res, "дней");
            value = 21;
            res = value.DeclensionGenerator("день", "дня", "дней");
            Assert.AreEqual(res, "день");
            value = 111;
            res = value.DeclensionGenerator("день", "дня", "дней");
            Assert.AreEqual(res, "дней");
        }
    }
}
