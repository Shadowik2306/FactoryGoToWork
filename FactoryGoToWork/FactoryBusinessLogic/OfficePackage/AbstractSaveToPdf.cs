using DocumentFormat.OpenXml.Bibliography;
using FactoryBusinessLogic.OfficePackage.HelperEnums;
using FactoryBusinessLogic.OfficePackage.HelperModels;
using FactoryContracts.SearchModels;

namespace FactoryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {

		public void CreatePDFMaster(PdfInfoMaster info)
		{
			CreatePdf(info);


			CreateParagraph(new PdfParagraph
			{
				Text = $"Расчётный период: с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			CreateParagraph(new PdfParagraph { Text = info.Title, Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "4cm", "4cm", "4cm", "4cm", "4cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "План", "Дата начала", "Дата конца", "Деталь", "Cтанок" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var lathe in info.Lathe)
			{
		
				CreateRow(new PdfRowParameters()
				{
					Texts = new List<string> { lathe.PlanName, lathe.dateFrom.ToString(), lathe.dateTo.ToString(), lathe.ComponentName, lathe.LatheName },
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
				
			}

			SavePdf(info);
		}


		protected abstract void CreatePdf(PdfInfoMaster info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfoMaster info);
    }
}
