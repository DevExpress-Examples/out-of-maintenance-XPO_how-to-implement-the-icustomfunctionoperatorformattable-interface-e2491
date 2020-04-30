Imports Microsoft.VisualBasic
Imports System
Imports DXSample
Imports System.Data
Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports System.Diagnostics
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.Helpers
Imports DevExpress.Xpo.Metadata

Namespace CustomCommand
	Friend Module Program
		Sub Main()
			Dim provider As ConnectionProviderSql = CType(XpoDefault.GetConnectionProvider(SQLiteConnectionProvider.GetConnectionString("CustomCommand.sqlite"), AutoCreateOption.DatabaseAndSchema), ConnectionProviderSql)
			provider.RegisterCustomFunctionOperator(New GetMonthFunction())
			Dim dict As XPDictionary = New ReflectionDictionary()
			dict.CustomFunctionOperators.Add(New GetMonthFunction())
			XpoDefault.DataLayer = New SimpleDataLayer(dict, provider)
			CreateData()

			Using session As New Session()
				Dim view As New XPView(session, GetType(Order))
				view.AddProperty("Month", "custom('GetMonth', OrderDate)")

				For Each prop As ViewRecord In view
					Console.WriteLine(prop("Month"))
				Next prop

				Dim list = From o In New XPQuery(Of Order)(session)
					Where o.OrderName = "Chai"
					Select New With {Key .Month = GetMonthFunction.GetMonth(o.OrderDate)}

				For Each item In list
					Console.WriteLine(item.Month)
				Next item
			End Using
			Console.WriteLine("done" & vbLf & "press any key to exit ..")
			Console.ReadKey()
		End Sub

		Private Sub CreateData()
			Using uow As New UnitOfWork()
				If uow.FindObject(Of Order)(Nothing) IsNot Nothing Then
					Return
				End If
				Call New Order(uow) With {
					.OrderName = "Chai",
					.OrderDate = New DateTime(2011, 2, 10)
				}
				uow.CommitChanges()
			End Using
		End Sub
	End Module
End Namespace