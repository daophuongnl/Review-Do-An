using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Utility;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public abstract class RevitCommand : IExternalCommand
    {
        protected RevitData revitData => RevitData.Instance; 
        protected StructuralData structuralData => StructuralData.Instance;
        protected ArchitectData architectData => ArchitectData.Instance;
        protected MEPData mepData => MEPData.Instance;
        protected ViewData viewData => ViewData.Instance;
        protected WorksetData worksetData => WorksetData.Instance;
        protected IOData ioData => IOData.Instance;
        protected UIApplication uiapp => revitData.UIApplication!;
        protected Autodesk.Revit.ApplicationServices.Application app => revitData.Application;
        protected UIDocument uidoc => revitData.UIDocument;
        protected Document doc => revitData.Document;
        protected Selection sel => revitData.Selection;

        protected virtual bool IsAutoDisposed => true;
        protected virtual bool HasExternalEvent => false;
        protected virtual bool IsExecute => true;
        protected virtual bool IsThrowWhenCatchException => false;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            revitData.UIApplication = commandData.Application;
            PreExecute();

            try
            {
                if (!IsExecute) return Result.Succeeded;
                Execute();
            }
            catch(Exception ex)
            {
                RevitDataUtil.Dispose();

                var mess = $"{ex.Message}\n{ex.StackTrace}";
                try
                {
                    File_Util.WriteTxtFileAndOpen(ioData.ErrorFilePath, mess);
                }
                catch
                {
                    // Xử lý để bắt lỗi trên máy người dùng, khi không thể viết file lỗi trên notepad
                    System.Windows.MessageBox.Show(mess, "Lỗi xảy ra!");
                }

                if (this.IsThrowWhenCatchException)
                {
                    throw;
                }
                else
                {
                    return Result.Succeeded;
                }
            }

            PostExecute();
            return Result.Succeeded;
        }

        protected virtual void PreExecute()
        {
            if (HasExternalEvent)
            {
                revitData.ExternalEvent = ExternalEvent.Create(revitData.ExternalEventHandler);
            }
        }

        protected virtual void PostExecute()
        {
            if (IsAutoDisposed)
            {
                RevitDataUtil.Dispose();
            }
        }

        public abstract void Execute();
    }
}
