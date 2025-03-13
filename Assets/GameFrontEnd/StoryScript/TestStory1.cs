using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace GameFrontEnd.StoryScript
{
    public class TestStory1:Story
    {
        public TestStory1(string filePath, DialogueActionManager m) : base(filePath, m)
        {
        }
    }
}