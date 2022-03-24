window.onload = init;

function init() {
};

function TaskClick() {
    var stringNumbers = "";
    stringNumbers = $("#str").val();
    if (stringNumbers != "") {
        $.ajax({
            type: "POST",
            url: "/Site/GetGreatestCommonDivisor",
            data: { stringNumbers },
            dataType: "text",
            success: function (answer) {
                $("#divAnswer").css("display", "flex");
                $("#answer").text(answer);
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}