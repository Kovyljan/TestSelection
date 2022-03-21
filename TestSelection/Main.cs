using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSelection
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            //Поиск элементов модели по классу
            var ducts = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            ////Выбор по параметру

            int countLevel_1 = 0;
            int countLevel_2 = 0;
            foreach (Duct duct in ducts)
            {
                string levelName = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsValueString().ToString();

                if (levelName == "Level 1" && levelName != "")
                {
                    countLevel_1++;
                }
                else if (levelName == "Level 2" && levelName != "")
                {
                    countLevel_2++;
                }
            }

            TaskDialog.Show("",$"Количество воздуховодов на первом этаже: {countLevel_1} {Environment.NewLine} Количество воздуховодов на втором этаже: {countLevel_2}");

            return Result.Succeeded;
        }
    }
}
