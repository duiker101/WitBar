using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace App
{
    public class Line
    {
        public bool isSeparator = false;
        public int depth = 0;
        public String text = "";
    }

    public class Entry : Line
    {
        public List<Entry> children = new List<Entry>();
    }

    public class RootEntry
    {
        public List<Entry> children = new List<Entry>();
        public List<Entry> menu = new List<Entry>();
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
            }
            else
            {
                entry.text = line;
            }



            return entry;
        }

        public static RootEntry parse(String data)
        {
            Debug.WriteLine(data);
            RootEntry result = new RootEntry();
            String[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var stack = new Stack<Entry>();
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
                    var currentDepth = stack.Count;

                    if (entry.depth < currentDepth)
                    {
                        for (var i = entry.depth; i > currentDepth; i--)
                        {
                            stack.Pop();
                        }
                    }

                    if (entry.depth == 0)
                    {
                        if (stack.Count > 0)
                            stack.Pop();
                        stack.Push(entry);
                        result.menu.Add(entry);
                    }
                    else
                        stack.Last().children.Add(entry);

                    if (entry.depth > currentDepth)
                    {
                        stack.Push(entry);
                    }
                }
            }

            return result;
        }
    }
}
