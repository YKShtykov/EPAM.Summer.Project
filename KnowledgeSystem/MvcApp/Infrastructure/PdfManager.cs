using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using MvcApp.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace MvcApp.Infrastructure
{
    public class PdfManager
    {
        public static MemoryStream CreateUserInfoPdf(FullProfileInfo profileInfo)
        {
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();            
            document.Add(new Paragraph("This is document, that was created automaticaly " + DateTime.Now.ToString()));
            document.Add(new Paragraph("It consists user information. KnowledgePortal.com"));

            if (!ReferenceEquals(profileInfo.Profile.Image,null))
            {
                //Image img = Image.GetInstance(profileInfo.Profile.Image);
                //img.ScaleAbsolute(120f, 155.25f);
                //PdfPCell imgCell1 = new PdfPCell();
                //imgCell1.AddElement(new Chunk(img, 0, 0));
                //document.Add(imgCell1); 
            }

            document.Add(new Paragraph("First name:" + profileInfo.Profile.FirstName));
            document.Add(new Paragraph("Middle name:" + profileInfo.Profile.MiddleName));
            document.Add(new Paragraph("Last name:" + profileInfo.Profile.LastName));
            document.Add(new Paragraph("Birth Date:" + profileInfo.Profile.BirthDate));
            document.Add(new Paragraph("Age:" + profileInfo.Profile.Age));
            document.Add(new Paragraph("City:" + profileInfo.Profile.City));
            document.Add(new Paragraph("Contact phone:" + profileInfo.Profile.ContactPhone));
            document.Add(new Paragraph("Contact mail:" + profileInfo.Profile.ContactEmail));
            document.Add(new Paragraph("Gender:" + profileInfo.Profile.Gender));
            document.Add(new Paragraph("Relationship status:" + profileInfo.Profile.RelationshipStatus));
            document.Add(new Paragraph("Additional information:" + profileInfo.Profile.AdditionalInfo));

            foreach (var item in profileInfo.Categories.Entities)
            {
                document.Add(new Paragraph(item.Name));
                document.Add(new Paragraph( " "));
                PdfPTable table = new PdfPTable(2);
                foreach (var skill in item.Skills)
                {
                    var firstRowCell1 = new PdfPCell(new Phrase(skill.Name));
                    var firstRowCell2 = new PdfPCell(new Phrase(skill.Level.ToString()));
                    PdfPCell[] row1Cells = { firstRowCell1, firstRowCell2 };
                    var row1 = new PdfPRow(row1Cells);
                    table.Rows.Add(row1);
                }
                document.Add(table);
            }
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return workStream;
        }
    }
}