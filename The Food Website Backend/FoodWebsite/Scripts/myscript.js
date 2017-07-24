﻿var broadcastid;
var restaurantid;

function LoadRestaurants(restList) {
    if (restList != null && restList.length > 0)
    {
        $('#orderSetupItems li:not(:first)').remove();
        $("#orderSetupItems").append("<li class=\"restButton list-group-item active\" restId = " + restList[restList.length - 1].RestaurantID + ">" + restList[restList.length - 1].Name + "</li>");
        for (var i = restList.length - 2; i >= 0; i--) {
            var name = restList[i].Name;
            $("#orderSetupItems").append("<li class=\"restButton list-group-item\" restId = " + restList[i].RestaurantID + ">" + name + "</li>");
        }
    }
}



function LoadTotalReceipt(receipt)
{
    $("#total_receipt").empty();
    receiptItems = receipt.ReceiptItems;
    total = receipt.Total;
    for (i = 0; i < receiptItems.length; i++)
    {
        $("#total_receipt").append("<tr class=\"details order_details\"><td><p>" + receiptItems[i].Email + "</p></td><td><p>" + receiptItems[i].Total + "</p></td></tr>");
    }
    if (total != null && total > 0)
    {
        $("#history_total_amount").text("Total: " + total);
    }
    else
    {
        $("#history_total_amount").text("Total is undefined. Please set prices for all items");
    }

}


function LoadOrders(orders) {
    $('#user_order_details').empty();
    if (orders != null)
    {
        for (var i = 0; i < orders.length; i++) {
            var name = orders[i].Item.Name;
            var quantity = orders[i].Quantity;
            var comments = orders[i].comments;
            $("#user_order_details").append("<tr><td>" + name + "</td><td>" + quantity + "</td><td>" + comments + "</th><td>Delete</td></tr>");
        }
    }
}

function LoadRestaurantItems(restList) {
    if (restList != null && restList.length > 0)
    {
        $('#ItemSetupItems li:not(:first)').remove();
        $("#ItemSetupItems").append("<li class=\"restButton items_list list-group-item active\">" + restList[restList.length - 1] + "</li>");
        for (var i = restList.length - 2; i >= 0; i--) {
            $("#ItemSetupItems").append("<li class=\"restButton items_list list-group-item\">" + restList[i] + "</li>");
        }
    }
}


function initializeOrderList()
{
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
        a = li[i];
        if (a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1) {
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
    $("#trans").hide();
    $("#will_order").hide();
    $("#close_order").hide();
    $("#review_order_table").hide();
    $("#settled_btn").hide();
    $("#unsettled_btn").hide();
    $("#history_trans").hide();
    $("#to_settle").hide();
    $("#to_settle_orders").hide();
    $("#done_settling").hide();


    $("#add_item").click(function () {
        var new_item = prompt("Please enter the item name");
        $('#ItemSetupFilterList').val(new_item);
        AddRestaurantItem(new_item, restaurantid);
    });


    

    $("#orderSetupItems").on("click", ".restButton", function () {
        $("#orderSetupItems").hide();
        $("#orderSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
    });

    $("#ItemSetupItems").on("click", ".restButton", function () {
       
        $("#ItemSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
        // Find a <table> element with id="myTable":
        var name = $("#ItemSetupFilterList").val();

        $("#user_order_details").append("<tr><td>" + name + "</td><td>1</td><td>some comment</td><td><input type=\"button\"></td>");
    });

    $("#done_settling").click(function () {
        $("#done_settling").hide();
        $("#to_settle").hide();
        $("#history").show();

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
        $("#trans").hide();
        $("#will_order").hide();
        $("#close_order").hide();
        $("#review_order_table").hide();
        LoadHistory();
        
    });

   

    

    $("#close_order").click(function () {
        
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#close_order").hide();
        $("#review_order_table").hide();
        CloseBroadcast(broadcastid);
        $("#history").show();
        
    });

    $("#details").click(function () {
        var details = prompt("please type any special comments you would like to include in the order");
    });
    $("#total_receipt").on("click", "tr", function () {
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
        $("#trans").hide();
        $("#will_order").hide();
        $("#close_order").hide();
        $("#review_order_table").hide();
        $("#history_trans").hide();
        $("#to_settle").hide();
        $("#to_settle_orders").hide();
        $("#done_settling").hide();
        LoadBroadcasts();
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
    $("#order_btn").click(function () {
        $("#trans").hide();
        $("#home_select").show();
        $("#check_btn").show();

    });
    $("#review_btn").click(function () {
        $("#trans").hide();
        $("#will_order").show();
        $("#review_order_table").show();
        $("#close_order").show();
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
        var id = $($("#orderSetupItems").find("li.active")).attr("restid");
        var deadline = $("#timepicker1").val();
        AddBroadcast(id, deadline);

    });

    $(".orders").on("click", "button", function () {
        broadcastid = $(this).attr('unique_id');
        restaurantid = $(this).attr('restaurantid');
        $("#history").hide();
        $("#history_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").show();
        CurrentOrder(broadcastid);
        GetRestaurantItems(restaurantid);
    });

    $("#add_restaurant").click(function () {
        var new_res = prompt("Please enter the restaurant name");
        $('#orderSetupFilterList').val(new_res);
        AddRestaurant(new_res);
    });

   

    $("#history").on("click", "button", function () {
        GetReciept($(this).attr('unique_id'));
        $("#history").hide();
        $("#history_select").show();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
    });

    $("#check_btn").click(function () {
        var names = $("#user_order_details tr td:nth-child(1)").map(function () {
            return $(this).text();
        }).get();
        var quantity = $("#user_order_details tr td:nth-child(2)").map(function () {
            return parseInt($(this).text());
        }).get();
        var comments = $("#user_order_details tr td:nth-child(3)").map(function () {
            return $(this).text();
        }).get();
        AddOrder(names, quantity, comments, broadcastid);
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").show();
    });

    LoadBroadcasts();
});





    
