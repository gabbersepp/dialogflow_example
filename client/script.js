$(document).ready(doSomething);

function doSomething() {
    $.ajax({
        type: "POST",
        url: "http://localhost:5001/api/bot/SendQuery",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "alsdjhadj asa klaasdl sdl ",
        success: function(data) {
            setResponse(JSON.stringify(data, undefined, 2));
        },
        error: function() {
            setResponse("Internal Server Error");
        }
    });

    function setResponse(data) {

    }
}

function Dialog() {

}