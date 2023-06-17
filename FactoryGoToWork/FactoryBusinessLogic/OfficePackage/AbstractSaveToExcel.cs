using FactoryBusinessLogic.OfficePackage.HelperEnums;
using FactoryBusinessLogic.OfficePackage.HelperModels;

namespace FactoryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReportMaster(ExcelInfoMaster info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;

			foreach (var plan in info.Plans)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "План " + plan.PlanName,
					StyleInfo = ExcelStyleInfoType.Text
				});

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "Кол-во " + plan.Count,
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
            }

			SaveExcel(info);
        }

        public void CreateReportEngenier(ExcelInfoEngenier info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;

            foreach (var plan in info.Lathe)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = "Станок " + plan.LatheName,
                    StyleInfo = ExcelStyleInfoType.Text
                });

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "Кол-во " + plan.Count,
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
            }

            SaveExcel(info);
        }

        protected abstract void CreateExcel(ExcelInfoMaster info);

        protected abstract void CreateExcel(ExcelInfoEngenier info);

        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        protected abstract void SaveExcel(ExcelInfoMaster info);

        protected abstract void SaveExcel(ExcelInfoEngenier info);
    }
}
