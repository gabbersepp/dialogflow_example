using System.Collections.Generic;

namespace WebApplication1.Bot
{
    public class Dialog
    {
        
        public List<Element> Elements = new List<Element>();

        public Dialog AddTextInput(string name)
        {
            Elements.Add(new TextInput() { Name = name });
            return this;
        }

        public Dialog AddTextPanel(string text, string name)
        {
            Elements.Add(new TextPanel { Name = name, Text = text });
            return this;
        }

        public Dialog AddButton(string text, string name)
        {
            Elements.Add(new Button{Name = name, Text = text});
            return this;
        }
    }
   
    public class Element
    {
        public string Name;
        public string Type { get; }

        public Element(string type)
        {
            Type = type;
        }
    }

    public class TextInput : Element
    {
        public TextInput() : base("textinput")
        {
        }
    }

    public class Button : Element
    {
        public string Text;

        public Button() : base("button")
        {
        }
    }

    public class TextPanel : Element
    {
        public string Text;

        public TextPanel() : base("textpanel")
        {
        }
    }

}