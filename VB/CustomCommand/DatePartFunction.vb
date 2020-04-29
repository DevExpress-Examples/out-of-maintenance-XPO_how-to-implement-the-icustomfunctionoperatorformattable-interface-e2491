Imports Microsoft.VisualBasic
Imports DevExpress.Data.Filtering
Imports System
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports System.Reflection

Namespace DXSample
	Public Class GetMonthFunction
		Implements ICustomFunctionOperatorFormattable, ICustomFunctionOperatorQueryable
		#Region "ICustomFunctionOperatorFormattable Members"

		Private Function ICustomFunctionOperatorFormattable_Format(ByVal providerType As Type, ParamArray ByVal operands() As String) As String Implements ICustomFunctionOperatorFormattable.Format
			If providerType Is GetType(SQLiteConnectionProvider) Then
				Return String.Format("strftime('%m', {0})", operands(0))
			End If
			Throw New NotSupportedException(String.Concat("This provider is not supported: ", providerType.Name))
		End Function

		Public Shared Function GetMonth(ByVal [date] As DateTime) As Integer
			Return [date].Month
		End Function

		#End Region

		#Region "ICustomFunctionOperator Members"

		Private Function Evaluate(ParamArray ByVal operands() As Object) As Object Implements ICustomFunctionOperator.Evaluate
			Return GetMonth(CDate(operands(0)))
		End Function

		Private ReadOnly Property Name() As String Implements ICustomFunctionOperator.Name
			Get
				Return "GetMonth"
			End Get
		End Property

		Private Function ResultType(ParamArray ByVal operands() As Type) As Type Implements ICustomFunctionOperator.ResultType
			Return GetType(Integer)
		End Function

		#End Region

		#Region "ICustomFunctionOperatorQueryable Members"

		Private Function GetMethodInfo() As MethodInfo Implements ICustomFunctionOperatorQueryable.GetMethodInfo
			Return Me.GetType().GetMethod("GetMonth")
		End Function

		#End Region
	End Class
End Namespace