using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace App
{
    public class Line
    {
        public bool isSeparator = false;
        public int depth = 0;
        public String text = "";
        public String color = null;
    }

    public class Entry : Line
    {
        public List<Entry> children = new List<Entry>();
    }

    public class RootEntry
    {
        public List<Entry> children = new List<Entry>();
        public List<Entry> menu = new List<Entry>();
        public bool error = false;
        public String errorMessage = null;

    }

    public class OutputParser
    {
        public static Entry parseLine(String line)
        {
            var depth = 0;
            Entry entry = new Entry();

            // Check depth
            if (line.StartsWith("--"))
            {

                while (line.StartsWith("--"))
                {
                    if (line == "---")
                    {
                        entry.depth = depth;
                        entry.isSeparator = true;
                        return entry;
                    }
                    depth++;
                    line = line.Substring(2, line.Length - 2);
                }

                entry.depth = depth;
            }

            var indexOfFirstSeparator = line.IndexOf("|");
            if (indexOfFirstSeparator >= 0)
            {
                entry.text = line.Substring(0, indexOfFirstSeparator);
                var parameters = Tokenize(line.Substring(indexOfFirstSeparator + 1, line.Length - indexOfFirstSeparator - 1));

                if (parameters.ContainsKey("color"))
                    entry.color = parameters["color"];
            }
            else
            {
                entry.text = line;
            }


            return entry;
        }

        public static RootEntry parse(String data)
        {
            RootEntry result = new RootEntry();
            String[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var stack = new Stack<List<Entry>>();
            stack.Push(result.menu);
            Entry parent = null;

            bool isRoot = true;

            foreach (var line in lines)
            {
                if (line == "") continue;

                var entry = parseLine(line);

                if (isRoot && entry.isSeparator)
                {
                    isRoot = false;
                    continue;
                }

                if (isRoot)
                {
                    result.children.Add(entry);
                }
                else
                {
                    var currentDepth = stack.Count - 1;

                    if (entry.depth < currentDepth)
                    {
                        for (var i = currentDepth; i > entry.depth; i--)
                        {
                            stack.Pop();
                        }
                    }
                    else if (entry.depth > currentDepth && parent != null)
                    {
                        stack.Push(parent.children);
                    }

                    stack.First().Add(entry);
                }
                parent = entry;
            }

            return result;
        }


        public static Dictionary<String, String> Tokenize(String input)
        {
            var res = new Dictionary<String, String>();


            string[] quotes = { "'", "\"" };
            string[] separators = { "=", ":" };
            string[] space = { " " };
            int nextIndex = findIndexOf(input, separators);

            while (nextIndex >= 0)
            {
                var key = input.Substring(0, nextIndex);
                input = input.Substring(nextIndex + 1, input.Length - nextIndex - 1);

                if (findIndexOf(input, quotes) == 0)
                {
                    input = input.Substring(1, input.Length - 1);
                    var indexOfLastQuote = findIndexOf(input, quotes);
                    var value = input.Substring(0, indexOfLastQuote);
                    res.Add(key.Trim(), value);

                    if (indexOfLastQuote + 1 < input.Length)
                        input = input.Substring(indexOfLastQuote + 1, input.Length - indexOfLastQuote - 1);
                }
                else
                {
                    var indexOfSpace = findIndexOf(input, space);
                    indexOfSpace = indexOfSpace >= 0 ? indexOfSpace : input.Length;
                    var value = input.Substring(0, indexOfSpace);
                    res.Add(key.Trim(), value);

                    if (indexOfSpace + 1 < input.Length)
                        input = input.Substring(indexOfSpace + 1, input.Length - indexOfSpace - 1);
                }

                nextIndex = findIndexOf(input, separators);
            }

            return res;
        }

        private static int findIndexOf(string input, string[] search)
        {
            var res = -1;

            foreach (var s in search)
            {
                var i = input.IndexOf(s);
                if (i >= 0 && (i < res || res < 0))
                    res = i;
            }

            return res;
        }

    }
}
