using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class TapAttachedToDuctFailurePreprocessor : IFailuresPreprocessor
    {
          public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
          {
            IList<FailureMessageAccessor> failList = failuresAccessor.GetFailureMessages();
            foreach (var failure in failList)
            {
                var failID = failure.GetFailureDefinitionId();
                if (BuiltInFailures.ConnectorFailures.FittingMustBeOnDuctWarn==failID)
                {
                    failuresAccessor.DeleteWarning(failure);
                }
            }

            return FailureProcessingResult.Continue;
          }       
    }
}
