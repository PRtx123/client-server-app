window.onload = init;
var lastId = 0;
var currentId;
var currentAction = "";

function init() {
    lastId = $(".table tr:last").attr("id");
};

function AddClick() {
    currentAction = "Add";
    $("#name").val("");
    $("#author").val("");
    $("#genre").val("");
    $(".edit").fadeIn();
}

function EditClick(id) {
    currentAction = "Edit";
    $(".edit").fadeIn();
    currentId = id;
    $("#name").val($(`#name-${id}`).html());
    $("#author").val($(`#author-${id}`).html());
    $("#genre").val($(`#genre-${id}`).html());
}

function DeleteClick(id) {
    $(`#${id}`).remove();
    $.ajax({
        type: "POST",
        url: "/Site/DeleteBook",
        data: { id },
        dataType: ""
    });
}

function SaveClick() {
    console.log(currentAction);
    var name = $("#name").val();
    var author = $("#author").val();
    var genre = $("#genre").val();
    if (currentAction == "Add") {
        $.ajax({
            type: "POST",
            url: "/Site/AddBook",
            data: { name, author, genre },
            dataType: "text",
            success: function (id) {
                lastId = parseInt(id);
                $(".table").append(`<tr id="${lastId}">
                                <td id="name-${lastId}" class="text-center">${name}</td>
                                <td id="author-${lastId}" class="text-center">${author}</td>
                                <td id="genre-${lastId}" class="text-center">${genre}</td>
                                <td class="text-center">
                                    <button style="color: blue;" onclick="EditClick(${lastId})">Изменить</button>
                                    <button style="color: red;" onclick="DeleteClick(${lastId})">Удалить</button>
                                </td>
                            </tr>`);
                $(".edit").fadeOut();
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Site/EditBook",
            data: { currentId, name, author, genre },
            dataType: ""
        });
        $(`#name-${currentId}`).html(name);
        $(`#author-${currentId}`).html(author);
        $(`#genre-${currentId}`).html(genre);
        $(".edit").fadeOut();
        currentId = -1;
    }
}