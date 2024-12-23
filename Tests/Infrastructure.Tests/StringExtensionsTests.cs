using NUnit.Framework;
using Infrastructure.Extensions;

namespace Infrastructure.Tests
{
    public class StringExtensionsTests
    {
        [Test]
        public void CheckStringEMail()
        {
            Assert.IsTrue("kpblc2000@gmail.com".IsCorrectMail());
            Assert.IsFalse("kpblc2000@yandex@gmail.com".IsCorrectMail());
        }
    }
}
