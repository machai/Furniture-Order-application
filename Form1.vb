Public Class Form1

    Const SOFA_PRICE As Double = 925
    Const CHAIR_PRICE As Double = 350
    Const SALES_TAX As Double = 0.05

    Private Sub btnProcessOrder_Click(sender As Object, e As EventArgs) Handles btnProcessOrder.Click
        Dim validationOK As Boolean
        Dim total As Double
        Dim name As String, address As String, cityZip As String
        Dim chairQuantity As Integer, sofaQuantity As Integer
        Dim invoiceNumber As String
        Dim swappedNames As String

        validationOK = InputValidation()

        If validationOK Then
            name = txtName.Text
            address = txtAddress.Text
            cityZip = txtCity.Text
            chairQuantity = CInt(txtChairs.Text)
            sofaQuantity = CInt(txtSofas.Text)

            total = CalculateTotal(chairQuantity, sofaQuantity)
            invoiceNumber = CreateInvoiceNumber(name, cityZip)
            swappedNames = SwapNames(name)
            DisplayInvoice(invoiceNumber, swappedNames, total)
        End If
    End Sub

    Public Sub DisplayInvoice(invoiceNumber As String, customerName As String, total As Double)
        lstDisplay.Items.Add("Invoice Number: " & invoiceNumber)
        lstDisplay.Items.Add(" ")
        lstDisplay.Items.Add("Customer Name: " & customerName)
        lstDisplay.Items.Add("Address: " & txtAddress.Text)
        lstDisplay.Items.Add("City, State, Zip: " & txtCity.Text)
        lstDisplay.Items.Add(" ")
        lstDisplay.Items.Add("Number of Chairs Ordered: " & txtChairs.Text)
        lstDisplay.Items.Add("Number of Sofas Ordered: " & txtSofas.Text)
        lstDisplay.Items.Add("Total Due (including Tax): " & FormatCurrency(total, 2))
    End Sub

    Public Function SwapNames(name As String) As String
        Dim firstName As String
        Dim lastName As String

        firstName = name.Split(","c)(1).Trim
        lastName = name.Split(","c)(0).Trim


        Return firstName & " " & lastName

    End Function

    Public Function CreateInvoiceNumber(name As String, cityZip As String) As String
        Dim invoiceNumber As String

        invoiceNumber = name.Substring(0, 2).ToUpper
        invoiceNumber += cityZip.Substring(cityZip.Length - 4)

        Return invoiceNumber
    End Function

    Public Function InputValidation() As Boolean

        If Not txtName.Text.Contains(",") Then
            MsgBox("Please make sure the names are separated by a comma")
            Return False
        End If

        If txtAddress.Text = "" Or txtCity.Text = "" Then
            MsgBox("Please enter address and city")
            Return False
        End If

        If Not IsNumeric(txtChairs.Text) Or Not IsNumeric(txtSofas.Text) Then
            MsgBox("Please enter valid quantities")
            Return False
        End If

        Return True
    End Function

    Public Function CalculateTotal(chairQuantity As Integer, sofaQuantity As Integer) As Double
        Dim total As Double
        Dim tax As Double

        total = chairQuantity * CHAIR_PRICE + sofaQuantity * SOFA_PRICE
        tax = total * SALES_TAX

        Return total + tax

    End Function

    
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        For Each Control In Me.Controls
            If TypeName(Control) = "TextBox" Then
                Control.Text = ""
            End If
        Next
        lstDisplay.Items.Clear()

    End Sub
End Class
