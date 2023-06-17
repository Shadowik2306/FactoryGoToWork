using FactoryBusinessLogic.OfficePackage.HelperEnums;
using FactoryBusinessLogic.OfficePackage.HelperModels;

namespace FactoryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreatePlanReport(WordInfoMaster info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            List<List<(string, WordTextProperties)>> rowList = new()
                {
                    new()
                    {
                        new("План", new WordTextProperties { Bold = true, Size = "24" } ),
                        new("Кол-во",  new WordTextProperties { Bold = true, Size = "24" })
                    }
                };

            foreach (var plan in info.Plans)
            {
                rowList.Add(new()
                        {
                            new(plan.PlanName, new WordTextProperties { Size = "24" }),
                            new(plan.Count.ToString(), new WordTextProperties { Size = "24" })
                        });
            }

            CreateTable(new WordParagraph
            {
                RowTexts = rowList,
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            SaveWord(info);
        }

        public void CreateLatheReport(WordInfoEngenier info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            List<List<(string, WordTextProperties)>> rowList = new()
                {
                    new()
                    {
                        new("Cтанок", new WordTextProperties { Bold = true, Size = "24" } ),
                        new("Кол-во",  new WordTextProperties { Bold = true, Size = "24" })
                    }
                };

            foreach (var plan in info.Lathe)
            {
                rowList.Add(new()
                        {
                            new(plan.LatheName, new WordTextProperties { Size = "24" }),
                            new(plan.Count.ToString(), new WordTextProperties { Size = "24" })
                        });
            }

            CreateTable(new WordParagraph
            {
                RowTexts = rowList,
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            SaveWord(info);
        }

        protected abstract void CreateWord(WordInfoMaster info);
        protected abstract void CreateWord(WordInfoEngenier info);

        protected abstract void CreateParagraph(WordParagraph paragraph);

        protected abstract void CreateTable(WordParagraph paragraph);

        protected abstract void SaveWord(WordInfoMaster info);

        protected abstract void SaveWord(WordInfoEngenier info);
    }
}
