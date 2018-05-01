$("a").on("click", function (e) {
    if (e.target.className != "dropdown-toggle") {
        e.stopPropagation();
    }
});

$(".post").on("click", function (e) {
    e.stopPropagation();
});

$(function () {
    $('[data-toggle="popover"]').popover()
})

function insertpost() {
    $.ajax({
        url: "/Post/InsertPost/",
        type: "POST",
        data: {
            PostContent: document.getElementById("exampleInputEmail1").value
        }

    }).done(function (partialViewResult) {
        $(".posts").prepend(partialViewResult);
    })
    document.getElementById("exampleInputEmail1").value = "";

}

function loadajax(id) {
    $.ajax({
        url: "/Post/Comments/",
        type: "GET",
        data: {
            id: id
        }
    }).done(function (partialViewResult) {
        $("#commentmodalbody").html(partialViewResult);
    });
};

function getposts() {
    $.ajax({
        url: "/Post/_Posts/",
        type: "POST",
        data: {
            PostContent: document.getElementById("exampleInputEmail1").value
        }

    }).done(function (partialViewResult) {
        $(".posts").html(partialViewResult);
    })
    document.getElementById("exampleInputEmail1").value = "";
};

function like(id) {
    event.preventDefault();
    $.ajax({
        url: "/Post/LikePost",
        type: "Post",
        data: {
            id: id
        }
    }).done(function (result) {
        if (result == "succeed") {
            $("#likes" + id).html("Unlike");
            document.getElementById("likes" + id).setAttribute("onclick", "unlike(" + id + ")");
            setTimeout(likecount(id), 2000);
        }
    });
}

function unlike(id) {
    event.preventDefault();
    $.ajax({
        url: "/Post/UnlikePost",
        type: "Post",
        data: {
            id: id
        }
    }).done(function (result) {
        if (result == "succeed") {
            $("#likes" + id).html("Like");
            document.getElementById("likes" + id).setAttribute("onclick", "like(" + id + ")");
            setTimeout(likecount(id), 2000);
        }
    });
}

function likecount(id) {
    $.ajax({
        url: "/Post/LikeCount",
        type: "GET",
        data: {
            id: id
        }
    }).done(function (count) {
        if (count == "0") {
            $("#likescount" + id).html("");
        }
        else {
            $("#likescount" + id).html(count);
        }
    });
}

function editpost(id) {
    event.stopPropagation();
    event.preventDefault();
    var post = document.getElementById("postcontent" + id);
    var content = post.textContent;
    post.textContent = "";
    var div = document.createElement("div");
    div.setAttribute("class", "input-group");
    div.setAttribute("width", "100%");
    var edit = document.createElement("textarea");
    edit.setAttribute("width", "auto")
    edit.textContent = content;
    edit.setAttribute("class", "form-control");
    var btn = document.createElement("input");
    btn.setAttribute("type", "button");
    btn.setAttribute("class", "btn btn-default");
    btn.value = "Done";
    btn.onclick = function () { editdone(id, edit.value) };
    div.appendChild(edit);
    div.appendChild(btn);
    post.appendChild(div);
}

function editdone(id, content) {
    event.stopPropagation();
    $.ajax({
        url: "/Post/Edit",
        type: "POST",
        data: {
            PostID: id,
            PostContent: content
        }
    }).done(function (result) {
        if (result == "succeed") {
            $("#postcontent" + id).html(content);
        }
        else if (result == "failed") {

        }
    })
}

function showdeletemodal(id) {
    event.stopPropagation();
    event.preventDefault();
    $("#deletemodal").modal('show');
    var content = $("#postcontent" + id).text();
    $("#deletemodalbody").text(content);
    $(".btn-danger").attr("onclick", "deletepost(" + id + ")");
}

function showPPmodal() {
    event.stopPropagation();
    event.preventDefault();
    $("#updateppmodal").modal('show');
}

function deletepost(id) {
    $.ajax({
        url: "/Post/Delete",
        type: "GET",
        data: {
            id: id
        }
    }).done(function (result) {
        if (result == "succeed") {
            $("#" + id).remove();
            $("#deletemodal").modal('hide');
        }
        else {
            $("#deletemodal").modal('hide');
        }
    });
}

function sendRequest(RequestedUsername) {
    $.ajax({
        url: "/Profile/Add/",
        type: "Post",
        data: {
            RequestedUsername: RequestedUsername
        }
    }).done(function (result) {
        if (result == "succeeded") {
            $("#" + RequestedUsername).html("Friend request was sent successfully!")
        }
        else if (result == "failed") {
            $("#" + RequestedUsername).html("Friend request failed!")
        }
    })
};

function acceptrequest(username) {
    $.ajax({
        url: "/Profile/AcceptFriendRequest",
        type: "POST",
        data: {
            username: username
        }
    }).done(function (result) {
        if (result == "succeed") {
            $(".request" + username).html("You are now friends!");
        }
    })
}

function rejectrequest(username) {
    $.ajax({
        url: "/Profile/RejectFriendRequest",
        type: "POST",
        data: {
            username: username
        }
    }).done(function (result) {
        if (result == "succeed") {
            $(".request" + username).html("Friend request removed!");
        }
    })
}

function getlikers(id) {
    var str = "";
    $.ajax({
        url: "http://localhost:59474/api/swapi/getlikers/"+id,
        dataType: "json",
    }).done(function (data) {
        $.each(data, function (i, item) {
            str = str.concat("<h6>", item.FullName,"</h6>");
        })
        $("#likes" + id).attr("data-content", str);
        $("#likes" + id).popover('show');
        });
}

function hidepopover(id) {
    $("#likes" + id).popover('hide');
}