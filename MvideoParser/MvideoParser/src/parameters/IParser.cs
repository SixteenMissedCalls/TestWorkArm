﻿using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvideoParser.src.parameters
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
