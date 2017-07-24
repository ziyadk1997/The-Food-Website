var broadcastid;
var restaurantid;
function LoadRestaurants(restList) {
    $('#orderSetupItems li:not(:first)').remove();
    for (var i = 0; i < restList.length; i++) {
        var name = restList[i].Name;
        $("#orderSetupItems").append("<li class=\"restButton list-group-item\" restId = " + restList[i].RestaurantID + ">" + name + "</li>");

    }
}

function LoadRestaurantItems(items) {
    $('#ItemSetupItems li:not(:first)').remove();
    for (var i = 0; i < items.length; i++) {
        var name = items[i].Name;
        $("#ItemSetupItems").append("<li> <a href=#>"+ name + "</a> </li>");
    }
}


function initializeOrderList() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("orderSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("orderSetupItems");
    ul.style.display = "";
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) {
        a = li[i];
        if (a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }

}

function initializeItemList() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("ItemSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("ItemSetupItems");
    ul.style.display = "";
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }

}

$(document).ready(function () {


    $("#history").hide();
    $("#history_select").hide();
    $("#home_select").hide();
    $("home").show();
    $("#add_btn").show();
    $("#check_btn").hide();
    $("#recipt").hide();
    $("#order_setup").hide();




    $("#ItemSetupItems li a ").click(function () {
        $("#ItemSetupItems").hide();
        $("#ItemSetupFilterList").val(this.innerText);
    });

    $("#orderSetupItems").on("click", ".restButton", function () {
        $("#orderSetupItems").hide();
        $("#orderSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
    });





    $("#history_btn").click(function () {
        $("#history").show();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
    });
    $("#close_order").click(function () {
        $("#history").show();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        CloseBroadcast(broadcastid);
    });

    $("#details").click(function () {
        var details = prompt("please type any special comments you would like to include in the order");
    });
    $(".order_details").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").show();
        $("#order_setup").hide();
    });
    $("#current_order_btn").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").show();
        $("#add_btn").show();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        LoadBroadcaststs();
    });
    $("#add_btn").on("click", function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").show();
        GetRestaurants();
    });
    $("#done").on("click", function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").show();
        $("#add_btn").show();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        var name = prompt("Please enter your name");
        var id = $($("#orderSetupItems").find("li.active")).attr("restid");
        var deadline = $("#timepicker1").val();
        AddBroadcast(id, deadline);

    });
    $(".orders").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").show();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").show();
        $("#recipt").hide();
        $("#order_setup").hide();
    });
    $("#add_restaurant").click(function () {
        var new_res = prompt("Please enter the restaurant name");
        $('#orderSetupFilterList').val(new_res);
        AddRestaurant(new_res);
    });
    $("#add_item").click(function () {
        var new_item = prompt("Please enter the item name");
        //AddRestaurantItem(new_item, restaurantid);
        this.innerText = new_item;

    });
    $(".pay").click(function () {
        broadcastid = $(this).attr('unique_id');
        //restaurantid = $(this).attr('restaurantid');
        $("#history").hide();
        $("#history_select").show();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
    });
    $("#check_btn").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").show();
        $("#add_btn").show();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
    });

    LoadBroadcaststs();
});






