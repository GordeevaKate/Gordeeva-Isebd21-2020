﻿using DocumentFormat.OpenXml.Wordprocessing;

namespace AbstractSyshiBarBusinessLogic.HelperModels
{
    class WordParagraphProperties
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public JustificationValues JustificationValues { get; set; }
    }
}
