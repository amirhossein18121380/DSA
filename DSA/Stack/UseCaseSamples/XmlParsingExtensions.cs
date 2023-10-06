namespace DSA.Stack.UseCaseSamples;
using System;
using System.Collections.Generic;
using System.Xml;

public static class XmlParsingExtensions
{
    public static bool IsXmlValid(this string xmlContent)
    {
        try
        {
            Stack<string> elementStack = new Stack<string>();
            using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlContent)))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        elementStack.Push(reader.Name);
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (elementStack.Count == 0 || elementStack.Peek() != reader.Name)
                        {
                            // Mismatched closing tag or extra closing tag
                            return false;
                        }
                        elementStack.Pop();
                    }
                }
            }

            // If the stack is empty, all tags had matching closing tags
            return elementStack.Count == 0;
        }
        catch (XmlException)
        {
            // Handle any XML parsing errors
            return false;
        }
    }

    public static void Apply()
    {
        string markup = "<html><head><title>Stack Parsing Example</title></head><body><p>Hello, <em>world</em>!</p></body></html>";
        string validXml = "<root><person><name>John</name><age>30</age></person></root>";
        string validXml2 = "<sth><name>John</name><age>30</age></sth>";
        string invalidXml = "<root><person><name>John</name><age>30</root></person>";


        foreach (var x in new List<string>() { markup, validXml, invalidXml, validXml2 })
        {
            if (x.IsXmlValid())
            {
                Console.WriteLine($"{x} == Valid XML");
            }
            else
            {
                Console.WriteLine($"{x} == Invalid XML");
            }
        }
    }
}
