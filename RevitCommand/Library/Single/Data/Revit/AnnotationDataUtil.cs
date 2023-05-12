using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class AnnotationDataUtil
    {
        private static AnnotationData annotationData
        {
            get
            {
                return AnnotationData.Instance;
            }
        }

        public static void RefreshDocument()
        {
            annotationData.MultiReferenceAnnotationTypes = null;
            annotationData.IndependentTags = null;
            annotationData.RebarTags = null;
            annotationData.RebarTagTypes = null;
            annotationData.RebarTagFamilies = null;
            annotationData.TextNoteTypes = null;
            annotationData.TextNotes = null;

            (annotationData as DataBase).RefreshDocument();
        }

        public static void RefreshUIDocument()
        {
            RefreshDocument();

            (annotationData as DataBase).RefreshDocument();
        }

        public static void Dispose()
        {
            AnnotationData.Instance = null;
        }
    }
}
