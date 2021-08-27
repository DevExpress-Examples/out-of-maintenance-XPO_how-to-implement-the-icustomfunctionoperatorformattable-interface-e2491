<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128586037/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2491)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DatePartFunction.cs](./CS/DatePartFunction.cs) (VB: [DatePartFunction.vb](./VB/DatePartFunction.vb))
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->
# How to implement the ICustomFunctionOperatorFormattable interface


<p>This example is a modified version of the <a href="https://www.devexpress.com/Support/Center/p/E207">E207</a> example. Starting from version 9.2 we implemented the ICustomFunctionOperatorFormattable interface. Now, there is no necessity to create a custom connection provider to implement custom functions. All that you need is to create a class implementing the ICustomFunctionOperatorFormattable interface.</p>
<p>The ICustomFunctionOperatorFormattable interface defines following members:</p>
<p><em>Name</em> - should return the name of the custom function. This name should be passed as a first operand to a FunctionOperator constructor:</p>


```cs
FunctionOperator op = new FunctionOperator(FunctionOperatorType.Custom, "MyFunction", operandA, operandB);
// or
CriteriaOperator op = CriteriaOperator.Parse("custom('MyFunction', ?, ?)", operandA, operandB);

```




```vb
Dim op As New FunctionOperator(FunctionOperatorType.Custom, "MyFunction", operandA, operandB)
' or
Dim op As CriteriaOperator = CriteriaOperator.Parse("custom('MyFunction', ?, ?)", operandA, operandB)

```


<p><em>ResultType</em> - should return the type of the result value.</p>
<p><em>Format</em> - this method should return the SQL command. The <em>providerType</em> parameter allows you to provide the different implementation for each SQL provider, if they use different SQL syntax for the operation you want to implement.</p>
<p><em>Evaluate</em> - this method allows you to provide the client side implementation of the custom function.</p>
<p>Please note that you should register the custom function operator using the ConnectionProviderSql.RegisterCustomFunction method, and add it to the XPDictionary.CustomFunctionOperators collection.</p>
<p><strong>Remark:<br> </strong>The code above demonstrates the old syntax: <em>"custom('MyFunction', ?, ?)"</em><br> Starting from version 10.2, a more intuitive syntax can be used: <em>"MyFunction(?, ?)"</em></p>
<p>In addition to the ICustomFunctionOperatorFormattable interface, the custom function can implement the ICustomFunctionOperatorQueryable interface. This function can be used in LINQ to XPO queries.</p>
<p>The ICustomFunctionOperatorQueryable interface defines one method: GetMethodInfo. This method should return the MethodInfo instance that will be used to find the custom function in the dictionary. The method name should match the custom function name, because LINQ to XPO will search the custom function using the method name. Use of the custom function in the LINQ to XPO query is shown below:</p>


```cs
var list = from o in new XPQuery<Order>(session)
          where o.OrderName == "Chai"
          select new { Month = GetMonthFunction.GetMonth(o.OrderDate) };

```




```vb
'This code snippet uses implicit typing. You will need to set 'Option Infer On' in the VB file or set 'Option Infer' at the project level:
Dim list = _
From o In New XPQuery(Of Order)(session) _
Where o.OrderName = "Chai" _
Select New With {Key .Month = GetMonthFunction.GetMonth(o.OrderDate)}

```


<p>It is necessary to register custom functions using the ConnectionProviderSql.RegisterCustomFunctionOperator method, and add them to the XPDictionary.CustomFunctionOperators collection.<br><br></p>
<p><strong>See Also: <br></strong><a href="https://documentation.devexpress.com/CoreLibraries/CustomDocument5206.aspx">How to: Implement a Custom Criteria Language Operator</a> <br><a href="https://documentation.devexpress.com/CoreLibraries/CustomDocument9948.aspx">How to: Implement Custom Functions and Criteria in LINQ to XPO</a> <br><br><br></p>

<br/>


