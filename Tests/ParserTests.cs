using NUnit.Framework;
using App;

namespace Tests
{
    public class ParserTests
    {
        [Test]
        public void TestSingleLine()
        {
            RootEntry root = OutputParser.parse("hello");

            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(root.menu.Count, 0);
            Assert.AreEqual(root.children[0].text, "hello");
        }

        [Test]
        public void TestTwoLinesInRoot()
        {
            RootEntry root = OutputParser.parse(@"hello
there");

            Assert.AreEqual(root.children.Count, 2);
            Assert.AreEqual(root.menu.Count, 0);
            Assert.AreEqual(root.children[0].text, "hello");
            Assert.AreEqual(root.children[1].text, "there");
        }

        [Test]
        public void TestOneContext()
        {
            RootEntry root = OutputParser.parse(@"hello
---
there");

            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(root.menu.Count, 1);
            Assert.AreEqual(root.children[0].text, "hello");
            Assert.AreEqual(root.menu[0].text, "there");
        }

        [Test]
        public void TestOneContextWithOneSubmenu()
        {
            RootEntry root = OutputParser.parse(@"hello
---
there
--b");
            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(root.menu.Count, 1);
            Assert.AreEqual(root.children[0].text, "hello");
            Assert.AreEqual(root.menu[0].text, "there");
            Assert.AreEqual(root.menu[0].children[0].text, "b");
        }

        [Test]
        public void TestNested()
        {
            RootEntry root = OutputParser.parse(@"a
---
b
--c
----d");
            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(root.menu.Count, 1);
            Assert.AreEqual(root.menu[0].children.Count, 1);
            Assert.AreEqual(root.menu[0].children[0].children.Count, 1);
        }

        [Test]
        public void TestNestedDifferentLevels()
        {
            RootEntry root = OutputParser.parse(@"a
---
b
--c
----d
----e
------f
------g
--h
----l");
            Assert.AreEqual(1, root.children.Count);
            Assert.AreEqual(1, root.menu.Count);
            Assert.AreEqual(2, root.menu[0].children.Count);
            Assert.AreEqual(2, root.menu[0].children[0].children.Count);
            Assert.AreEqual(2, root.menu[0].children[0].children[1].children.Count);
            Assert.AreEqual(1, root.menu[0].children[1].children.Count);
        }

        [Test]
        public void TestSubmenuSeparator()
        {
            RootEntry root = OutputParser.parse(@"hello
---
there
--b
-----
--c");
            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(root.menu.Count, 1);
            Assert.AreEqual(root.children[0].text, "hello");
            Assert.AreEqual(root.menu[0].text, "there");
            Assert.AreEqual(root.menu[0].children[0].text, "b");
            Assert.AreEqual(root.menu[0].children[1].isSeparator, true);
        }


        [Test]
        public void TestFull()
        {
            RootEntry root = OutputParser.parse(@"a
---
b
---
c
--d
-----
--e
--f");
            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(3, root.menu.Count);
        }

        [Test]
        public void TestParameters()
        {
            RootEntry root = OutputParser.parse(@"a|color=#ff0");
            Assert.AreEqual(1, root.children.Count);
            Assert.AreEqual("#ff0", root.children[0].color);
        }

        [Test]
        public void TestTokenizerSimple()
        {
            var root = OutputParser.Tokenize(@"a=b");
            Assert.AreEqual("b", root["a"]);
        }

        [Test]
        public void TestTokenizerTwoKeys()
        {
            var root = OutputParser.Tokenize(@"a=b c=d");
            Assert.AreEqual("b", root["a"]);
        }

        [Test]
        public void TestTokenizerWithQuotes()
        {
            var root = OutputParser.Tokenize("a=b c=d f=\"a a\"");
            Assert.AreEqual("b", root["a"]);
            Assert.AreEqual("d", root["c"]);
            Assert.AreEqual("a a", root["f"]);
        }

        [Test]
        public void TestTokenizerWithSingleQuotes()
        {
            var root = OutputParser.Tokenize("a=b f='a a' c=d");
            Assert.AreEqual("b", root["a"]);
            Assert.AreEqual("d", root["c"]);
            Assert.AreEqual("a a", root["f"]);
        }
    }

}