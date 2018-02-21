$(document).ready(doSomething);

function doSomething() {
    var messages = new Messages();
}

function Messages() {
    this.sessionId = Math.random();
    this.$messagesInput = $("#messageinput");
    this.$messagesSubmit = $("#messagesubmit");

    this.$messagesInput.keypress((function(e) {
        if (e.which == 13) {
            this.$messagesSubmit.click();
            return false;
        }
    }).bind(this));
    this.$messagesSubmit.click(this.send.bind(this));
    $("#messagenew").click((function() {
        this.sessionId = Math.random();
    }).bind(this));
};

Messages.prototype.send = function() {
    var bot = new Bot();
    var query = this.$messagesInput.val();
    this.addOwn(query);
    bot.send(query, this.sessionId, this.processAgentResponse.bind(this));
    this.$messagesInput.val("");
};

Messages.prototype.processAgentResponse = function(data) {
    if(data.result.fulfillment && data.result.fulfillment.speech) {
        this.addAgent(data.result.fulfillment.speech);
    }
};

Messages.prototype.addOwn = function(text) {
    $(".messagecontainer .messages").append("<div class='own'>" + text + "</div>");
    var myElement = $('.messageinput')[0];
    var topPos = myElement.offsetTop;
    $('.messages')[0].scrollTop = topPos;
};

Messages.prototype.addAgent = function(text) {
    $(".messagecontainer .messages").append("<div class='agent'>" + text + "</div>");
    var myElement = $('.messageinput')[0];
    var topPos = myElement.offsetTop;
    $('.messages')[0].scrollTop = topPos;
};

function Bot() {

};

Bot.prototype.send = function(text, sessionId, callback) {
    $.ajax({
        type: "POST",
        url: "http://localhost:5001/api/bot/SendQuery",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({text: text, sessionId: sessionId}),
        success: function(data) {
            callback(data);
        },
        error: function() {
            setResponse("Internal Server Error");
        }
    });

    function setResponse(data) {

    }
}