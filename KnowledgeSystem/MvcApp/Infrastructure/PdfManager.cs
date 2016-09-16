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
            MemoryStream workStream;
            var document = GetNewDocument("It consists user information. Knowledge.com", out workStream);

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

            foreach (var category in profileInfo.Categories.Entities)
            {
                document.Add(new Paragraph(category.Name));
                document.Add(new Paragraph( " "));
                PdfPTable table = new PdfPTable(2);
                foreach (var skill in category.Skills)
                {
                    var firstRowCell1 = new PdfPCell(new Phrase(skill.Name));
                    var firstRowCell2 = new PdfPCell(new Phrase(skill.Level.ToString()));
                    PdfPCell[] row1Cells = { firstRowCell1, firstRowCell2 };
                    var row1 = new PdfPRow(row1Cells);
                    table.Rows.Add(row1);
                }
                document.Add(table);
            }

            return SendDocument(document, workStream);
        }

        public static MemoryStream CreateUserListPdf(List<SkillsModel> userList)
        {
            MemoryStream workStream;
            var document = GetNewDocument("It consists table with users and their skills. Knowledge.com",out workStream);

            int skillCount = userList.First().Skills.Count;
            PdfPTable table = new PdfPTable(skillCount+2);

            PdfPCell numberHeader = new PdfPCell(new Phrase("Number"));
            table.AddCell(numberHeader);

            PdfPCell userHeader= new PdfPCell(new Phrase("User"));
            table.AddCell(userHeader);

            foreach (var skill in userList.First().Skills)
            {
                PdfPCell skillHeader = new PdfPCell(new Phrase(skill.Name));
                table.AddCell(skillHeader);
            }

            int i = 0;
            foreach (var item in userList)
            {
                var numberCell = new PdfPCell(new Phrase((++i).ToString()));
                table.AddCell(numberCell);
                var userNameCell = new PdfPCell(new Phrase(item.FirstName+" "+item.LastName));
                table.AddCell(userNameCell);
                foreach (var skill in item.Skills)
                {
                    PdfPCell skillNameCell = new PdfPCell(new Phrase(skill.Level.ToString()));
                    table.AddCell(skillNameCell);
                }                
            }
            document.Add(table);

            return SendDocument(document,workStream);
        }

        private static Document GetNewDocument(string message, out MemoryStream workStream)
        {
            workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            document.Open();
            document.Add(new Paragraph("This is document, that was created automaticaly " + DateTime.Now.ToString()));
            document.Add(new Paragraph(message));
            document.Add(Chunk.NEWLINE);

            return document;
        }

        private static MemoryStream SendDocument(Document document, MemoryStream workStream)
        {
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return workStream;
        }
    }
}