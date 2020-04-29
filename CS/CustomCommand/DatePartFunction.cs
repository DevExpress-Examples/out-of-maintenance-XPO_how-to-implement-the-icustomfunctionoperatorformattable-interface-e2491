using DevExpress.Data.Filtering;
using System;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using System.Reflection;

namespace DXSample {
    public class GetMonthFunction :ICustomFunctionOperatorFormattable, ICustomFunctionOperatorQueryable {
        #region ICustomFunctionOperatorFormattable Members

        string ICustomFunctionOperatorFormattable.Format(Type providerType, params string[] operands) {
            if (providerType == typeof(SQLiteConnectionProvider))
                return string.Format("strftime('%m', {0})", operands[0]);
            throw new NotSupportedException(string.Concat("This provider is not supported: ", providerType.Name));
        }

        public static int GetMonth(DateTime date) {
            return date.Month;
        }

        #endregion

        #region ICustomFunctionOperator Members

        object ICustomFunctionOperator.Evaluate(params object[] operands) {
            return GetMonth((DateTime)operands[0]);
        }

        string ICustomFunctionOperator.Name {
            get { return "GetMonth"; }
        }

        Type ICustomFunctionOperator.ResultType(params Type[] operands) {
            return typeof(int);
        }

        #endregion

        #region ICustomFunctionOperatorQueryable Members

        MethodInfo ICustomFunctionOperatorQueryable.GetMethodInfo() {
            return GetType().GetMethod("GetMonth");
        }

        #endregion
    }
}