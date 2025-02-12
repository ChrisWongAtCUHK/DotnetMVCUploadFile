using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotnetMVC.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DotnetMVC.Controllers;

public class AspsnippetsController(ILogger<AspsnippetsController> logger) : Controller
{
    private readonly ILogger<AspsnippetsController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create()
    {
        using (MemoryStream stream = new MemoryStream())
        {
            // Create a document.
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
            {
                // Add main document part.
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create document structure.
                mainPart.Document = new Document();

                // Create document body.
                Body body = mainPart.Document.AppendChild(new Body());

                // Create paragraph.
                Paragraph paragraph = new Paragraph();

                // Creating Run.
                Run run = this.AddRun("Hi,");

                // Adding Run to Paragraph.
                paragraph.Append(run);

                // Adding Paragraph to Body.
                body.Append(paragraph);

                // Adding new Paragraph.
                paragraph = new Paragraph();
                run = this.AddRun("This is ");
                paragraph.Append(run);

                // Adding Text with Bold and Italic.
                run = this.AddRun("Mudassar Khan", bold: true, italic: true);
                paragraph.Append(run);

                run = this.AddRun(".");
                paragraph.Append(run);

                // Adding Paragraph to Body.
                body.Append(paragraph);

                // Adding Paragraph for Hyperlink.
                paragraph = new Paragraph();

                // Adding Hyperlink to Paragraph.
                paragraph.Append(this.AddLink(ref mainPart, "aspsnippets", "https://www.aspsnippets.com"));

                // Adding Hyperlink Paragraph to Document Body.
                body.Append(paragraph);
            }

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Sample.docx");
        }
    }

    private Run AddRun(
    string word,
    string font = "Arial",
    string size = "20",
    bool bold = false,
    bool italic = false,
    bool underline = false,
    bool preserveSpace = true)
    {
        // Create Run instance.
        Run run = new();

        // Create RunFonts instance.
        RunFonts runFont = new RunFonts { Ascii = font };

        // Create FontSize instance.
        //It must be multiplication twice of the required size.
        FontSize fontSize = new FontSize { Val = new StringValue(size) };

        // Create Text instance.
        Text text = new Text(word);

        // Create RunProperties instance.
        RunProperties runProperties = new RunProperties();
        if (bold)
        {
            // Applying Bold.
            runProperties.Bold = new Bold();
        }
        if (italic)
        {
            // Applying Italic.
            runProperties.Italic = new Italic();
        }
        if (underline)
        {
            // Applying Underline.
            runProperties.Underline = new Underline { Val = UnderlineValues.Single };
        }
        if (preserveSpace)
        {
            // Defines the SpaceProcessingModeValues.
            text.Space = SpaceProcessingModeValues.Preserve;
        }
        // Adding Font to RunProperties.
        runProperties.Append(runFont);

        // Adding FontSize to RunProperties.
        runProperties.Append(fontSize);

        // Adding RunProperties to Run.
        run.Append(runProperties);

        // Adding Text to Run.
        run.Append(text);

        return run;
    }

    private Hyperlink AddLink(ref MainDocumentPart mainPart, string text, string url)
    {
        // Adding HyperlinkRelationship with Relationship Id.
        HyperlinkRelationship hyperlinkRelationship = mainPart.AddHyperlinkRelationship(new Uri(url), true);

        // Creating Hyperlink.
        Hyperlink hyperlink = new Hyperlink();
        hyperlink.Id = hyperlinkRelationship.Id;

        // Add to Viewed Hyperlinks.
        hyperlink.History = OnOffValue.FromBoolean(true);

        // Creating Run.
        Run run = this.AddRun(text, underline: true);

        // Specifying the Color Class.
        run.RunProperties!.Color = new Color { ThemeColor = ThemeColorValues.Hyperlink };

        // Specifying the RunStyle Class.
        run.RunProperties.RunStyle = new RunStyle { Val = "Hyperlink" };

        // Underline the Hyperlink.
        run.RunProperties.Underline = new Underline { Val = UnderlineValues.Single };

        // Adding the Run to Hyperlink.
        hyperlink.Append(run);

        return hyperlink;
    }

}
