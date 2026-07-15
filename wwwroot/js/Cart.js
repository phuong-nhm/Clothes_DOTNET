const data = @Html.Raw(JsonSerializer.Serialize(Model));
/*$.ajax({
    url: '/ControllerName/ActionName',
    type: 'POST',
    contentType: 'application/json',
    data: JSON.stringify(data),
    success: function (response) {
        console.log("Submitted successfully!");
    },
    error: function (xhr, status, error) {
        console.error("Error:", error);
    }
});*/