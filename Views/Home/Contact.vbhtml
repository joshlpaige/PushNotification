@ModelType List(Of String)

<h1>Send Push Notifications</h1>
<form asp-action="Notify">
    @If ViewData("msg") IsNot Nothing Then

          @<h1>@ViewData("msg")</h1>

    End If
    <label for="message">Message: </label>
    <input id="message" name="message" /><br />
    @If ViewData("Clients") IsNot Nothing Then
        Dim clientList As List(Of String) = TryCast(ViewData("Clients"), List(Of String))
        For Each item As String In clientList
            @<input type="radio" id="@(item)_identifier" name="client" value="@item">
            @<label for="@(item)_identifier">@item</label>
        Next

    End If
    <button type="submit"> Push</button>
</form>
