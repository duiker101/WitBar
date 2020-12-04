using NUnit.Framework;
using App;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSingleLine()
        {
            RootEntry root = OutputParser.parse("hello");

            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(root.menu.Count, 0);
            Assert.AreEqual(root.children[0].text, "hello");
            Assert.Pass();
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
            Assert.Pass();
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
            Assert.Pass();
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
            Assert.Pass();
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
            Assert.Pass();
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
            Assert.Pass();
        }


        [Test]
        public void TestFull()
        {
            RootEntry root = OutputParser.parse(@"G $175|
---
Time: 10.3|
---
Month|
--$175|
-----
--01 Tue        $46.92|
--03 Thu        $127.65|");
            Assert.AreEqual(root.children.Count, 1);
            Assert.AreEqual(3, root.menu.Count);
            Assert.Pass();
        }

    }
}